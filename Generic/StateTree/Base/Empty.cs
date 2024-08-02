using Godot;
using System;

namespace Electronova.Generic
{
    [Tool]
    public partial class Empty : Node, IStateTree
    {
        [ExportCategory("State Tree")]
        [Export] StringName emptyState = null;

        public virtual StringName State => emptyState;

        public virtual void Tick()
        {
            return;
        }
    }
}