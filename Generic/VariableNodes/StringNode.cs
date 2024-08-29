using Godot;
using System;

namespace Electronova.Generic
{
    [Tool]
    public partial class StringNode : Node
    {
        public StringName Value { get; set; }
    }
}