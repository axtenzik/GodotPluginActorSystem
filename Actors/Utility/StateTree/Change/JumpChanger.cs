using Electronova.Generic;
using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    public partial class JumpChanger : Node, IStateTree
    {
        [ExportCategory("JumpChanger")]
        [Export] JumpStats jumpStats;

        [ExportCategory("Changer")]
        [Export] StringName desiredState = null;
        [Export] StringNode StateToChange { get; set; }

        public StringName State => Name;

        public override string[] _GetConfigurationWarnings()
        {
            string[] strings = { "Can be changed out for IntNodes and Changer", "VariableNodes are now my Properties and Stats" };
            return strings;
        }

        public void Tick()
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
            if (jumpStats.StepsSinceLastJump > 1)
            {
                StateToChange.Value = desiredState;
            }
        }
    }
}