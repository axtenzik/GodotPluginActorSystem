using Electronova.Generic;
using Godot;
using System;
using System.Collections.Generic;

namespace Electronova.Actors
{
    [Tool]
    public partial class TimeWaiter : Node, IStateTree
    {
        [ExportCategory("TimeWaiter")]
        [Export] Actor parent;
        [Export] StateString resetCheck;

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

        [ExportCategory("State Tree")]
        [Export] StringName counterState = null;

        public StringName State => counterState;

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

        public void Tick()
        {
            before = resetCheck.State;

            if (currentCount < maxTime)
            {
                currentCount += parent.DeltaStep;

                if (GetChildCount() < 2)
                {
                    return;
                }

                IStateTree selectedChild = (IStateTree)GetChild(1);
                selectedChild?.Tick();
            }
            else
            {
                if (GetChildCount() == 0)
                {
                    return;
                }

                IStateTree selectedChild = (IStateTree)GetChild(0);
                selectedChild?.Tick();

                if (before == resetCheck.State)
                {
                    return;
                }

                currentCount = 0f;
            }
        }
    }
}