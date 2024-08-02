using Godot;
using System;

namespace Electronova.World
{
    public partial class GravityPlane : GravitySource
    {
        [Export] float gravity = 10f;

        public override Vector3 GetGravity (Vector3 position) 
        {
            Vector3 up = GlobalTransform.Basis.Y; //https://ask.godotengine.org/67052/how-do-i-get-local-space-vectors
            return -gravity * up;
        }
    }
}