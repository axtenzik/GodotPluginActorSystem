using Electronova.Generic;
using Godot;
using Godot.Collections;
using System;

namespace Electronova.Actors
{
	[Tool]
    [GlobalClass, Icon("res://addons/Electronova/Icons/Actor/ActorContacts.png")]
    public partial class ActorContacts : Node
    {
        [Export] Actor Parent { get; set; }
        [Export] JumpStats JumpStatistics { get; set; }
        [Export] bool usePhysicsProcess = true;

        [ExportCategory("StringNode")]
        [Export] public StringNode ContactState { get; set; }

        [ExportCategory("Ground Handling")]
        [Export(PropertyHint.Range, "0, 100")] float maxSnapSpeed = 100f;
        [Export(PropertyHint.Range, "0, 90")] float maxGroundAngle = 30f, maxTraversalAngle = 50f;
        [Export(PropertyHint.Range, "0, 100")] float probeDistance = 1f;

        [ExportGroup("Masks")]
        [Export(PropertyHint.Layers3DPhysics)] public int ProbeMask { get; set; }
        [Export(PropertyHint.Layers3DPhysics)] public int WaterMask { get; set; }
        [Export(PropertyHint.Layers3DPhysics)] public int TraversalMask { get; set; }

        Vector3 contactNormal, steepNormal, wallNormal, ceilingNormal;
        int groundContactCount, steepContactCount, wallContactCount, ceilingContactCount;
        float minGroundDotProduct, minTraversalDotProduct;
        //bool desiredJump;

        public Vector3 ContactNormal => contactNormal;
        public Vector3 SteepNormal => steepNormal;
        public Vector3 WallNormal => wallNormal;
        public Vector3 CeilingNormal => ceilingNormal;

        private RigidBody3D ConnectedBody, PreviousBody;
        public Vector3 ConnectedVelocity;
        private Vector3 connectionPoint, connectedWorldPosition, connectedLocalPosition;

        public override void _Ready()
        {
            minGroundDotProduct = Mathf.Cos(Mathf.DegToRad(maxGroundAngle));
            minTraversalDotProduct = Mathf.Cos(Mathf.DegToRad(maxTraversalAngle));
            SetProcess(!usePhysicsProcess);
            SetPhysicsProcess(usePhysicsProcess);
        }

        public override void _Process(double delta)
        {
            if (Engine.IsEditorHint())
            {
                return;
            }

            Clear();
            EvaluateCollisions();
            UpdateContacts();

            if (ConnectedBody != null)
            {
                if (ConnectedBody.FreezeMode == RigidBody3D.FreezeModeEnum.Kinematic || ConnectedBody.Mass >= Parent.Mass)
                {
                    UpdateConnectionState();
                }
            }
        }

        public override void _PhysicsProcess(double delta)
        {
            if (Engine.IsEditorHint())
            {
                return;
            }

            Clear();
            EvaluateCollisions();
            UpdateContacts();

            if (ConnectedBody != null)
            {
                if (ConnectedBody.FreezeMode == RigidBody3D.FreezeModeEnum.Kinematic || ConnectedBody.Mass >= Parent.Mass)
                {
                    UpdateConnectionState();
                }
            }
        }

        private void Clear()
        {
            ContactState.Value = Strings.None;
            groundContactCount = steepContactCount = 0;
            contactNormal = steepNormal = Vector3.Zero;
            ConnectedVelocity = connectionPoint = Vector3.Zero;
            PreviousBody = ConnectedBody;
            ConnectedBody = null;
        }

        bool CheckSteepContacts()
        {
            if (steepContactCount > 1)
            {
                steepNormal.Normalized();
                float upDot = steepNormal.Dot(Parent.UpAxis);
                if (upDot >= minGroundDotProduct)
                {
                    groundContactCount = 1;
                    contactNormal = steepNormal;
                    return true;
                }
            }
            return false;
        }

