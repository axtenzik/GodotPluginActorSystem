using Electronova.Generic;
using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Changer.png")]
    public partial class Changer : StateTree
    {
        [ExportCategory("Changer")]
        [Export] protected StringName desiredState = null;
        [Export] protected StringNode StateToChange { get; set; }

        public override string[] _GetConfigurationWarnings()
        {
            return Array.Empty<string>();
        }

        public override void Tick()
        {
            StateToChange.Value = desiredState;

            base.Tick();
        }
    }
}