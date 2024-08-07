using Electronova.Actors;
using Godot;
using System;

namespace Electronova.Generic
{
    [Tool]
    public partial class MotionPerformer : Node, IStateTree
    {
        [ExportCategory("Actor")]
        [Export] Actor Parent;
        [Export] ActorUpdate actorUpdate;

        [ExportCategory("Motion")]
        [Export] float speed;
        [Export] float duration;
        [Export] Vector3 direction;

        [ExportCategory("State Tree")]
        [Export] StringName performerState = null;

        public StringName State => performerState;

        public void Tick()
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