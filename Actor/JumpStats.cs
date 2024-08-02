using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    public partial class JumpStats : Node
    {
        public int CurrentJump { get; private set; }
        public int StepsSinceLastGrounded { get; private set; } 
        public int StepsSinceLastJump { get; private set; }

        public void AddGroundedStep()
        {
            StepsSinceLastGrounded++;
        }

        public void AddJump()
        {
            CurrentJump++;
        }

        public void AddJumpStep()
        {
            StepsSinceLastJump++;
        }

        public void DisableJump()
        {
            CurrentJump = -1;
        }

        public void ResetGroundedSteps()
        {
            StepsSinceLastGrounded = 0;
        }

        public void ResetJumps()
        {
            CurrentJump = 0;
        }

        public void ResetJumpSteps()
        {
            StepsSinceLastJump = 0;
        }
    }
}