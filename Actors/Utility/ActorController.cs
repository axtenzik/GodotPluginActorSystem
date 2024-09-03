using Godot;
using System;

namespace Electronova.Actors
{
	[GlobalClass, Icon("res://addons/Electronova/Icons/Actor/Actor.png")]
	public abstract partial class ActorController : Node
	{
		public abstract void Start();
	}
}