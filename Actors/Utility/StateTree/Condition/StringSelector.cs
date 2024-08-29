using Electronova.Generic;
using Godot;
using System;
using System.Linq;

namespace Electronova.Actors
{
    [Tool]
    public partial class StringSelector : Node, IStateTree
    {
        [Export] StringNode ChildStateType { get; set; }
        [Export] bool defaultFirstChild = true;

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

            foreach (IStateTree child in GetChildren().Cast<IStateTree>())
            {
                if (child.State == ChildStateType.Value)
                {
                    child.Tick();
                    return;
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