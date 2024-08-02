using Godot;
using System;
using System.Collections.Generic;

namespace Electronova.World
{
    public static class Gravity
    {
        static readonly List<GravitySource> sources = new();

        public static Vector3 GetGravity (Vector3 position) 
        {
            Vector3 g = Vector3.Zero;
            if (sources.Count == 0)
            {
                g = (Vector3)ProjectSettings.GetSetting("physics/3d/default_gravity_vector");
                float gMagnitude = (float)ProjectSettings.GetSetting("physics/3d/default_gravity");
                g *= gMagnitude;

                return g;
            }
            
            for (int i = 0; i < sources.Count; i++)
            {
                g += sources[i].GetGravity(position);
            }

            return g;
        }

        public static Vector3 GetGravity (Vector3 position, out Vector3 upAxis)
        {
            Vector3 g = Vector3.Zero;
            if (sources.Count == 0)
            {
                g = (Vector3)ProjectSettings.GetSetting("physics/3d/default_gravity_vector");
                upAxis = -g;

                float gMagnitude = (float)ProjectSettings.GetSetting("physics/3d/default_gravity");
                g *= gMagnitude;
                return g;
            }

            for (int i = 0; i < sources.Count; i++)
            {
                g += sources[i].GetGravity(position);
            }

            upAxis = -g.Normalized();
            return g;
        }

        public static Vector3 GetUpAxis (Vector3 position)
        {
            Vector3 g = Vector3.Zero;
            if (sources.Count == 0)
            {
                g = (Vector3)ProjectSettings.GetSetting("physics/3d/default_gravity_vector");
                return -g;
            }

            for (int i = 0; i < sources.Count; i++)
            {
                g += sources[i].GetGravity(position);
            }
            return -g.Normalized();
        }

        public static void Register (GravitySource source)
        {
            sources.Add(source);
        }

        public static void Unregister (GravitySource source)
        {
            sources.Remove(source);
        }
    }
}