using Godot;
using System;

namespace Electronova.Generic
{
	[GlobalClass, Icon("res://addons/Electronova/Icons/Generic/VariableNode.png")]
	public partial class BoolNode : Variable
	{
		public bool Value { get; set; }
	}
}