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

            script = GD.Load<Script>("res://addons/Electronova/Actors/Actor.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Actor/Actor.png");
            AddCustomType("Actor", "RigidBody3D", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Components/ActorInput.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Actor/ActorInput.png");
            AddCustomType("ActorInput", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Components/ActorContacts.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Actor/ActorContacts.png");
            AddCustomType("ActorContacts", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Properties/JumpProperties.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Actor/ActorStats.png");
            AddCustomType("JumpProperties", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Stats/JumpStats.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Actor/ActorStats.png");
            AddCustomType("JumpStats", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Properties/MovementProperties.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Actor/ActorStats.png");
            AddCustomType("MovementProperties", "Node", script, texture);
        }

        private void ActorRemove()
        {
            RemoveCustomType("Actor");
            RemoveCustomType("ActorInput");
            RemoveCustomType("ActorContacts");
            RemoveCustomType("JumpProperties");
            RemoveCustomType("JumpStats");
            RemoveCustomType("MovementProperties");
        }

        private void GenericAdd()
        {
            Script script;
            Texture2D texture;

            script = GD.Load<Script>("res://addons/Electronova/Generic/StateString.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateString.png");
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

            script = GD.Load<Script>("res://addons/Electronova/Actors/Utility/StateTree/Rotate/AimRotator.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Rotator.png");
            AddCustomType("AimRotator", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Utility/StateTree/Wait/AnimationWaiter.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Waiter.png");
            AddCustomType("AnimationWaiter", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Utility/StateTree/Change/Changer.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Changer.png");
            AddCustomType("Changer", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Utility/StateTree/Condition/DirectionSelector.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/DirectionSelector.png");
            AddCustomType("DirectionSelector", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Utility/StateTree/Generic/Empty.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Empty.png");
            AddCustomType("Empty", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Utility/StateTree/Move/ImpulsePerformer.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Performer.png");
            AddCustomType("ImpulsePerformer", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Utility/StateTree/Change/JumpChanger.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Changer.png");
            AddCustomType("JumpChanger", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Utility/StateTree/Move/JumpPerformer.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Performer.png");
            AddCustomType("JumpPerformer", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Utility/StateTree/Move/MotionPerformer.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Performer.png");
            AddCustomType("MotionPerformer", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Utility/StateTree/Move/MovementPerformer.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Performer.png");
            AddCustomType("MovementPerformer", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Utility/StateTree/Rotate/MoveRotator.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Rotator.png");
            AddCustomType("Rotator", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Utility/StateTree/Condition/Sequencer.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Sequencer.png");
            AddCustomType("Sequencer", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Utility/StateTree/Condition/StringSelector.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Selector.png");
            AddCustomType("StringSelector", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Utility/StateTree/Wait/StepWaiter.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Counter.png");
            AddCustomType("StepWaiter", "Node", script, texture);

            script = GD.Load<Script>("res://addons/Electronova/Actors/Utility/StateTree/Wait/TimeWaiter.cs");
            texture = GD.Load<Texture2D>("res://addons/Electronova/Icons/Generic/StateTree/Waiter.png");
            AddCustomType("TimeWaiter", "Node", script, texture);
        }

        private void StateTreeRemove()
        {
            RemoveCustomType("AimRotator");
            RemoveCustomType("AnimationWaiter");
            RemoveCustomType("Changer");
            RemoveCustomType("DirectionSelector");
            RemoveCustomType("Empty");
            RemoveCustomType("ImpulsePerformer");
            RemoveCustomType("JumpChanger");
            RemoveCustomType("JumpPerformer");
            RemoveCustomType("MotionPerformer");
            RemoveCustomType("MovementPerformer");
            RemoveCustomType("MoveRotator");
            RemoveCustomType("Sequencer");
            RemoveCustomType("StringSelector");
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