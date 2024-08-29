using Electronova.Generic;
using Godot;
using System;
using System.Linq;

namespace Electronova.Actors
{
    [Tool]
    public partial class DirectionSelector : Node, IStateTree
    {
        [ExportCategory("Direction")]
        [Export] Vector2Node direction;
        [Export] bool defaultFirstChild = true;

        public StringName State => Name;

        public void Tick()
        {
            if (GetChildCount() == 0)
            {
                return;
            }

            StringName moveState = Strings.None;

            if (direction.Value.LengthSquared() > 0.001f)
            {
                float angle = Mathf.RadToDeg(direction.Value.AngleTo(Vector2.Up));

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
            else if (defaultFirstChild)
            {
                selectedChild = (IStateTree)GetChild(0);
                selectedChild?.Tick();
            }
        }
    }
}