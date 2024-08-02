using Electronova.Generic;
using Godot;
using System;

namespace Electronova.Actors
{
    public abstract partial class ActorState : Node3D
    {
        [Export] public StateString ContactState { get; set; }

        protected Vector3 contactNormal, steepNormal;
        protected int groundContactCount, steepContactCount;

        public virtual Vector3 ContactNormal => contactNormal;
        public virtual Vector3 SteepNormal => steepNormal;

        public abstract void Clear();

        public abstract void Update(Actor actor);
    }
}