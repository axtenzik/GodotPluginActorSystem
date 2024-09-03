using Godot;
using System;

namespace Electronova.World
{
    [GlobalClass, Icon("res://addons/Electronova/Icons/World/Gravity.png")]
    public partial class GravitySource : Node3D
    {
        public override void _EnterTree()
        {
            Gravity.Register(this);
        }

        public override void _ExitTree()
        {
            Gravity.Unregister(this);
        }

        public virtual Vector3 GetGravity (Vector3 position) 
        {
            return Vector3.Down * 10;
        }
    }
}