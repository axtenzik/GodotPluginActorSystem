using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    public partial class StateTreeRoot : Node, IUtilityRoot
    {
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
            IStateTree selectedChild = (IStateTree)GetChild(0);
            selectedChild?.Tick();
        }
    }
}