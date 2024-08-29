using Electronova.Generic;
using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    public partial class Changer : Node, IStateTree
    {
        [ExportCategory("Changer")]
        [Export] protected StringName desiredState = null;
        [Export] protected StateString StateToChange { get; set; }

        public StringName State => Name;

        public virtual void Tick()
        {
            Change();

            if (GetChildCount() == 0)
            {
                return;
            }
            IStateTree selectedChild = (IStateTree)GetChild(0);
            selectedChild?.Tick();
        }

        private void Change()
        {
            StateToChange.State = desiredState;
        }
    }
}