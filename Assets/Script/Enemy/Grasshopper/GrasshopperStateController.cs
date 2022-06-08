using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Grasshopper {
    public class GrasshopperStateController : StateControllerBase<Grasshopper>
    {
        public enum StateType
        {
            Idle,
            Jump,
            Fall
        }
        public override void Initalize(Grasshopper grasshopper, int initalizeStateType)
        {
            // ‘Ò‹@
            stateDic[(int)StateType.Idle] = new GrasshopperIdleState();
            stateDic[(int)StateType.Idle].Initialize((int)StateType.Idle);

            // ‘Ò‹@
            stateDic[(int)StateType.Jump] = new GrasshopperJumpState();
            stateDic[(int)StateType.Jump].Initialize((int)StateType.Jump);

            // ‘Ò‹@
            stateDic[(int)StateType.Fall] = new GrasshopperFallState();
            stateDic[(int)StateType.Fall].Initialize((int)StateType.Fall);


            currentState = initalizeStateType;
            stateDic[currentState].OnEnter(grasshopper);
        }
    }
}