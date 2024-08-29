using Godot;
using Godot.Collections;
using System;

namespace Electronova.Actors
{
	public partial class RaycastPerformer : Node, IStateTree
	{
        public StringName State => Name;

		[ExportCategory("Actor")]
        [Export] Actor parent;

		[ExportCategory("Raycast")]
		[Export(PropertyHint.Range, "0, 100")] float probeDistance = 1f;

		[ExportGroup("Masks")]
        [Export(PropertyHint.Layers3DPhysics)] public int ProbeMask { get; set; }

		public void Tick()
		{
			if (GetChildCount() == 0)
            {
                return;
            }

			PhysicsDirectSpaceState3D spaceState = parent.GetWorld3D().DirectSpaceState;
            PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create
            (parent.Transform.Origin, parent.Transform.Origin - parent.UpAxis * probeDistance);
            query.CollideWithAreas = true;
            query.CollisionMask = (uint)ProbeMask;
            Dictionary result = spaceState.IntersectRay(query);

			if (result.Count == 0)
            {
                if (GetChildCount() < 2)
                {
                    return;
                }

                IStateTree selectedChild = (IStateTree)GetChild(1);
                selectedChild?.Tick();
            }
            else
            {
                if (GetChildCount() == 0)
                {
                    return;
                }

                IStateTree selectedChild = (IStateTree)GetChild(0);
                selectedChild?.Tick();
            }
		}
	}
}