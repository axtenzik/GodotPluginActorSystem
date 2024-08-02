using Godot;
using System;

namespace Electronova.Generic
{
    [Tool]
    public partial class StateString : Node
    {
        [Export] public StringName State { get; set; }
    }
}