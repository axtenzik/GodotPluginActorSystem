using Electronova.Generic;
using Godot;
using System;
using System.Collections.Generic;

namespace Electronova.Actors
{
    [Tool]
    public partial class StepWaiter : Node, IStateTree
    {
        [Export(PropertyHint.Range, "0, 100")]
        public int MaxCount
        {
            get => maxCount;
            private set
            {
                maxCount = value;
                UpdateConfigurationWarnings();
            }
        }

        [Export] StateString resetCheck;

        public StringName State => Name;

        private StringName before;
        private int maxCount;
        private int currentCount;

        public override string[] _GetConfigurationWarnings()
        {
            List<string> list = new();
            if (maxCount == 0)
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

            if (currentCount < maxCount)
            {
                currentCount++;

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

                currentCount = 0;
            }
        }
    }
}