using Godot;
using System;

namespace Electronova.Generic
{
    [Tool]
    public partial class InputState : StateString
    {
        public Vector2 Aim { get; set; }
        public Vector2 Move { get; set; }
        public Vector2 BufferedAim { get; set; }
        public Vector2 BufferedMove { get; set; }
    }
}