using Godot;
using System;

namespace Electronova.Generic
{
    public static class VectorExtensions
    {
        /*/// <summary>
        /// Takes a direction and translates it to the plane defined by its normal.
        /// </summary>
        /// <param name="direction">Vector to be translated</param>
        /// <param name="normal">Normal of the plane</param>
        /// <returns>Vector3 of direction translated to the plane</returns>
        static Vector3 ProjectDirectionOnPlane(Vector3 direction, Vector3 normal)
        {
            return (direction - normal * direction.Dot(normal)).Normalized();
        }*/

        /// <summary>
        /// Takes a direction and translates it to the plane defined by its normal.
        /// </summary>
        /// <param name="direction">Vector to be translated.</param>
        /// <param name="normal">Normal of the plane.</param>
        /// <returns></returns>
        public static Vector3 ProjectOntoPlane(this Vector3 direction, Vector3 normal)
        {
            return (direction - normal * direction.Dot(normal)).Normalized();
        }
    }
}