using Electronova.Generic;
using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    [GlobalClass,Icon("res://addons/Electronova/Icons/Generic/StateTree/Changer.png")]
    public partial class JumpChanger : StateTree
    {
        [ExportCategory("JumpChanger")]
        [Export] JumpStats jumpStats;

        [ExportCategory("Changer")]
        [Export] StringName desiredState = null;
        [Export] StringNode StateToChange { get; set; }

        public override string[] _GetConfigurationWarnings()
        {
            string[] strings = { "Can be changed out for IntNodes and Changer", "VariableNodes are now my Properties and Stats" };
            return strings;
        }

        public override void Tick()
        {
            if (jumpStats.StepsSinceLastJump > 1)
            {
                StateToChange.Value = desiredState;
            }

            base.Tick();
        }
    }
}