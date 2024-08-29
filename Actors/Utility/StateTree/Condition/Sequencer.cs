using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    public partial class Sequencer : Node, IStateTree
    {
        public virtual StringName State => Name;

        public override string[] _GetConfigurationWarnings()
        {
            if (GetChildCount() == 0)
            {
                string[] strings = { "End of State Tree path. Try adding State Tree nodes as children to add functionality!" };
                return strings;
            }

            return Array.Empty<string>();
        }

        public virtual void Tick()
        {
            IStateTree selectedChild = null; //VSCode don't like this here for some reason, "Unnecessary" apparently
            for (int i = 0; i < GetChildCount(); i++)
            {
                selectedChild = (IStateTree)GetChild(i);
                selectedChild?.Tick();
            }
        }
    }
}