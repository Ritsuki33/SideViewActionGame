using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Ant
{
    public class AntFloatState : StateChildBase<Ant>
    {

        // “üêˆ—
        public override void OnEnter(Ant ant)
        {
        }

        // ‘Şêˆ—
        public override void OnExit(Ant ant){}


        // XVˆ—
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