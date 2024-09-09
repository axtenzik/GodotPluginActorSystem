using Electronova.World;
using Godot;

namespace Electronova.Actors
{
	[Tool]
    [GlobalClass, Icon("res://addons/Electronova/Icons/Actor/Actor.png")]
    public partial class Actor : RigidBody3D
    {
        [Export] public Node3D ControllingCamera { get; private set; }
        [Export] ActorController actorController;

        public PhysicsDirectBodyState3D BodyState { get; private set; }
        public Vector3 Force { get; private set; }
        public Vector3 Velocity { get; private set; }
        public Vector3 UpAxis { get; private set; }
        public float DeltaStep { get; private set; }

        Vector3 bufferedVelocity;

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
            if (Engine.IsEditorHint())
            {
                return;
            }

            DeltaStep = state.Step;
            Velocity = state.LinearVelocity;
            BodyState = state;

            Velocity += bufferedVelocity;
            bufferedVelocity = Vector3.Zero;

            actorController?.Start();

            bufferedVelocity += Gravity.GetGravity(Transform.Origin) * DeltaStep;
            //Velocity += Gravity.GetGravity(Transform.Origin) * DeltaStep;

            state.LinearVelocity = Velocity;
        }

        public void AddForce(Vector3 deltaForce)
        {
            Force += deltaForce;
        }

        public void AddVelocity(Vector3 deltaVelocity)
        {
            bufferedVelocity += deltaVelocity;
            //Velocity += deltaVelocity;
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