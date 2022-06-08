using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VelocityControll : MonoBehaviour
{
    [SerializeField,Tooltip("���ݑ��x"), ReadOnly] private float magnitude;

    [SerializeField, Tooltip("�����x")] protected float acceleration; 
    [SerializeField, Tooltip("�ő呬�x")] protected float maxVelocity; 
    [SerializeField, Tooltip("����")] protected float decelerate;

    [SerializeField, Tooltip("�����x�N�g��")] protected Vector2 normal;

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
                // �ő呬�x�𒴉߂��Ȃ��悤�ɂ���
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
