using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public class PlayerState_Float : StateChildBase<Player>
    {
       
        // 入場処理
        public override void OnEnter(Player player)
        {
            player.animator.SetBool("IsOnGround", false);


            //if (Input.GetKey(KeyCode.LeftArrow))
            //{
            //    player.move.Input(Move.Direction.Left);
            //}
            //else if (Input.GetKey(KeyCode.RightArrow))
            //{
            //    player.move.Input(Move.Direction.Right);
            //}
        }

        // 退場処理
        public override void OnExit(Player player) { }

        public override int StateFixedUpdate(Player player)
        {
            if (player.isDead)
            {
                return (int)PlayerStateController.StateType.Dead;
            }
            else if (player.standOnGround.IsOnGround)
            {
                return (int)PlayerStateController.StateType.Idle;
            }

            return this.stateType;
        }

        // 更新処理
        public override int StateUpdate(Player player)
        {
            if (player.currentController.LeftRun())
            {
                //player.transform.localScale = new Vector3(-1, 1, 1);
                player.move.Input(Move.Direction.Left);
            }
            else if (player.currentController.RightRun())
            {
                //player.transform.localScale = new Vector3(1, 1, 1);
                player.move.Input(Move.Direction.Right);
            }

            //if (Input.GetKey(KeyCode.RightArrow))
            //{
            //    player.move.Input(Move.Direction.Right);
            //    return (int)PlayerStateController.StateType.Float;
            //}
            //else if (Input.GetKey(KeyCode.LeftArrow))
            //{
            //    player.move.Input(Move.Direction.Left);
            //    return (int)PlayerStateController.StateType.Float;

            //}
            return stateType;
        }


        public class Left : PlayerState_Float
        {
            // 入場処理
            public override void OnEnter(Player player)
            {
                base.OnEnter(player);
                player.move.Input(Move.Direction.Left);
            }
        }

        public class Right : PlayerState_Float
        {
            // 入場処理
            public override void OnEnter(Player player)
            {
                base.OnEnter(player);
                player.move.Input(Move.Direction.Right);
            }
        }
    }
}