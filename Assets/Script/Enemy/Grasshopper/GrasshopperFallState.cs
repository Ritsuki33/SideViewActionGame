using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Grasshopper
{
    public class GrasshopperFallState : StateChildBase<Grasshopper>
    {
        // “üêˆ—
        public override void OnEnter(Grasshopper grasshopper)
        {
            grasshopper.animator.SetBool("IsOnGround", false);
            grasshopper.animator.SetBool("Jump", false);

        }

        public override int StateFixedUpdate(Grasshopper grasshopper)
        {
            if (grasshopper.gravity.IsOnGround)
            {
                return (int)GrasshopperStateController.StateType.Idle;
            }
            return (int)GrasshopperStateController.StateType.Fall;
        }

        // ‘Şêˆ—
        public override void OnExit(Grasshopper grasshopper)
        {

        }
    }

}