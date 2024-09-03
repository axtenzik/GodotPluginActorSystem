using Godot;
using System;
using System.Collections.Generic;

namespace Electronova.Actors
{
    [GlobalClass, Icon("res://addons/Electronova/Icons/Actor/ActorStats.png")]
    public partial class MovementProperties : Node
    {
        private float maxSpeed;
        private float acceleration;
        private float deceleration;
        
        [Export(PropertyHint.Range, "0, 100")] public float MaxSpeed 
        { 
            get => maxSpeed; 
            private set
            {
                maxSpeed = value;
                UpdateConfigurationWarnings();
            } 
        }
        [Export(PropertyHint.Range, "0, 100")] public float Acceleration
        { 
            get => acceleration; 
            private set
            {
                acceleration = value;
                UpdateConfigurationWarnings();
            } 
        }
        [Export(PropertyHint.Range, "0, 100")] public float Deceleration
        { 
            get => deceleration; 
            private set
            {
                deceleration = value;
                UpdateConfigurationWarnings();
            } 
        }
        [Export(PropertyHint.Range, "-1, 1")] public float DotThreshold { get; private set; }

        public override string[] _GetConfigurationWarnings()
        {
            List<String> list = new();
            if (maxSpeed == 0)
            {
                list.Add("Max Speed is set to zero.");
            }
            if (acceleration == 0)
            {
                list.Add("Acceleration is set to zero.");
            }
            if (deceleration == 0)
            {
                list.Add("Deceleration is set to zero.");
            }
            string[] strings = list.ToArray();
            return strings;
        }
    }
}