using Electronova.Generic;
using Godot;
using System;
using System.Linq;

namespace Electronova.Actors
{
    [Tool]
    public partial class StateSelector : Node, IStateTree
    {
        [ExportCategory("State Tree")]
        [Export] StringName selectorState = null;
        [Export] StateString ChildStateType { get; set; }

        public StringName State => selectorState;

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

            //GD.Print("");
            //GD.Print(selectorState + " Selector state");
            //GD.Print("Looking for: " + ChildStateType.State);

            IStateTree selectedChild = null;
            foreach (IStateTree child in GetChildren().Cast<IStateTree>())
            {
                //GD.Print("Looked at: " + child.State);
                if (child.State == ChildStateType.State)
                {
                    //GD.Print(child.State + " Selected");
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
                //GD.Print("No suitable child");
                //if no valid children, use first one
                selectedChild = (IStateTree)GetChild(0);
                selectedChild?.Tick();
            }
        }
    }
}