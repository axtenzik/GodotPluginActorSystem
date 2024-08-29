using Electronova.Generic;
using Godot;
using Godot.Collections;

namespace Electronova.Actors
{
    [Tool]
    public partial class ActorInput : Node
    {
        [Export] Vector2Node move;
        [Export] Vector2Node aim;
        [Export] Array<BoolNode> boolNodes;
        [Export] float bufferTimer = 0.1f;

        Array<float> currentBuffer = new();

        public override void _Ready()
        {
            currentBuffer.Resize(boolNodes.Count);
        }

        public override void _Process(double delta)
        {
            if (Engine.IsEditorHint())
            {
                return;
            }

            //ClearBuffer();

            move.Value = Input.GetVector(Strings.MoveLeft, Strings.MoveRight, Strings.MoveForward, Strings.MoveBackward);
            aim.Value = Input.GetVector(Strings.AimLeft, Strings.AimRight, Strings.AimDown, Strings.AimUp);

            /*if (Input.IsActionJustPressed(Strings.Jump))
            {
                inputState.State = Strings.Jump;
            }*/

            for (int i = 0; i < boolNodes.Count; i++)
            {
                if (Input.IsActionPressed(boolNodes[i].Name))
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
            aim.Value = Vector2.Zero;
            move.Value = Vector2.Zero;
        }
    }
}