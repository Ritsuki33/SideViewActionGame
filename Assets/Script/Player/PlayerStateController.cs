using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public class PlayerStateController : StateControllerBase<Player>
    {
        public enum StateType
        {
            Idle,
            Run,
            Dash,
            Float,
            Jump,
            IsJumping,
            Dead
        }

        public override void Initalize(Player player, int initalizeStateType)
        {
            // 待機
            stateDic[(int)StateType.Idle] = new PlayerState_Idle();
            stateDic[(int)StateType.Idle].Initialize((int)StateType.Idle);

            //走行
            stateDic[(int)StateType.Run] = new PlayerState_Run();
            stateDic[(int)StateType.Run].Initialize((int)StateType.Run);

            //ダッシュ
            stateDic[(int)StateType.Dash] = new PlayerState_Dash();
            stateDic[(int)StateType.Dash].Initialize((int)StateType.Dash);

            stateDic[(int)StateType.Dash] = new PlayerState_Dash();
            stateDic[(int)StateType.Dash].Initialize((int)StateType.Dash);

            //滞空
            stateDic[(int)StateType.Float] = new PlayerState_Float();
            stateDic[(int)StateType.Float].Initialize((int)StateType.Float);

            //ジャンプ
            stateDic[(int)StateType.Jump] = new PlayerState_Jump();
            stateDic[(int)StateType.Jump].Initialize((int)StateType.Jump);
            stateDic[(int)StateType.IsJumping] = new PlayerState_IsJumping();
            stateDic[(int)StateType.IsJumping].Initialize((int)StateType.IsJumping);

            //死亡
            stateDic[(int)StateType.Dead] = new PlayerState_Dead();
            stateDic[(int)StateType.Dead].Initialize((int)StateType.Dead);

            currentState = initalizeStateType;
            stateDic[currentState].OnEnter(player);
        }

    }

}