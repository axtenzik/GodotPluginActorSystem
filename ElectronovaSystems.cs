#if TOOLS
using Godot;
using System;

namespace Electronova
{
    [Tool]
    public partial class ElectronovaSystems : EditorPlugin
    {
	    public override void _EnterTree()
	    {
		    // Initialization of the plugin goes here.
            ActorAdd();
            GenericAdd();
            StateTreeAdd();
            WorldAdd();
	    }

	    public override void _ExitTree()
	    {
		    // Clean-up of the plugin goes here.
            ActorRemove();
            GenericRemove();
            StateTreeRemove();
            WorldRemove();
	    }

        private void ActorAdd()
        {
            Script script;
            Texture2D texture;

            script = GD.Load<Script>("res://addons/Electronova/Actor/Actor.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Actor/Actor.png");
            AddCustomType("Rb3DActor", "RigidBody3D", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actor/ActorInput.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Actor/ActorInput.png");
            AddCustomType("ActorInput", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actor/ActorUpdate.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Actor/ActorUpdate.png");
            AddCustomType("ActorUpdate", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actor/JumpProperties.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Actor/ActorStats.png");
            AddCustomType("JumpProperties", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actor/JumpStats.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Actor/ActorStats.png");
            AddCustomType("JumpStats", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actor/MovementProperties.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Actor/ActorStats.png");
            AddCustomType("MovementProperties", "Node", script, texture);
        }

        private void ActorRemove()
        {
            RemoveCustomType("Rb3DActor");
            RemoveCustomType("ActorInput");
            RemoveCustomType("ActorUpdate");
            RemoveCustomType("JumpProperties");
            RemoveCustomType("JumpStats");
            RemoveCustomType("MovementProperties");
        }

        private void GenericAdd()
        {
            Script script = GD.Load<Script>("res://addons/Electronova/Generic/StateString.cs");
            Texture2D texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateString.png");
            AddCustomType("StateString", "Node", script, texture);
        }

        private void GenericRemove()
        {
            RemoveCustomType("StateString");
        }

        private void StateTreeAdd()
        {
            Script script;
            Texture2D texture;

            script = GD.Load<Script>("res://addons/Electronova/Generic/StateTree/Base/AnimationWaiter.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Waiter.png");
            AddCustomType("AnimationWaiter", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Generic/StateTree/Base/Changer.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Changer.png");
            AddCustomType("Changer", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Generic/StateTree/Base/DirectionSelector.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/DirectionSelector.png");
            AddCustomType("DirectionSelector", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Generic/StateTree/Base/Empty.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Empty.png");
            AddCustomType("Empty", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Generic/StateTree/Base/Performer.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Performer.png");
            AddCustomType("Performer", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Generic/StateTree/Base/Rotator.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Rotator.png");
            AddCustomType("Rotator", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Generic/StateTree/Base/Sequencer.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Sequencer.png");
            AddCustomType("Sequencer", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Generic/StateTree/Base/StateSelector.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Selector.png");
            AddCustomType("StateSelector", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Generic/StateTree/Base/StepWaiter.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Counter.png");
            AddCustomType("StepWaiter", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Generic/StateTree/Base/TimeWaiter.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Waiter.png");
            AddCustomType("TimeWaiter", "Node", script, texture);
        }

        private void StateTreeRemove()
        {
            RemoveCustomType("AnimationWaiter");
            RemoveCustomType("Changer");
            RemoveCustomType("DirectionSelector");
            RemoveCustomType("Empty");
            RemoveCustomType("Performer");
            RemoveCustomType("Rotator");
            RemoveCustomType("Sequencer");
            RemoveCustomType("StateSelector");
            RemoveCustomType("StepWaiter");
            RemoveCustomType("TimeWaiter");
        }

        private void WorldAdd()
        {
            Script script = GD.Load<Script>("res://addons/Electronova/World/GravitySource.cs");
            Texture2D texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/World/Gravity.png");
            AddCustomType("GravitySource", "Node3D", script, texture);
        }

        private void WorldRemove()
        {
            RemoveCustomType("GravitySource");
        }
    }
}
#endif