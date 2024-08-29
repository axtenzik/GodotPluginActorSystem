using Electronova.Generic;
using Godot;
using System;

namespace Electronova.Actors
{
    /// <summary>
    /// State tree performer node that will move an actor toward a desired direction based on acceleration.
    /// </summary>
    [Tool]
    public partial class MovementPerformer : Node, IStateTree
    {
        [ExportCategory("State Tree")]
        [Export] StringName performerState = null;

        [ExportCategory("Actor")]
        [Export] Actor parent;
        [Export] InputState inputState;
        [Export] ActorContacts actorContacts;

        [ExportCategory("Movement Type")]
        [Export] MovementProperties movementProperties = null;
        [Export] bool projectOntoGround = true;
        
        public StringName State => performerState;

        Vector3 forwardAxis;
        Vector3 rightAxis;
        Vector3 desiredVelocity;

        public void Tick()
        {
            FindDesiredVelocity();
            AdjustVelocity();
            //Rotate();

            if (GetChildCount() == 0)
            {
                return;
            }
            
            IStateTree selectedChild = (IStateTree)GetChild(0);
            selectedChild?.Tick();
        }

        private void AdjustVelocity()
        {
            /*Vector3 xAxis = ProjectDirectionOnPlane(rightAxis, contactNormal);
            Vector3 zAxis = ProjectDirectionOnPlane(forwardAxis, contactNormal);*/

            Vector3 xAxis = projectOntoGround ? rightAxis.ProjectOntoPlane(actorContacts.ContactNormal) : rightAxis;
            Vector3 zAxis = projectOntoGround ? forwardAxis.ProjectOntoPlane(actorContacts.ContactNormal) : forwardAxis;

            float currentX = parent.Velocity.Dot(xAxis);
            float currentZ = parent.Velocity.Dot(zAxis);

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

            desiredVelocity = new Vector3(inputState.Move.X, 0, inputState.Move.Y) * movementProperties.MaxSpeed;
            //desiredVelocity = (forwardAxis * parent.Inputter.CurrentMove.Y + rightAxis * parent.Inputter.CurrentMove.X) * movementProperties.MaxSpeed;
        }

        private void Rotate()
        {
            if (parent.Velocity.LengthSquared() < 0.01f)
            {
                return;
            }

            Vector3 lookDirection = parent.Velocity.ProjectOntoPlane(parent.UpAxis);

            //If LookDirection is zero then Basis.LookingAt() will error.
            if (lookDirection == Vector3.Zero)
            { 
                return;
            }

            Basis lookBasis = Basis.LookingAt(lookDirection, parent.UpAxis);

            parent.SetRotation(lookBasis);

            /*Vector3 xLook = parent.UpAxis.Cross(parent.Velocity);
            Vector3 zLook = xLook.Cross(parent.UpAxis);

            if (xLook.LengthSquared() > 0.1f || zLook.LengthSquared() > 0.1f)
            {
                Basis lookBasis = Basis.LookingAt(zLook, parent.UpAxis);

                parent.SetRotation(lookBasis);
            }*/
        }
    }
}