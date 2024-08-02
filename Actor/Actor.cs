using Electronova.Generic;
using Electronova.World;
using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    public partial class Actor : RigidBody3D
    {
        [Export] public Node3D ControllingCamera { get; private set; }
        [Export] public ActorInput Inputter { get; private set; }
        [Export] public ActorUpdate Updater { get; private set; }
        [Export] StateSelector StateTreeRoot { get; set; }

        public PhysicsDirectBodyState3D BodyState { get; private set; }
        public Vector3 Force { get; private set; }
        public Vector3 Velocity { get; private set; }
        public Vector3 UpAxis { get; private set; }
        public float DeltaStep { get; private set; }

        [Export] StateString actorState;
        [Export] StateString fluidState;
        [Export] StateString contactState;
        [Export] StateString inputState;
        [Export] JumpStats jumpstat;//*/

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

            Updater.Update();
            StateTreeRoot.Tick();

            Velocity += Gravity.GetGravity(Transform.Origin) * DeltaStep;

            state.LinearVelocity = Velocity;
            //GD.Print("Actor: " + actorState.State);
            //GD.Print("Fluid: " + fluidState.State);
            //GD.Print("Contact: " + contactState.State);
            //GD.Print("Input: " + inputState.State);
            //GD.Print("Contacts: " + BodyState.GetContactCount());
            //GD.Print(jumpstat.StepsSinceLastJump);
            //GD.Print("");
        }

        public void AddForce(Vector3 deltaForce)
        {
            Force += deltaForce;
        }

        public void AddVelocity(Vector3 deltaVelocity)
        {
            Velocity += deltaVelocity;
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
            Velocity = velocity;
        }
    }
}