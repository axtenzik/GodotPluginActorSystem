using Electronova.Actors;
using Godot;
using System;

namespace Electronova.Generic
{
    [Tool]
    public partial class JumpChanger : Changer
    {
        [ExportCategory("JumpChanger")]
        [Export] JumpStats jumpStats;

        public override void Tick()
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