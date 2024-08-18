using Godot;
using System;

namespace Electronova.Actors
{
    interface IStateTree
    {
        public StringName State { get; }

        public void Tick();
    }
}