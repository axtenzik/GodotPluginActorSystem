using Godot;
using System;

namespace Electronova.Actors
{
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Performer.png")]
    public partial class ImpulsePerformer : StateTree
    {
        [ExportCategory("Actor")]
        [Export] Actor Parent;
        [Export] ActorContacts actorContacts;
        
        [ExportCategory("Impulse")]
        [Export] float speed;
        [Export] Vector3 direction;

        public override void Tick()
        {
            if (GetChildCount() == 0)
            {
                return;
            }
            
            StateTree selectedChild = (StateTree)GetChild(0);
            selectedChild?.Tick();
        }
    }
}