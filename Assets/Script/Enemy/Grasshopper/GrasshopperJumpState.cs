using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Grasshopper
{
    public class GrasshopperJumpState : StateChildBase<Grasshopper>
    {
        // ì¸èÍèàóù
        public override void OnEnter(Grasshopper grasshopper)
        {
            grasshopper.jump.SetJumpVelocity(grasshopper.JumpVelocity);
            grasshopper.move.MaxSpeed = 2;
            grasshopper.move.Acceleration = 2;
            grasshopper.move.Decelerate = 1;
            if (grasshopper.collisionTurn.CheckIsLeft())
            {
                grasshopper.transform.localScale = new Vector3(1, 1, 0);
                grasshopper.move.Input(Move.Direction.Left);
            }
            else
            {
                grasshopper.transform.localScale = new Vector3(-1, 1, 0);
                grasshopper.move.Input(Move.Direction.Right);
            }
            grasshopper.animator.SetBool("IsOnGround", false);
            grasshopper.animator.SetBool("Jump", true);
            grasshopper.jump.Input(true);
        }

        public override int StateFixedUpdate(Grasshopper grasshopper)
        {
            if (grasshopper.jump.JumpSpeedIsZero())
            {
                return (int)GrasshopperStateController.StateType.Fall;
            }

            return (int)GrasshopperStateController.StateType.Jump;
        }

        // ëﬁèÍèàóù
        public override void OnExit(Grasshopper grasshopper)
        {
        }
    }

}