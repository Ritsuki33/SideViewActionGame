using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VelocityControll : MonoBehaviour
{
    [SerializeField,Tooltip("現在速度"), ReadOnly] private float magnitude;

    [SerializeField, Tooltip("加速度")] protected float acceleration; 
    [SerializeField, Tooltip("最大速度")] protected float maxVelocity; 
    [SerializeField, Tooltip("減速")] protected float decelerate;

    [SerializeField, Tooltip("方向ベクトル")] protected Vector2 normal;

    [SerializeField] private bool enable = true;
    protected Rigidbody2D rb;

    public float Acceleration { get { return acceleration; } set { this.acceleration = value; } }
    public float MaxVelocity { get { return maxVelocity; } set { this.maxVelocity = value; } }
    public float Decelerate { get { return decelerate; } set { this.decelerate = value; } }
    public bool Enable
    {
        get
        {
            return enable;
        }
        set
        {
            if (value == false) magnitude = 0;
            enable = value;
        }
    }
    public float Magnitude
    {
        get
        {
                return magnitude;
        }
        set
        {
            if (enable)
            {
                // 最大速度を超過しないようにする
                this.magnitude = Mathf.Clamp(value, 0, maxVelocity);
            }
        }
    }

    public Vector2 CurrentVelocity
    {
        get
        {
            return magnitude * normal;
        }
        set
        {
            Magnitude = value.magnitude;
            normal = value.normalized;
        }
    }


    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
