using Electronova.Generic;
using Godot;
using Godot.Collections;
using System;

namespace Electronova.Actors
{
    [Tool]
    public partial class ActorInput : Node
    {
        [Export] InputState inputState;

        [Export] Array<StringName> stringNames;
        [Export] Array<BoolNode> boolNodes;
        [Export] float bufferTimer = 0.1f;

        Array<float> currentBuffer = new();

        public override void _Ready()
        {
            currentBuffer.Resize(Math.Min(stringNames.Count, boolNodes.Count));
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

            int loopMax = Math.Min(stringNames.Count, boolNodes.Count);

            for (int i = 0; i < loopMax; i++)
            {
                if (Input.IsActionJustPressed(stringNames[i]))
                {
                    boolNodes[i].Value = true;
                    currentBuffer[i] = bufferTimer;
                }
                else
                {
                    if (currentBuffer[i] <= 0f)
                    {
                        boolNodes[i].Value = false;
                    }
                    else
                    {
                        currentBuffer[i] -= (float)delta;
                    }
                }
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