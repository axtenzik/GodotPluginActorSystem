using Godot;
using System;

namespace Electronova.Generic
{
    [Tool]
    public partial class Performer : Node, IStateTree
    {
        [ExportCategory("State Tree")]
        [Export] StringName performerState = null;

        public StringName State => performerState;

        public virtual void Tick()
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