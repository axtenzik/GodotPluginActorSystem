using Electronova.Generic;
using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Rotator.png")]
    public partial class MoveRotator : StateTree
    {
        [Export] Actor parent;

        public override string[] _GetConfigurationWarnings()
        {
            return Array.Empty<string>();
        }

        public override void Tick()
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
            

            base.Tick();
        }
    }
}