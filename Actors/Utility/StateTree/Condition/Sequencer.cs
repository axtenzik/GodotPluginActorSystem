using Godot;
using System;

namespace Electronova.Actors
{
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Sequencer.png")]
    public partial class Sequencer : StateTree
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

        public override void Tick()
        {
            StateTree selectedChild = null; //VSCode don't like this here for some reason, "Unnecessary" apparently
            for (int i = 0; i < GetChildCount(); i++)
            {
                selectedChild = (StateTree)GetChild(i);
                selectedChild?.Tick();
            }
        }
    }
}