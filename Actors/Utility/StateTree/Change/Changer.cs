using Electronova.Generic;
using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    public partial class Changer : Node, IStateTree
    {
        [ExportCategory("State Tree")]
        [Export] protected StringName changerState = null;

        [ExportCategory("Changer")]
        [Export] protected StringName desiredState = null;
        [Export] protected StateString StateToChange { get; set; }

        [ExportCategory("Animation")]
        [Export] protected StringName desiredAnimation = null;
        [Export] protected AnimationPlayer Player { get; set; }

        public StringName State => changerState;

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
            Player?.Play(desiredAnimation);
            StateToChange.State = desiredState;
        }
    }
}