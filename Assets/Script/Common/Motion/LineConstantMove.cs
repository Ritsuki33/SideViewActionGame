using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//　2点間直線SIN波移動
public class LineConstantMove : MonoBehaviour
{
    [SerializeField, Tooltip("通過点")] GameObject[] movePosition;
    [SerializeField, Tooltip("速度")] float speed;
    [SerializeField, Tooltip("ループ")] bool loop;
    [SerializeField, Tooltip("現在速度")] Vector2 currentVelocity;

    // 通過点番号
    int nextMovePositionNum;


    // 前フレーム位置
    Vector2 oldPos;

    Rigidbody2D rb;

    bool reverseFlag;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        if (movePosition != null && movePosition.Length != 0)
        {
            // スタート地点は1個目の位置に合わせる。
            this.rb.position = movePosition[0].transform.position;
            this.nextMovePositionNum = 1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movePosition == null || movePosition.Length == 0) return;

        MovePositionCtl();

        Vector2 toVector = Vector2.MoveTowards(this.rb.position, this.movePosition[nextMovePositionNum].transform.position, speed * Time.deltaTime);

        currentVelocity = (toVector - this.rb.position) / Time.deltaTime;
        oldPos = toVector;
        this.rb.MovePosition(toVector);
    }

    // 次の目的地制御
    void MovePositionCtl()
    {
        if (loop)
        {
            if (Vector2.Distance(this.rb.position, this.movePosition[nextMovePositionNum].transform.position) < 0.1f)
            {
                if (nextMovePositionNum < movePosition.Length - 1)
                {
                    nextMovePositionNum++;
                }
                else if (nextMovePositionNum == movePosition.Length - 1)
                {
                    nextMovePositionNum = 0;
                }
            }
        }
        else
        {
            if (Vector2.Distance(this.rb.position, this.movePosition[nextMovePositionNum].transform.position) < 0.1f)
            {
                if (reverseFlag)
                {
                    if (nextMovePositionNum > 0)
                    {
                        nextMovePositionNum--;
                    }
                    else if (nextMovePositionNum == 0)
                    {
                        reverseFlag = false;
                    }
                }
                else
                {
                    if (nextMovePositionNum < movePosition.Length - 1)
                    {
                        nextMovePositionNum++;
                    }
                    else if (nextMovePositionNum == movePosition.Length - 1)
                    {
                        reverseFlag = true;
                    }
                }

            }
        }
    }

    Vector2 GetVelocity()
    {
        return currentVelocity;
    }
}
