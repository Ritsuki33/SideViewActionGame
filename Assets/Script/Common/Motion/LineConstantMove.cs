using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�@2�_�Ԓ���SIN�g�ړ�
public class LineConstantMove : MonoBehaviour
{
    [SerializeField, Tooltip("�ʉߓ_")] GameObject[] movePosition;
    [SerializeField, Tooltip("���x")] float speed;
    [SerializeField, Tooltip("���[�v")] bool loop;
    [SerializeField, Tooltip("���ݑ��x")] Vector2 currentVelocity;

    // �ʉߓ_�ԍ�
    int nextMovePositionNum;


    // �O�t���[���ʒu
    Vector2 oldPos;

    Rigidbody2D rb;

    bool reverseFlag;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        if (movePosition != null && movePosition.Length != 0)
        {
            // �X�^�[�g�n�_��1�ڂ̈ʒu�ɍ��킹��B
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

    // ���̖ړI�n����
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
