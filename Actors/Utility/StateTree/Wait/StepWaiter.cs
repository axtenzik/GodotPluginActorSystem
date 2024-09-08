using Electronova.Generic;
using Godot;
using System;
using System.Collections.Generic;

namespace Electronova.Actors
{
    [Tool]
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Waiter.png")]
    public partial class StepWaiter : StateTree
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

        [Export] StringNode resetCheck;

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

        public override void Tick()
        {
            before = resetCheck.Value;

            if (currentCount < maxCount)
            {
                currentCount++;

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

                currentCount = 0;
            }
        }
    }
}