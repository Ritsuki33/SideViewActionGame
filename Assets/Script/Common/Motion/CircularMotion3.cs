

using System.Collections;

using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// 速度を上げすぎると、回転円の半径を維持できなくなるので注意
/// </summary>
public class CircularMotion3 : MonoBehaviour
{

    [SerializeField, Tooltip("中心")] GameObject center;
    [SerializeField, Tooltip("半径")] float radius;
    [SerializeField, Tooltip("速度")] float speed;
    [SerializeField, Tooltip("角度(y軸正を0度とする)")] float angle;
    Velocity velocity;

    Rigidbody2D rb;
    Rigidbody2D center_rb;
    [SerializeField, Tooltip("中心を追随する")] bool followCenter;

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
        // 中心点からの位置ベクトル
        Vector2 posVector = this.transform.position - center.transform.position;
        Debug.Log(posVector.magnitude);
        Vector3 radiusVector = posVector.normalized * radius;

        Vector2 direction = (new Vector2(-posVector.y, posVector.x)).normalized;

        // 速度
        Vector3 v = direction * speed;

        // 移動量作成
        Vector3 dv = v * Time.deltaTime;

        // 円運動をするための移動補正分(内分点の公式)
        Vector2 a = dv;
        Vector2 b = -radiusVector;
        float m = radius;
        float n = (b - a).magnitude - radius;

        //  Vector2 correctionsVelocity = (m * a + n * b) / (m + n) と同義;
        Vector2 correctionsVelocity = Vector2.Lerp(a, b, n / (m + n));

        // 円運動の半径の調整分の移動補正分
        Vector3 CorrectPos = Vector3.Lerp(
            this.transform.position,
            center.transform.position + radiusVector,
            0.1f
            );
        Vector2 radiusCorrectVel = CorrectPos - this.transform.position;

        // 秒速に変換

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
