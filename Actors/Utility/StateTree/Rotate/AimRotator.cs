using Electronova.Generic;
using Godot;
using System;
using System.Collections.Generic;

namespace Electronova.Actors
{
    /// <summary>
    /// Changes the actors rotation based on the look direction of the camera connected to the actor.
    /// If no camera is connected to the actor then the world forward axis is used instead.
    /// </summary>
    [Tool]
    public partial class AimRotator : Node, IStateTree
    {
        [ExportCategory("Rotator")]
        [Export] Actor parent;

        public StringName State => Name;

        Vector3 forwardAxis;

        public override string[] _GetConfigurationWarnings()
        {
            List<String> list = new();
            if (parent == null)
            {
                list.Add("Please set parent to a valid instance of Actor");
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

            //Early return due to a Vector3 of zero will cause an error in Basis.LookingAt()
            //This keeps the actors current rotation instead of changing it.            
            if (lookDirection == Vector3.Zero)
            { 
                return;
            }

            Basis lookBasis = Basis.LookingAt(-lookDirection, parent.UpAxis);

            parent.SetRotation(lookBasis);

            //If node has any children will try continuing down the tree through its first child.
            if (GetChildCount() == 0)
            {
                return;
            }
            
            IStateTree selectedChild = (IStateTree)GetChild(0);
            selectedChild?.Tick();
        }
    }
}