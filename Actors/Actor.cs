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

        //private Vector3 bufferedVelocity;
        public Vector3 ConnectedVelocity;
        private Vector3 connectedWorldPosition, connectedLocalPosition;
        private Node3D parent;

        public override void _Ready()
        {
            UpAxis = Gravity.GetUpAxis(Transform.Origin);
            CustomIntegrator = true;
            ContactMonitor = true;
            MaxContactsReported = 5;
            LockRotation = true;
            parent = GetParentNode3D();
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
                //Vector3 connectionMovement = ConnectedBody.Position - connectedWorldPosition;

                Vector3 newGlobalPosition = ConnectedBody.Transform * connectedLocalPosition;
                Vector3 connectionMovement = newGlobalPosition - connectedWorldPosition;

                ConnectedVelocity = connectionMovement / DeltaStep;
            }
            
            //connectedWorldPosition = ConnectedBody.Position;

            connectedWorldPosition = Position;
            connectedLocalPosition = connectedWorldPosition * ConnectedBody.Transform;
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