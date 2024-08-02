using Electronova.Actors;
using Godot;
using System;

namespace Electronova.Generic
{
    [Tool]
    public partial class MotionPerformer : Performer
    {
        [ExportCategory("Actor")]
        [Export] Actor Parent;
        [Export] ActorUpdate actorUpdate;

        [ExportCategory("Motion")]
        [Export] float speed;
        [Export] float duration;
        [Export] Vector3 direction;

        public override void Tick()
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