using Godot;
using System;

namespace Electronova.Actors
{
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Empty.png")]
    public partial class Empty : StateTree
    {
        public override void Tick()
        {
            return;
        }
    }
}