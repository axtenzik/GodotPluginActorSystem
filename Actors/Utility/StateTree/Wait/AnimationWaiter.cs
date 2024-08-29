using Electronova.Generic;
using Godot;
using System;
using System.Linq;

namespace Electronova.Actors
{
    [Tool]
    public partial class AnimationWaiter : Node, IStateTree
    {
        [ExportCategory("State Tree")]
        [Export] StateString ChildStateType { get; set; }

        [ExportCategory("AnimationWaiter")]
        [Export] StringName desiredAnimation = null;
        [Export] StringName animationToWait = null;
        [Export] AnimationPlayer Player { get; set; }

        public StringName State => Name;

        public void Tick()
        {
            if (Player.CurrentAnimation == animationToWait)
            {
                return;
            }
            else if (Player.CurrentAnimation != desiredAnimation)
            {
                Player.Play(desiredAnimation);
            }

            if (GetChildCount() == 0)
            {
                return;
            }

            if (GetChildCount() == 0)
            {
                return;
            }

            IStateTree selectedChild = (IStateTree)GetChild(0);
            selectedChild?.Tick();
        }
    }
}