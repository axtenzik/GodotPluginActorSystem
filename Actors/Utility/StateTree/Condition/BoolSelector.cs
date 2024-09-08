using Electronova.Generic;
using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Selector.png")]
    public partial class BoolSelector : StateTree
    {
        [Export] BoolNode ChildBool { get; set; }

        public override void Tick()
        {
            if (GetChildCount() == 0)
            {
                return;
            }

            StateTree selectedChild = null;
            if (ChildBool.Value)
            {
                selectedChild = (StateTree)GetChild(0);
            }
            else
            {
                if (GetChildCount() < 2)
                {
                    return;
                }

                selectedChild = (StateTree)GetChild(1);
            }

            selectedChild?.Tick();
        }
    }
}