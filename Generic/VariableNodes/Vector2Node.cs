using Godot;
using System;

namespace Electronova.Generic
{
	[GlobalClass, Icon("res://addons/Electronova/Icons/Generic/VariableNode.png")]
	public partial class Vector2Node : Variable
	{
		public Vector2 Value { get; set; }
	}
}