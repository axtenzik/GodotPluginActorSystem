using Godot;
using System;

namespace Electronova.Generic
{
	[Tool]
	public partial class Vector2Node : Node
	{
		[Export] public Vector2 Value { get; set; }
	}
}