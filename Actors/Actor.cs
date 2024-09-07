using Electronova.Generic;
using Electronova.World;
using Godot;
using System;
using System.Collections.Generic;

namespace Electronova.Actors
{
    [GlobalClass, Icon("res://addons/Electronova/Icons/Actor/Actor.png")]
    public partial class Actor : RigidBody3D
    {
        [Export] public Node3D ControllingCamera { get; private set; }
        [Export] ActorController actorController;

        public RigidBody3D ConnectedBody, PreviousBody;
        public PhysicsDirectBodyState3D BodyState { get; private set; }
        public Vector3 Force { get; private set; }
        public Vector3 Velocity { get; private set; }
        public Vector3 UpAxis { get; private set; }
        public float DeltaStep { get; private set; }

        public Vector3 ConnectedVelocity, ConnectionPoint;
        private Vector3 connectedWorldPosition, connectedLocalPosition;

        public override void _Ready()
        {
            UpAxis = Gravity.GetUpAxis(Transform.Origin);
            CustomIntegrator = true;
            ContactMonitor = true;
            MaxContactsReported = 5;
            LockRotation = true;
        }

        public override void _IntegrateForces(PhysicsDirectBodyState3D state)
        {
            DeltaStep = state.Step;
            Velocity = state.LinearVelocity;
            BodyState = state;

            //Velocity += bufferedVelocity;
            //bufferedVelocity = Vector3.Zero;

            if (ConnectedBody != null)
            {
                if (ConnectedBody.FreezeMode == FreezeModeEnum.Kinematic || ConnectedBody.Mass >= Mass)
                {
                    UpdateConnectionState();
                    //CallDeferred(Node3D.MethodName.Reparent, ConnectedBody, true);
                }
            }

            actorController?.Start();

            //bufferedVelocity += Gravity.GetGravity(Transform.Origin) * DeltaStep;
            Velocity += Gravity.GetGravity(Transform.Origin) * DeltaStep;

            state.LinearVelocity = Velocity;
        }

        private void UpdateConnectionState()
        {
            if (ConnectedBody == PreviousBody)
            {
                //Turn point from local of previous body to global based on current transform
                Vector3 newGlobalPosition = ConnectedBody.Transform * connectedLocalPosition;
                Vector3 connectionMovement = newGlobalPosition - connectedWorldPosition;

                ConnectedVelocity = connectionMovement / DeltaStep;
            }

            //Turn point to local of connectedBody
            connectedWorldPosition = ConnectionPoint;

            //Vector3 * Transform == Transform.Inverse * Vector3
            //AfflineInverse used as Vector3 * Transform assumes basis is orthonormal
            //scaling Transform makes basis non orthonormal. e.g. is don't work properly
            connectedLocalPosition = ConnectedBody.Transform.AffineInverse() * connectedWorldPosition;
        }

        public void AddForce(Vector3 deltaForce)
        {
            Force += deltaForce;
        }

        public void AddVelocity(Vector3 deltaVelocity)
        {
            //bufferedVelocity += deltaVelocity;
            Velocity += deltaVelocity;
        }

        public void SetCamera(Node3D camera)
        {
            ControllingCamera = camera;
        }

        public void SetForce(Vector3 force)
        {
            Force = force;
        }

        public void SetRotation(Basis deltaBasis)
        {
            Transform3D bodyTransform = Transform;
            bodyTransform.Basis = deltaBasis;
            Transform = bodyTransform;
        }

        public void SetVelocity(Vector3 velocity)
        {
            LinearVelocity = velocity;
        }
    }
}