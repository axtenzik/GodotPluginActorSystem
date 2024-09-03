using Electronova.Generic;
using Godot;
using System;
using System.Linq;

namespace Electronova.Actors
{
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Selector.png")]
    public partial class DirectionSelector : StateTree
    {
        [ExportCategory("Direction")]
        [Export] Vector2Node direction;
        [Export] bool defaultFirstChild = true;

        public override void Tick()
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

            StateTree selectedChild = null;
            foreach (StateTree child in GetChildren().Cast<StateTree>())
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
            }
            else if (defaultFirstChild)
            {
                selectedChild = (StateTree)GetChild(0);
                selectedChild?.Tick();
            }
        }
    }
}