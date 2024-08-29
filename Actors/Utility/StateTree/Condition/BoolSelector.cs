using Electronova.Generic;
using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    public partial class BoolSelector : Node, IStateTree
    {
        [Export] BoolNode ChildBool { get; set; }

        public StringName State => Name;

        public override string[] _GetConfigurationWarnings()
        {
            if (GetChildCount() == 0)
            {
                string[] strings = { "End of State Tree path. Try adding State Tree nodes as children to add functionality!" };
                return strings;
            }

            return Array.Empty<string>();
        }

        public void Tick()
        {
            if (GetChildCount() == 0)
            {
                return;
            }

            IStateTree selectedChild = null;
            if (ChildBool.Value)
            {
                if (GetChildCount() == 0)
                {
                    return;
                }

                selectedChild = (IStateTree)GetChild(0);
            }
            else
            {
                if (GetChildCount() < 2)
                {
                    return;
                }

                selectedChild = (IStateTree)GetChild(1);
            }

            selectedChild?.Tick();
        }
    }
}