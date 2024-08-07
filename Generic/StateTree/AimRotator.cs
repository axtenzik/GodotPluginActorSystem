using Electronova.Actors;
using Godot;
using System;
using System.Collections.Generic;

namespace Electronova.Generic
{
    [Tool]
    public partial class AimRotator : Node, IStateTree
    {
        [ExportCategory("Rotator")]
        [Export] Actor parent;

        [ExportCategory("State Tree")]
        [Export] StringName rotatorState = null;

        public StringName State => rotatorState;

        Vector3 forwardAxis;

        public override string[] _GetConfigurationWarnings()
        {
            List<String> list = new();
            if (parent == null)
            {
                list.Add("Please set parent to a valid instance of IActor3D");
            }
            string[] strings = list.ToArray();
            return strings;
        }

        public void Tick()
        {
            if (parent.ControllingCamera != null)
            {
                Vector3 forward = parent.ControllingCamera.Transform.Basis.Z;
                forward.Normalized();
                forwardAxis = forward.ProjectOntoPlane(parent.UpAxis);
            }
            else
            {
                forwardAxis = Vector3.Forward.ProjectOntoPlane(parent.UpAxis);
            }

            Vector3 lookDirection = forwardAxis.ProjectOntoPlane(parent.UpAxis);

            if (lookDirection == Vector3.Zero)
            { 
                return;
            }

            Basis lookBasis = Basis.LookingAt(-lookDirection, parent.UpAxis);

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