using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Ant
{
    public class AntFloatState : StateChildBase<Ant>
    {

        // ���ꏈ��
        public override void OnEnter(Ant ant)
        {
        }

        // �ޏꏈ��
        public override void OnExit(Ant ant){}


        // �X�V����
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