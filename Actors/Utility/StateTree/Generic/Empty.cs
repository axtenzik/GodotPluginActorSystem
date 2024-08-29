using Godot;
using System;

namespace Electronova.Actors
{
    [Tool]
    public partial class Empty : Node, IStateTree
    {
        public virtual StringName State => Name;

        public virtual void Tick()
        {
            return;
        }
    }
}