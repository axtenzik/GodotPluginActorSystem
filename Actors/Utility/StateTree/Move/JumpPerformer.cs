using Electronova.Generic;
using Electronova.World;
using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    public partial class JumpPerformer : Node, IStateTree
    {
        //

        [ExportCategory("Actor")]
        [Export] Actor parent;
        [Export] ActorContacts actorContacts;
        [Export] StringNode actorState;
        [Export] StringNode ContactState;

        [ExportCategory("Jump")]
        [Export] StringName setActorStateTo;
        [Export] JumpProperties jumpProperties;
        [Export] JumpStats jumpStats;

        /*[ExportCategory("Animator")]
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
        
        private AnimationPlayer player;*/

        public StringName State => Name;

        /*public override string[] _GetConfigurationWarnings()
        {
            List<String> list = new();
            if (player == null)
            {
                list.Add("No animation player selected");
            }
            string[] strings = list.ToArray();
            return strings;
        }*/

        public void Tick()
        {
            Jump();
            actorState.Value = setActorStateTo;

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
            if (ContactState.Value == Strings.OnGround)
            {
                jumpStats.ResetJumps();
                jumpDirection = actorContacts.ContactNormal;
            }
            else if (ContactState.Value == Strings.OnSteep)
            {
                jumpStats.ResetJumps();
                jumpDirection = actorContacts.SteepNormal;
            }
            else if (jumpProperties.MaxAirJumps > 0 && jumpStats.CurrentJump <= jumpProperties.MaxAirJumps)
            {
                if (jumpStats.CurrentJump == 0)
                {
                    jumpStats.AddJump();
                }

                jumpDirection = actorContacts.ContactNormal;
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
        }
    }
}