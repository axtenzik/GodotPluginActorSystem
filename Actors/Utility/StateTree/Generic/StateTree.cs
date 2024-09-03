using Godot;
using System;

namespace Electronova.Actors
{
    [GlobalClass, Icon("res://addons/Electronova/Icons/Actor/ActorContacts.png")]
    public partial class StateTree : ActorController
    {
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

        public override void Start()
        {
            Tick();
        }

        public virtual void Tick()
        {
            //If node has any children will try continuing down the tree through its first child.
            if (GetChildCount() == 0)
            {
                return;
            }
            StateTree selectedChild = (StateTree)GetChild(0);
            selectedChild?.Tick();
        }
    }
}