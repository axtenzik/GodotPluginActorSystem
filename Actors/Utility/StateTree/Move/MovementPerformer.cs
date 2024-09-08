using Electronova.Generic;
using Godot;
using System;

namespace Electronova.Actors
{
    /// <summary>
    /// State tree performer node that will move an actor toward a desired direction based on acceleration.
    /// </summary>
    [Tool]
    [GlobalClass, Icon("res://addons/Electronova/Icons/Generic/StateTree/Performer.png")]
    public partial class MovementPerformer : StateTree
    {
        [ExportCategory("Actor")]
        [Export] Actor parent;
        [Export] Vector2Node move;
        [Export] ActorContacts actorContacts;

        [ExportCategory("Movement Type")]
        [Export] MovementProperties movementProperties = null;
        [Export] bool projectOntoGround = true;

        Vector3 forwardAxis;
        Vector3 rightAxis;
        Vector3 desiredVelocity;

        public override string[] _GetConfigurationWarnings()
        {
            return Array.Empty<string>();
        }

        public override void Tick()
        {
            FindDesiredVelocity();
            AdjustVelocity();

            base.Tick();
        }

        private void AdjustVelocity()
        {
            /*Vector3 xAxis = ProjectDirectionOnPlane(rightAxis, contactNormal);
            Vector3 zAxis = ProjectDirectionOnPlane(forwardAxis, contactNormal);*/

            Vector3 xAxis = projectOntoGround ? rightAxis.ProjectOntoPlane(actorContacts.ContactNormal) : rightAxis;
            Vector3 zAxis = projectOntoGround ? forwardAxis.ProjectOntoPlane(actorContacts.ContactNormal) : forwardAxis;

            /*float currentX = parent.Velocity.Dot(xAxis);
            float currentZ = parent.Velocity.Dot(zAxis);//*/

            Vector3 relativeVelocity = parent.Velocity - parent.ConnectedVelocity;
            float currentX = relativeVelocity.Dot(xAxis);
            float currentZ = relativeVelocity.Dot(zAxis);//*/

            //float acceleration = OnGround ? maxAcceleration : maxAirAcceleration;
            //float maxSpeedChange = acceleration * parent.DeltaStep;
            float maxSpeedChange = movementProperties.Acceleration * parent.DeltaStep;

            float newX = Mathf.MoveToward(currentX, desiredVelocity.X, maxSpeedChange);
            float newZ = Mathf.MoveToward(currentZ, desiredVelocity.Z, maxSpeedChange);

            Vector3 deltaVelocity = xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
            parent.AddVelocity(deltaVelocity);
            //parent.AddVelocity(xAxis * (newX - currentX) + zAxis * (newZ - currentZ));
        }

        private void FindDesiredVelocity()
        {
            if (parent.ControllingCamera != null)
            {
                Vector3 forward = parent.ControllingCamera.Transform.Basis.Z;
                forward.Normalized();
                forwardAxis = forward.ProjectOntoPlane(parent.UpAxis);

                Vector3 right = parent.ControllingCamera.Transform.Basis.X;
                right.Normalized();
                rightAxis = right.ProjectOntoPlane(parent.UpAxis);

                //desiredVelocity = (forward * playerInput.Y + right * playerInput.X) * maxSpeed;
            }
            else
            {
                forwardAxis = Vector3.Forward.ProjectOntoPlane(parent.UpAxis);
                rightAxis = Vector3.Right.ProjectOntoPlane(parent.UpAxis);

                //desiredVelocity = new Vector3(playerInput.X, 0, playerInput.Y) * maxSpeed;
            }//*/

            desiredVelocity = new Vector3(move.Value.X, 0, move.Value.Y) * movementProperties.MaxSpeed;
            //desiredVelocity = (forwardAxis * parent.Inputter.CurrentMove.Y + rightAxis * parent.Inputter.CurrentMove.X) * movementProperties.MaxSpeed;
        }
    }
}