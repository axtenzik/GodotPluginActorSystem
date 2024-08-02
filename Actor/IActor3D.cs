using Godot;
using System;

namespace Electronova.Actors
{
    interface IActor3D
    {
        //Maybe in future add this so it doesn't matter if the actor used is a characterbody3d or a rigidbody3d
        public Node3D ControllingCamera { get; }
        public ActorUpdate Updater { get; }
        public Vector3 Velocity { get; }
        public Vector3 UpAxis { get; }
        public float DeltaStep { get; }
        public void AddVelocity(Vector3 deltaVelocity);
        public void SetRotation(Basis deltaBasis);
        public void SetVelocity(Vector3 velocity);
    }
}