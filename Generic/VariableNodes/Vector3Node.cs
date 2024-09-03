using Godot;
using System;

namespace Electronova.Generic
{
	[GlobalClass, Icon("res://addons/Electronova/Icons/Generic/VariableNode.png")]
	public partial class Vector3Node : Variable
	{
		public Vector3 Value { get; set; }
	}
}