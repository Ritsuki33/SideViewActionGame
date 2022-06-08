using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public class PlayerState_Jump : StateChildBase<Player>
    {
        // 入場処理
        public override void OnEnter(Player player)
        {
            player.jump.Input(true);
            player.jump.SetJumpVelocity(new Vector2(0,30f));
            player.jump.Decrease(false);
            player.animator.SetBool("IsOnGround", false);
        }

        // 退場処理
        public override void OnExit(Player player)
        {
        }
        
        public override int StateFixedUpdate(Player player)
        {
             if (!player.standOnGround.IsOnGround)
            {
                return (int)PlayerStateController.StateType.IsJumping;

            }
            return stateType;
        }
    }
    public class PlayerState_IsJumping : StateChildBase<Player>
    {

        // 入場処理
        public override void OnEnter(Player player)
        {
            player.jump.Decrease(true);
            player.animator.SetBool("IsOnGround", false);

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                player.move.Input(Move.Direction.Left);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                player.move.Input(Move.Direction.Right);
            }
        }

        // 退場処理
        public override void OnExit(Player player)
        {
            player.jump.Input(false);

        }

        public override int StateFixedUpdate(Player player)
        {
            if (player.standOnGround.IsOnGround)
            {
                return (int)PlayerStateController.StateType.Idle;

            }
            return stateType;
        }

        public override int StateUpdate(Player player)
        {
            

            if (Input.GetKey(KeyCode.RightArrow))
            {
                    player.move.Input(Move.Direction.Right);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                    player.move.Input(Move.Direction.Left);
            }

            if (!Input.GetKey(KeyCode.Space))
            {

                return (int)PlayerStateController.StateType.Float;
            }

            return stateType;
        }
    }

}