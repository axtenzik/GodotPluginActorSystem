using Godot;
using Godot.Collections;
using System;

namespace Electronova.Actors
{
    [Tool]
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Selector.png")]
	public partial class RaycastPerformer : StateTree
	{
        [ExportCategory("Actor")]
        [Export] Actor parent;

		[ExportCategory("Raycast")]
		[Export(PropertyHint.Range, "0, 100")] float probeDistance = 1f;

		[ExportGroup("Masks")]
        [Export(PropertyHint.Layers3DPhysics)] public int ProbeMask { get; set; }

		public override void Tick()
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

                StateTree selectedChild = (StateTree)GetChild(1);
                selectedChild?.Tick();
            }
            else
            {
                StateTree selectedChild = (StateTree)GetChild(0);
                selectedChild?.Tick();
            }
		}
	}
}