        void EvaluateCollisions()
        {
            //Early return as first physics frame can be before actor sets BodyState
            if (Parent.BodyState == null)
            {
                return;
            }

            for (int i = 0; i < Parent.BodyState.GetContactCount(); i++)
            {
                CollisionObject3D obj = (CollisionObject3D)Parent.BodyState.GetContactColliderObject(i);
                float minDot = GetMinDot((int)obj.CollisionLayer);
                Vector3 normal = Parent.BodyState.GetContactLocalNormal(i);
                float upDot = normal.Dot(Parent.UpAxis);
                if (upDot >= minDot)
                {
                    groundContactCount++;
                    contactNormal += normal;

                    RigidBody3D rigidBody = Parent.BodyState.GetContactColliderObject(i) as RigidBody3D;
                    ConnectedBody = rigidBody;
                    connectionPoint = Parent.BodyState.GetContactLocalPosition(i);
                }
                else if (upDot > -0.01f)
                {
                    steepContactCount++;
                    steepNormal += normal;
                    if (groundContactCount == 0)
                    {
                        RigidBody3D rigidBody = Parent.BodyState.GetContactColliderObject(i) as RigidBody3D;
                        ConnectedBody = rigidBody;
                        connectionPoint = Parent.BodyState.GetContactLocalPosition(i);
                    }
                }
            }

            if (groundContactCount >= 1)
            {
                ContactState.Value = Strings.OnGround;
            }
            else if (steepContactCount >= 1)
            {
                ContactState.Value = Strings.OnSteep;
            }
            else if (wallContactCount >= 1)
            {
                ContactState.Value = Strings.OnWall;
            }
            else if (ceilingContactCount >= 1)
            {
                ContactState.Value = Strings.OnCeiling;
            }
            else
            {
                ContactState.Value = Strings.None;
            }
        }

        float GetMinDot(int layer)
        {
            return (TraversalMask & layer) == TraversalMask ? minTraversalDotProduct : minGroundDotProduct;
            //return (TraversalMask & (1 << layer)) == 0 ? minGroundDotProduct : minTraversalDotProduct; //Catlike
        }

        bool SnapToGround()
        {
            if (JumpStatistics.StepsSinceLastGrounded > 1 || JumpStatistics.StepsSinceLastJump <= 2)
            {
                return false;
            }

            float speed = Parent.Velocity.Length();
            if (speed > maxSnapSpeed)
            {
                return false;
            }

            PhysicsDirectSpaceState3D spaceState = Parent.GetWorld3D().DirectSpaceState;
            PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create
            (Parent.Transform.Origin, Parent.Transform.Origin - Parent.UpAxis * probeDistance);
            query.CollideWithAreas = true;
            query.CollisionMask = (uint)ProbeMask;
            Dictionary result = spaceState.IntersectRay(query);

            if (result.Count == 0)
            {
                return false;
            }

            Vector3 rayHitNormal = (Vector3)result["normal"];
            CollisionObject3D obj = (CollisionObject3D)result["collider"];
            float minDot = GetMinDot((int)obj.CollisionLayer);
            float upDot = rayHitNormal.Dot(Parent.UpAxis);

            if (upDot < minDot)
            {
                return false;
            }

            groundContactCount = 1;
            contactNormal = rayHitNormal;
            float dot = Parent.Velocity.Dot(rayHitNormal);

            if (dot > 0f)
            {
                Vector3 velocity = (Parent.Velocity - rayHitNormal * dot).Normalized() * speed; //desired velocity
                velocity -= Parent.Velocity; //current velocity
                Parent.AddVelocity(velocity); //Once added should be desired velocity
            }

            //I hate I have to cast then cast again, Why godot why???????
            RigidBody3D rigidBody = (GodotObject)result["collider"] as RigidBody3D;
            ConnectedBody = rigidBody;
            connectionPoint = (Vector3)result["position"];

            return true;
        }

        private void UpdateConnectionState()
        {
            if (ConnectedBody == PreviousBody)
            {
                //Turn point from local of previous body to global based on current transform
                Vector3 newGlobalPosition = ConnectedBody.Transform * connectedLocalPosition;
                Vector3 connectionMovement = newGlobalPosition - connectedWorldPosition;

                ConnectedVelocity = connectionMovement / Parent.DeltaStep;
            }

            //Turn point to local of connectedBody
            connectedWorldPosition = connectionPoint;

            //Vector3 * Transform == Transform.Inverse * Vector3
            //AfflineInverse used as Vector3 * Transform assumes basis is orthonormal
            //scaling Transform makes basis non orthonormal. e.g. is don't work properly
            connectedLocalPosition = ConnectedBody.Transform.AffineInverse() * connectedWorldPosition;
        }

        void UpdateContacts()
        {
            JumpStatistics.AddGroundedStep();
            JumpStatistics.AddJumpStep();

            bool onGround = ContactState.Value == Strings.OnGround;

            if (onGround || SnapToGround() || CheckSteepContacts())
            {
                JumpStatistics.ResetGroundedSteps();
                if (JumpStatistics.StepsSinceLastJump > 1)
                {
                    JumpStatistics.ResetJumps();
                }

                if (groundContactCount > 1)
                {
                    contactNormal.Normalized();
                }
            }
            else
            {
                contactNormal = Parent.UpAxis.Normalized();
            }
        }
    }
}