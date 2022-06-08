

using System.Collections;

using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// ���x���グ������ƁA��]�~�̔��a���ێ��ł��Ȃ��Ȃ�̂Œ���
/// </summary>
public class CircularMotion3 : MonoBehaviour
{

    [SerializeField, Tooltip("���S")] GameObject center;
    [SerializeField, Tooltip("���a")] float radius;
    [SerializeField, Tooltip("���x")] float speed;
    [SerializeField, Tooltip("�p�x(y������0�x�Ƃ���)")] float angle;
    Velocity velocity;

    Rigidbody2D rb;
    Rigidbody2D center_rb;
    [SerializeField, Tooltip("���S��ǐ�����")] bool followCenter;

    public float CurrentSpeed
    {
        get
        {
            return this.speed;
        }
        set
        {
            this.speed = value;
        }
    }

    public float Radius
    {
        get
        {
            return this.radius;
        }
        set
        {
            this.radius = value;
        }
    }

    void Start()
    {

        velocity = new Velocity();

        rb = this.GetComponent<Rigidbody2D>();
        if (center)
        {
            center_rb = center.GetComponent<Rigidbody2D>();
        }
    }

    void FixedUpdate()
    {
        if (!center) return;
        // ���S�_����̈ʒu�x�N�g��
        Vector2 posVector = this.transform.position - center.transform.position;
        Debug.Log(posVector.magnitude);
        Vector3 radiusVector = posVector.normalized * radius;

        Vector2 direction = (new Vector2(-posVector.y, posVector.x)).normalized;

        // ���x
        Vector3 v = direction * speed;

        // �ړ��ʍ쐬
        Vector3 dv = v * Time.deltaTime;

        // �~�^�������邽�߂̈ړ��␳��(�����_�̌���)
        Vector2 a = dv;
        Vector2 b = -radiusVector;
        float m = radius;
        float n = (b - a).magnitude - radius;

        //  Vector2 correctionsVelocity = (m * a + n * b) / (m + n) �Ɠ��`;
        Vector2 correctionsVelocity = Vector2.Lerp(a, b, n / (m + n));

        // �~�^���̔��a�̒������̈ړ��␳��
        Vector3 CorrectPos = Vector3.Lerp(
            this.transform.position,
            center.transform.position + radiusVector,
            0.1f
            );
        Vector2 radiusCorrectVel = CorrectPos - this.transform.position;

        // �b���ɕϊ�

         this.rb.velocity = (correctionsVelocity + radiusCorrectVel) / Time.deltaTime;

        if (followCenter && center_rb)
        {
            this.rb.velocity += center_rb.velocity;
        }

        angle = Mathf.Atan2(radiusVector.x, radiusVector.y) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 360;
        }

    }
}
