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

        [ExportCategory("State Tree")]
        [Export] StringName changerState = null;

        [ExportCategory("Changer")]
        [Export] StringName desiredState = null;
        [Export] StateString StateToChange { get; set; }

        [ExportCategory("Animation")]
        [Export] StringName desiredAnimation = null;
        [Export] AnimationPlayer Player { get; set; }

        public StringName State => changerState;

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
            /*if (jumpStats.StepsSinceLastGrounded > 1 || jumpStats.StepsSinceLastJump > 2)
            {
                Player?.Play(desiredAnimation);
                StateToChange.State = desiredState;
            }*/

            if (jumpStats.StepsSinceLastJump > 1)
            {
                StateToChange.State = desiredState;
                Player?.Play(desiredAnimation);
            }
        }
    }
}