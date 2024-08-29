using Godot;
using System;

namespace Electronova.Generic
{
	[Tool]
	public partial class FloatNode : Node
	{
		[Export] public float Value { get; set; }
	}
}