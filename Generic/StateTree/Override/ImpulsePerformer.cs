using Electronova.Actors;
using Godot;
using System;

namespace Electronova.Generic
{
    [Tool]
    public partial class ImpulsePerformer : Performer
    {
        [ExportCategory("Actor")]
        [Export] Actor Parent;
        [Export] ActorUpdate actorUpdate;
        
        [ExportCategory("Impulse")]
        [Export] float speed;
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