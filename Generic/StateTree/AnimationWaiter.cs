using Godot;
using System;
using System.Linq;

namespace Electronova.Generic
{
    [Tool]
    public partial class AnimationWaiter : Node, IStateTree
    {
        [ExportCategory("State Tree")]
        [Export] StringName waiterState = null;
        [Export] StateString ChildStateType { get; set; }

        [ExportCategory("AnimationWaiter")]
        [Export] StringName desiredAnimation = null;
        [Export] StringName animationToWait = null;
        [Export] AnimationPlayer Player { get; set; }

        public StringName State => waiterState;

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
            
            IStateTree selectedChild = null;
            foreach (IStateTree child in GetChildren().Cast<IStateTree>())
            {
                if (child.State == ChildStateType.State)
                {
                    selectedChild = child;
                    break;
                }
            }

            if (selectedChild != null)
            {
                selectedChild.Tick();
                return;
            }
            else
            {
                //if no valid children, use first one
                selectedChild = (IStateTree)GetChild(0);
                selectedChild?.Tick();
            }
        }
    }
}