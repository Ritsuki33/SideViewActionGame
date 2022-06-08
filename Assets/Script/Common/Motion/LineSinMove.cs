using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�@2�_�Ԓ���SIN�g�ړ�
public class LineSinMove: MonoBehaviour
{

    [SerializeField, Tooltip("�p���x")] float speed;
    [SerializeField, Tooltip("����")] float distance;
    [SerializeField, Tooltip("����(y�����������玞�v���)")] float angle = 0;

    Rigidbody2D rb;
    Velocity velocity;

    Vector2 position;

    //sin�g�̐��䐧��
    float omega;

    // Start is called before the first frame update
    void Start()
    {
        rb=this.GetComponent<Rigidbody2D>();
        velocity = new Velocity();
        position = this.transform.position;
        omega = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = new Vector2(1.0f, 0);
        direction = Quaternion.Euler(0, 0, angle) * direction;
        omega += speed*Time.deltaTime;
        omega %= 360;

        // sin(at)��t(����)�Ŕ�������ƁAa�~cos(at)
        rb.velocity = speed* Mathf.Cos(omega) * distance * direction.normalized;
    }
}

