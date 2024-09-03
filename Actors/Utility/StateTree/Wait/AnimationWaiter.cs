using Electronova.Generic;
using Godot;
using System;
using System.Linq;

namespace Electronova.Actors
{
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Waiter.png")]
    public partial class AnimationWaiter : StateTree
    {
        [ExportCategory("AnimationWaiter")]
        [Export] StringName desiredAnimation = null;
        [Export] StringName animationToWait = null;
        [Export] AnimationPlayer Player { get; set; }

        public override void Tick()
        {
            if (Player.CurrentAnimation == animationToWait)
            {
                return;
            }
            else if (Player.CurrentAnimation != desiredAnimation)
            {
                Player.Play(desiredAnimation);
            }

            base.Tick();
        }
    }
}