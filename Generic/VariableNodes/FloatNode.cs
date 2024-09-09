using Godot;
using System;

namespace Electronova.Generic
{
	[Tool]
	[GlobalClass, Icon("res://addons/Electronova/Icons/Generic/VariableNode.png")]
	public partial class FloatNode : Variable
	{
		public float Value { get; set; }
	}
}