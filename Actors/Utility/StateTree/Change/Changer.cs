using Electronova.Generic;
using Godot;
using System;

namespace Electronova.Actors
{
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Changer.png")]
    public partial class Changer : StateTree
    {
        [ExportCategory("Changer")]
        [Export] protected StringName desiredState = null;
        [Export] protected StringNode StateToChange { get; set; }

        public override void Tick()
        {
            Change();

            base.Tick();
        }

        private void Change()
        {
            StateToChange.Value = desiredState;
        }
    }
}