using Godot;
using System;

namespace Electronova.Generic
{
    interface IStateTree
    {
        public StringName State { get; }

        public void Tick();
    }
}