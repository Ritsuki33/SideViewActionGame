using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public class PlayerState_Run : StateChildBase<Player>
    {
        //// 入場処理
        public override void OnEnter(Player player)
        {
            player.animator.SetBool("IsOnGround", true);
            player.animator.SetFloat("RunSpeed", 0.3f);

            player.move.Acceleration = 1.0f;
            player.move.Decelerate = 0.5f;
            player.move.MaxSpeed = 5.0f;

            //if (Input.GetKey(KeyCode.LeftArrow))
            //{
            //    player.transform.localScale = new Vector3(-1, 1, 1);
            //    player.move.Input(Move.Direction.Left);
            //}
            //else if(Input.GetKey(KeyCode.RightArrow))
            //{
            //    player.transform.localScale = new Vector3(1, 1, 1);
            //    player.move.Input(Move.Direction.Right);
            //}
        }

        public override int StateFixedUpdate(Player player)
        {
            if (player.isDead)
            {
                return (int)PlayerStateController.StateType.Dead;
            }
            else if (!player.standOnGround.IsOnGround)
            {
                return (int)PlayerStateController.StateType.Float;
            }

            return this.stateType;
        }

        public override int StateUpdate(Player player)
        {
            if (player.currentController.Jump())
            {
                return (int)PlayerStateController.StateType.Jump;
            }
            else if (player.currentController.LeftDash())
            {
                player.transform.localScale = new Vector3(-1, 1, 1);
                player.move.Input(Move.Direction.Left);
                return (int)PlayerStateController.StateType.Dash;
            }
            else if (player.currentController.RightDash())
            {
                player.transform.localScale = new Vector3(1, 1, 1);
                player.move.Input(Move.Direction.Right);
                return (int)PlayerStateController.StateType.Dash;
            }
            else if (player.currentController.LeftRun())
            {
                player.transform.localScale = new Vector3(-1, 1, 1);
                player.move.Input(Move.Direction.Left);
                return (int)PlayerStateController.StateType.Run;
            }
            else if (player.currentController.RightRun())
            {
                player.transform.localScale = new Vector3(1, 1, 1);
                player.move.Input(Move.Direction.Right);
                return (int)PlayerStateController.StateType.Run;
            }
            
            return (int)PlayerStateController.StateType.Idle;

            //if (Input.GetKey(KeyCode.Space))
            //{
            //    return (int)PlayerStateController.StateType.Jump;
            //}


            //if (!player.standOnGround.IsOnGround)
            //{
            //    return (int)PlayerStateController.StateType.Float;
            //}

            //if (Input.GetKey(KeyCode.A))
            //{
            //    if (Input.GetKey(KeyCode.LeftArrow))
            //    {
            //        return (int)PlayerStateController.StateType.Dash;
            //    }
            //    else if (Input.GetKey(KeyCode.RightArrow))
            //    {
            //        return (int)PlayerStateController.StateType.Dash;
            //    }

            //    return (int)PlayerStateController.StateType.Idle;
            //}
            //else
            //{

            //    if (Input.GetKey(KeyCode.LeftArrow))
            //    {
            //        player.transform.localScale = new Vector3(-1, 1, 1);
            //        player.move.Input(Move.Direction.Left);

            //        return (int)PlayerStateController.StateType.Run;
            //    }
            //    else if (Input.GetKey(KeyCode.RightArrow))
            //    {
            //        player.transform.localScale = new Vector3(1, 1, 1);
            //        player.move.Input(Move.Direction.Right);

            //        return (int)PlayerStateController.StateType.Run;
            //    }

            //    return (int)PlayerStateController.StateType.Idle;
            //}

        }
        // 退場処理
        public override void OnExit(Player player) { }
    }
}