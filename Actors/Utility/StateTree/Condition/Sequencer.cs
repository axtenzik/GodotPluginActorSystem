using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Sequencer.png")]
    public partial class Sequencer : StateTree
    {
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