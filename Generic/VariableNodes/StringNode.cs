using Godot;
using System;

namespace Electronova.Generic
{
    [Tool]
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/VariableNode.png")]
    public partial class StringNode : Variable
    {
        public StringName Value { get; set; }
    }
}