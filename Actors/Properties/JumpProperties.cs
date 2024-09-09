using Godot;
using System;

namespace Electronova.Actors
{
	[Tool]
    [GlobalClass, Icon("res://addons/Electronova/Icons/Actor/ActorStats.png")]
    public partial class JumpProperties : Node
    {
        [Export(PropertyHint.Range, "0, 1")] public float CoyoteTime { get; set; }
        
        //maybe distance and speed over height?
        [Export(PropertyHint.Range, "0, 10")] public float Height { get; private set; }
        [Export(PropertyHint.Range, "0, 5")] public int MaxAirJumps { get; private set; }
    }
}