using Godot;
using System;

namespace Electronova.Generic
{
    [Tool]
    public partial class Sequencer : Node, IStateTree
    {
        [ExportCategory("State Tree")]
        [Export] StringName sequencerState = null;

        public virtual StringName State => sequencerState;

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
            for (int i = 0; i < GetChildCount(); i++)
            {
                IStateTree selectedChild = (IStateTree)GetChild(i);
                selectedChild?.Tick();
            }
        }
    }
}