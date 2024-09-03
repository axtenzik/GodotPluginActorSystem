using Electronova.Generic;
using Godot;
using System;
using System.Collections.Generic;

namespace Electronova.Actors
{
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Waiter.png")]
    public partial class TimeWaiter : StateTree
    {
        [ExportCategory("TimeWaiter")]
        [Export] Actor parent;
        [Export] StringNode resetCheck;

        [Export(PropertyHint.Range, "0, 100")]
        public float MaxTime
        {
            get => maxTime;
            private set
            {
                maxTime = value;
                UpdateConfigurationWarnings();
            }
        }

        private StringName before;
        private float maxTime;
        private float currentCount;

        public override string[] _GetConfigurationWarnings()
        {
            List<string> list = new();
            if (maxTime == 0)
            {
                list.Add("Max Count is set to zero.");
            }
            if (GetChildCount() == 0)
            {
                list.Add("End of State Tree path. Try adding State Tree nodes as children to add functionality!");
            }

            string[] strings = list.ToArray();
            return strings;
        }

        public override void Tick()
        {
            before = resetCheck.Value;

            if (currentCount < maxTime)
            {
                currentCount += parent.DeltaStep;

                if (GetChildCount() < 2)
                {
                    return;
                }

                StateTree selectedChild = (StateTree)GetChild(1);
                selectedChild?.Tick();
            }
            else
            {
                if (GetChildCount() == 0)
                {
                    return;
                }

                StateTree selectedChild = (StateTree)GetChild(0);
                selectedChild?.Tick();

                if (before == resetCheck.Value)
                {
                    return;
                }

                currentCount = 0f;
            }
        }
    }
}