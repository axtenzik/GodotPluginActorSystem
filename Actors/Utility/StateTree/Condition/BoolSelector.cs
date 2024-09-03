using Electronova.Generic;
using Godot;
using System;

namespace Electronova.Actors
{
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Selector.png")]
    public partial class BoolSelector : StateTree
    {
        [Export] BoolNode ChildBool { get; set; }

        public override string[] _GetConfigurationWarnings()
        {
            if (GetChildCount() == 0)
            {
                string[] strings = { "End of State Tree path. Try adding State Tree nodes as children to add functionality!" };
                return strings;
            }

            return Array.Empty<string>();
        }

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