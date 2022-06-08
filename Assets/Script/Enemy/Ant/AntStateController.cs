using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Ant
{
    public class AntStateController : StateControllerBase<Ant>
    {
        public enum StateType
        {
            Run,
            Float
        }
        public override void Initalize(Ant player, int initalizeStateType)
        {
            //ëñçs
            stateDic[(int)StateType.Run] = new AntRunState();
            stateDic[(int)StateType.Run].Initialize((int)StateType.Run);

            stateDic[(int)StateType.Float] = new AntFloatState();
            stateDic[(int)StateType.Float].Initialize((int)StateType.Float);

            currentState = initalizeStateType;
            stateDic[currentState].OnEnter(player);
        }
    }

}