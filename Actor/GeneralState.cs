using Godot;
using Godot.Collections;
using System;

namespace Electronova.Actors
{
    public partial class GeneralState : ActorState
    {
        [ExportCategory("Ground Handling")]
        [Export(PropertyHint.Range, "0, 100")] float maxSnapSpeed = 100f;
        [Export(PropertyHint.Range, "0, 90")] float maxGroundAngle = 30f, maxTraversalAngle = 50f;
        [Export(PropertyHint.Range, "0, 100")] float probeDistance = 1f;

        [ExportGroup("Masks")]
	    [Export(PropertyHint.Layers3DPhysics)] public int ProbeMask { get; set; }
        [Export(PropertyHint.Layers3DPhysics)] public int WaterMask { get; set; }
	    [Export(PropertyHint.Layers3DPhysics)] public int TraversalMask { get; set; }

        Actor actor;
        int stepsSinceLastGrounded, stepsSinceLastJump;
        float minGroundDotProduct, minTraversalDotProduct;

        static readonly StringName noneString = new("None");
        static readonly StringName onGroundString = new("OnGround");
        static readonly StringName onSteepString = new("OnSteep");

        public override void _Ready()
        {
            minGroundDotProduct = Mathf.Cos(Mathf.DegToRad(maxGroundAngle));
            minTraversalDotProduct = Mathf.Cos(Mathf.DegToRad(maxTraversalAngle));
        }

        public override void Clear()
        {
            //actorContactState = ActorContactState.InAir;
            ContactState.State = noneString;
            groundContactCount = steepContactCount = 0;
            contactNormal = steepNormal = Vector3.Zero;
        }

        public override void Update(Actor _actor)
        {
            actor = _actor;
            EvaluateCollisions();
            UpdateContacts();
        }

        bool CheckSteepContacts()
        {
            if (steepContactCount > 1)
            {
                steepNormal.Normalized();
                float upDot = steepNormal.Dot(actor.UpAxis);
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
            for (int i = 0; i < actor.BodyState.GetContactCount(); i++)
            {
                CollisionObject3D obj = (CollisionObject3D)actor.BodyState.GetContactColliderObject(i);
                float minDot = GetMinDot((int)obj.CollisionLayer);
                Vector3 normal = actor.BodyState.GetContactLocalNormal(i);
                float upDot = normal.Dot(actor.UpAxis);
                if (upDot >= minDot)
                {
                    //actorContactState |= ActorContactState.OnGround;
                    ContactState.State = onGroundString;
                    groundContactCount++;
                    contactNormal += normal;
                }
                else if (upDot > -0.01f)
                {
                    //actorContactState |= ActorContactState.OnSteep;
                    if (ContactState.State != onGroundString)
                    {
                        ContactState.State = onSteepString;
                    }
                    steepContactCount++;
                    steepNormal += normal;
                }
            }
        }

        float GetMinDot(int layer)
        {
            return (TraversalMask & layer) == TraversalMask ? minTraversalDotProduct : minGroundDotProduct;
        }

        bool SnapToGround()
        {
            if (stepsSinceLastGrounded > 1 || stepsSinceLastJump <= 2)
            {
                return false;
            }

            float speed = actor.Velocity.Length();
            if (speed > maxSnapSpeed)
            {
                return false;
            }

            PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;
            PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create(Transform.Origin, Transform.Origin - actor.UpAxis * probeDistance);
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
            float upDot = rayHitNormal.Dot(actor.UpAxis);

            if (upDot < minDot)
            {
                return false;
            }

            groundContactCount = 1;
            contactNormal = rayHitNormal;
            float dot = actor.Velocity.Dot(rayHitNormal);

            if (dot > 0f)
            {
                Vector3 velocity = (actor.Velocity - rayHitNormal * dot).Normalized() * speed;
                actor.AddVelocity(velocity);
            }

            return true;
        }

        void UpdateContacts()
        {
            stepsSinceLastGrounded++;

            bool onGround = ContactState.State == onGroundString;

            if (onGround || SnapToGround() || CheckSteepContacts())
            {
                stepsSinceLastGrounded = 0;
                if (stepsSinceLastJump > 1)
                {
                    //jumpPhase = 0;
                }

                if (groundContactCount > 1)
                {
                    contactNormal.Normalized();
                }
            }
            else
            {
                contactNormal = actor.UpAxis.Normalized();
            }
        }
    }
}