using Godot;
using System;

namespace Electronova.Generic
{
	[GlobalClass, Icon("res://addons/Electronova/Icons/Generic/VariableNode.png")]
	public partial class IntNode : Variable
	{
		public int Value { get; set; }
	}
}