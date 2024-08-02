using Electronova.Generic;
using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    public partial class ActorInput : Node
    {
        [Export] InputState inputState;

        //public Vector2 CurrentMove { get; private set; }
        //public Vector2 CurrentAim { get; private set; }
        //public Vector2 BufferedMove { get; private set; }
        //public Vector2 BufferedAim { get; private set; }

        public override void _Ready()
        {
            /*BufferedAim = Vector2.Zero;
            BufferedMove = Vector2.Zero;*/
        }

        public override void _Process(double delta)
        {
            if (Engine.IsEditorHint())
            {
                return;
            }

            ClearBuffer();

            inputState.Move = Input.GetVector(Strings.MoveLeft, Strings.MoveRight, Strings.MoveForward, Strings.MoveBackward);
            inputState.Aim = Input.GetVector(Strings.AimLeft, Strings.AimRight, Strings.AimDown, Strings.AimUp);
            
            if (Input.IsActionJustPressed(Strings.Jump))
            {
                inputState.State = Strings.Jump;
            }
        }

        /// <summary>
        /// Clears the buffered inputs
        /// </summary>
        public void ClearBuffer()
        {
            inputState.State = Strings.None;
            inputState.Aim = Vector2.Zero;
            inputState.Move = Vector2.Zero;
        }
    }
}