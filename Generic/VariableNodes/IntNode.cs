using Godot;
using System;

namespace Electronova.Generic
{
	[Tool]
	public partial class IntNode : Node
	{
		[Export] public int Value { get; set; }
	}
}