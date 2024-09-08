using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Empty.png")]
    public partial class Empty : StateTree
    {
        public override string[] _GetConfigurationWarnings()
        {
            return Array.Empty<string>();
        }
        
        public override void Tick()
        {
            return;
        }
    }
}