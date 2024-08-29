using Godot;
using System;

namespace Electronova.Generic
{
	[Tool]
	public partial class Vector3Node : Node
	{
		[Export] public Vector3 Value { get; set; }
	}
}