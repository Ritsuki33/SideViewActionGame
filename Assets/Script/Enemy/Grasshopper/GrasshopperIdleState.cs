using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Grasshopper
{
    public class GrasshopperIdleState : StateChildBase<Grasshopper>
    {
        float time;
        // “üêˆ—
        public override void OnEnter(Grasshopper grasshopper)
        {
            grasshopper.move.Input(Move.Direction.None);

            if (grasshopper.collisionTurn.CheckIsLeft())
            {
                grasshopper.transform.localScale = new Vector3(1, 1, 0);
            }
            else
            {
                grasshopper.transform.localScale = new Vector3(-1, 1, 0);
            }

            grasshopper.animator.SetBool("IsOnGround",true);
            time = 0;
        }

        public override int StateFixedUpdate(Grasshopper grasshopper)
        {
            time += Time.deltaTime;

            if (time > grasshopper.idleTime)
            {
                return (int)GrasshopperStateController.StateType.Jump;
            }
            return this.stateType;
        }

        // ‘Şêˆ—
        public override void OnExit(Grasshopper grasshopper)
        {
            time = 0;
        }
    }

}