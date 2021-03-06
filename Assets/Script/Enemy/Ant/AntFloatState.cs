using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Ant
{
    public class AntFloatState : StateChildBase<Ant>
    {

        // 入場処理
        public override void OnEnter(Ant ant)
        {
        }

        // 退場処理
        public override void OnExit(Ant ant){}


        // 更新処理
        public override int StateFixedUpdate(Ant ant)
        {
            if (ant.standOnGround.IsOnGround)
            {
                return (int)AntStateController.StateType.Run;
            }
            return this.stateType;
        }

        public override int StateUpdate(Ant ant)
        {
            return this.stateType;
        }
    }
}