using Godot;
using System;

namespace Electronova.Generic
{
	[Tool]
	public partial class BoolNode : Node
	{
		[Export] public bool Value { get; set; }
	}
}