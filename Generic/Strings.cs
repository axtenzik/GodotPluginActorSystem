using Godot;
using System;

namespace Electronova.Generic
{
    /// <summary>
    /// Class of StringNames for other classes to use.
    /// Only necessary because Godot doesn't use c# strings and conversions cause performance hits apparently.
    /// </summary>
    public readonly struct Strings
    {
        //General
        public static readonly StringName None = new("None");
        public static readonly StringName Up = new("Up");
        public static readonly StringName Down = new("Down");
        public static readonly StringName Right = new("Right");
        public static readonly StringName Left = new("Left");
        public static readonly StringName Aim = new("Aim");
        public static readonly StringName Move = new("Move");

        //Input
        public static readonly StringName MoveForward = new("MoveForward");
        public static readonly StringName MoveBackward = new("MoveBackward");
        public static readonly StringName MoveLeft = new("MoveLeft");
        public static readonly StringName MoveRight = new("MoveRight");

        public static readonly StringName AimUp = new("AimUp");
        public static readonly StringName AimDown = new("AimDown");
        public static readonly StringName AimLeft = new("AimLeft");
        public static readonly StringName AimRight = new("AimRight");

        public static readonly StringName FlickUp = new("FlickUp");
        public static readonly StringName FlickDown = new("FlickDown");
        public static readonly StringName FlickLeft = new("FlickLeft");
        public static readonly StringName FlickRight = new("FlickRight");

        public static readonly StringName Click = new("Click");
        public static readonly StringName Escape = new("Escape");

        public static readonly StringName Jump = new("Jump");
        public static readonly StringName Crouch = new("Crouch");
        public static readonly StringName Run = new("Run");

        //Target Lock
        public static readonly StringName Locked = new("Locked");
        public static readonly StringName Unlocked = new("Unlocked");

        //Fluid
        public static readonly StringName InSpace = new("InSpace");
        public static readonly StringName InAir = new("InAir");
        public static readonly StringName InWater = new("InWater");

        //Contacts
        public static readonly StringName OnGround = new("OnGround");
        public static readonly StringName OnSteep = new("OnSteep");
        public static readonly StringName OnWall = new("OnWall");
        public static readonly StringName OnCeiling = new("OnCeiling");

        //Actions
        public static readonly StringName Standing = new("Standing");
        public static readonly StringName Walking = new("Walking");
        public static readonly StringName Running = new("Running");
        public static readonly StringName Crouching = new("Crouching");
        public static readonly StringName Rolling = new("Running");
        public static readonly StringName Jumping = new("Jumping");
        public static readonly StringName RollJumping = new("RollJumping");
        public static readonly StringName Diving = new("Diving");
    }
}