using Godot;
using System;

namespace Electronova.Generic
{
    [Tool]
    public partial class Rotator : Node, IStateTree
    {
        [ExportCategory("State Tree")]
        [Export] StringName rotatorState = null;

        public virtual StringName State => rotatorState;

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