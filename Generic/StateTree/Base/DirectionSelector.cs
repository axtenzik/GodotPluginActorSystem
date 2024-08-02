using Electronova.Actors;
using Godot;
using System;
using System.Linq;

namespace Electronova.Generic
{
    [Tool]
    public partial class DirectionSelector : Node, IStateTree
    {
        [ExportCategory("Direction")]
        [Export] InputState inputState;

        [ExportCategory("State Tree")]
        [Export] StringName selectorState = null;

        public StringName State => selectorState;

        public void Tick()
        {
            if (GetChildCount() == 0)
            {
                return;
            }

            StringName moveState = Strings.None;

            if (inputState.Move.LengthSquared() > 0.001f)
            {
                float angle = Mathf.RadToDeg(inputState.Move.AngleTo(Vector2.Up));

                if (angle <= -135f || angle >= 135)
                {
                    moveState = Strings.Down;
                }
                else if (angle <= -45f)
                {
                    moveState = Strings.Right;
                }
                else if (angle >= 45f)
                {
                    moveState = Strings.Left;
                }
                else
                {
                    moveState = Strings.Up;
                }
            }

            IStateTree selectedChild = null;
            foreach (IStateTree child in GetChildren().Cast<IStateTree>())
            {
                if (child.State == moveState)
                {
                    selectedChild = child;
                    break;
                }
            }

            if (selectedChild != null)
            {
                selectedChild.Tick();
                return;
            }
            else
            {
                //if no valid children, use first one
                selectedChild = (IStateTree)GetChild(0);
                selectedChild?.Tick();
            }
        }
    }
}