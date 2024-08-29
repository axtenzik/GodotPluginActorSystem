using Electronova.Generic;
using Godot;
using Godot.Collections;
using System;
using System.Linq;

namespace Electronova.Actors
{
    [Tool]
    public partial class InputSelector : Node, IStateTree
    {
        [Export] Array<BoolNode> boolNodes;
        [Export] bool defaultFirstChild = true;

        public StringName State => Name;

        public override string[] _GetConfigurationWarnings()
        {
            if (GetChildCount() == 0)
            {
                string[] strings = { "End of State Tree path. Try adding State Tree nodes as children to add functionality!" };
                return strings;
            }

            return System.Array.Empty<string>();
        }

        public void Tick()
        {
            if (GetChildCount() == 0)
            {
                return;
            }

            foreach (IStateTree child in GetChildren().Cast<IStateTree>())
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
                IStateTree selectedChild = (IStateTree)GetChild(0);
                selectedChild?.Tick();
            }
        }
    }
}