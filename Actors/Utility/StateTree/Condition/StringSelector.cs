using Electronova.Generic;
using Godot;
using System;
using System.Linq;

namespace Electronova.Actors
{
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Selector.png")]
    public partial class StringSelector : StateTree
    {
        [Export] StringNode ChildStateType { get; set; }
        [Export] bool defaultFirstChild = true;

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

            foreach (StateTree child in GetChildren().Cast<StateTree>())
            {
                if (child.State == ChildStateType.Value)
                {
                    child.Tick();
                    return;
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