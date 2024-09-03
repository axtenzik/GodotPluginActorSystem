using Electronova.Generic;
using Godot;
using Godot.Collections;
using System;
using System.Linq;

namespace Electronova.Actors
{
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Selector.png")]
    public partial class InputSelector : StateTree
    {
        [Export] Array<BoolNode> boolNodes;
        [Export] bool defaultFirstChild = true;

        public override string[] _GetConfigurationWarnings()
        {
            if (GetChildCount() == 0)
            {
                string[] strings = { "End of State Tree path. Try adding State Tree nodes as children to add functionality!" };
                return strings;
            }

            return System.Array.Empty<string>();
        }

        public override void Tick()
        {
            if (GetChildCount() == 0)
            {
                return;
            }

            foreach (StateTree child in GetChildren().Cast<StateTree>())
            {
                for (int i = 0; i < boolNodes.Count; i++)
                {
                    if (child != null && boolNodes[i].Value && child.State == boolNodes[i].Name)
                    {
                        child.Tick();
                        return;
                    }
                }
            }

            if (defaultFirstChild)
            {
                StateTree selectedChild = (StateTree)GetChild(0);
                selectedChild?.Tick();
            }
        }
    }
}