using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Ant
{
    public class AntRunState : StateChildBase<Ant>
    {
        private bool leftFlag = true;

        // ì¸èÍèàóù
        public override void OnEnter(Ant ant)
        {
            //ant.move.Acceleration = 1.0f;
            //ant.move.Decelerate = 0.5f;
            //ant.move.MaxSpeed = 2.5f;
            Vector3 startPosition = ant.transform.position;
            startPosition.x -= Mathf.Abs(ant.transform.localScale.x) / 2;
            //ant.criffCheck.SetCheckStartVector(startPosition);
        }

        // ëﬁèÍèàóù
        public override void OnExit(Ant ant)
        {

        }


        // çXêVèàóù
        public override int StateFixedUpdate(Ant ant)
        {
            if (!ant.standOnGround.IsOnGround)
            {
                return (int)AntStateController.StateType.Float;
            }

            Vector3 startPosition = ant.transform.position;

            if (ant.criffCheck.CheckCriff(leftFlag))
            {
                leftFlag ^= true;
            }
            if (leftFlag)
            {
                startPosition.x -= Mathf.Abs(ant.transform.localScale.x) / 2;
                ant.transform.localScale = new Vector3(-1, 1, 0);
                ant.move.Input(Move.Direction.Left);
            }
            else
            {
                startPosition.x += Mathf.Abs(ant.transform.localScale.x) / 2;
                ant.transform.localScale = new Vector3(1, 1, 0);
                ant.move.Input(Move.Direction.Right);
            }
            //ant.criffCheck.SetCheckStartVector(startPosition);

            

            return this.stateType;
        }

        public override int StateUpdate(Ant ant)
        {
            return this.stateType;
        }

        //private bool FindCliff(Ant ant)
        //{
        //    Vector3 startPosition = ant.transform.position;
        //    if (leftFlag)
        //    {
        //        startPosition.x -= Mathf.Abs(ant.transform.localScale.x) / 2;
        //    }
        //    else
        //    {
        //        startPosition.x += Mathf.Abs(ant.transform.localScale.x) / 2;
        //    }

        //    RaycastHit2D hit = Physics2D.Linecast(startPosition, startPosition+Vector3.down, ant.gravity.GetGroundLayer());
        //    Debug.DrawLine(startPosition, startPosition + Vector3.down,Color.red);
        //    return !hit;
        //}
    }

}
