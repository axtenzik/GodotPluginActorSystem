using Electronova.Actors;
using Godot;
using System;

namespace Electronova.Generic
{
    [Tool]
    public partial class MoveRotator : Node, IStateTree
    {
        [Export] Actor parent;

        [ExportCategory("State Tree")]
        [Export] StringName rotatorState = null;

        public StringName State => rotatorState;

        public void Tick()
        {
            if (parent.Velocity.LengthSquared() < 0.01f)
            {
                return;
            }

            Vector3 lookDirection = parent.Velocity.ProjectOntoPlane(parent.UpAxis);

            if (lookDirection == Vector3.Zero)
            { 
                return;
            }

            Basis lookBasis = Basis.LookingAt(lookDirection, parent.UpAxis);

            parent.SetRotation(lookBasis);
            

            if (GetChildCount() == 0)
            {
                return;
            }

            IStateTree selectedChild = (IStateTree)GetChild(0);
            selectedChild?.Tick();
        }
    }
}