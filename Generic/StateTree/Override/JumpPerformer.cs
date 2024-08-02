using Electronova.Actors;
using Electronova.World;
using Godot;
using System;
using System.Collections.Generic;

namespace Electronova.Generic
{
    [Tool]
    public partial class JumpPerformer : Performer
    {
        private AnimationPlayer player;

        [ExportCategory("Actor")]
        [Export] Actor parent;
        [Export] StateString actorState;
        [Export] StateString ContactState;

        [ExportCategory("Jump")]
        [Export] StringName setActorStateTo;
        [Export] JumpProperties jumpProperties;
        [Export] JumpStats jumpStats;

        [ExportCategory("Animator")]
        [Export] StringName animationName;
        [Export]
        AnimationPlayer Player
        {
            get => player;
            set
            {
                player = value;
                UpdateConfigurationWarnings();
            }
        }

        public override string[] _GetConfigurationWarnings()
        {
            List<String> list = new();
            if (player == null)
            {
                list.Add("No animation player selected");
            }
            string[] strings = list.ToArray();
            return strings;
        }

        public override void Tick()
        {
            Jump();
            actorState.State = setActorStateTo;

            if (GetChildCount() == 0)
            {
                return;
            }
            
            IStateTree selectedChild = (IStateTree)GetChild(0);
            selectedChild?.Tick();
        }

        void Jump()
        {
            Vector3 jumpDirection;
            if (ContactState.State == Strings.OnGround)
            {
                jumpStats.ResetJumps();
                jumpDirection = parent.Updater.ContactNormal;
            }
            else if (ContactState.State == Strings.OnSteep)
            {
                jumpStats.ResetJumps();
                jumpDirection = parent.Updater.SteepNormal;
            }
            else if (jumpProperties.MaxAirJumps > 0 && jumpStats.CurrentJump <= jumpProperties.MaxAirJumps)
            {
                if (jumpStats.CurrentJump == 0)
                {
                    jumpStats.AddJump();
                }

                jumpDirection = parent.Updater.ContactNormal;
            }
            else
            {
                return;
            }

            jumpStats.AddJump();
            jumpStats.ResetJumpSteps();

            float jumpSpeed = Mathf.Sqrt(2f * Gravity.GetGravity(parent.Transform.Origin).Length() * jumpProperties.Height);
            //jumpDirection = (jumpDirection + upAxis).Normalized();
            float alignedSpeed = parent.Velocity.Dot(jumpDirection);

            if (alignedSpeed > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);
            }

            parent.AddVelocity(jumpDirection.Normalized() * jumpSpeed);

            player?.Play(animationName);
        }
    }
}