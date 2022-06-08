using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Velocity
{
    [SerializeField,Tooltip("現在スピード")] float currentSpeed;
    [SerializeField,Tooltip("現在速度")] Vector2 currentVelocity;

    public float CurrentSpeed
    {
        get
        {
            
            return currentSpeed;
        }
        set
        {
            currentSpeed = Mathf.Clamp(value, 0, float.MaxValue);
            this.currentVelocity = currentSpeed * currentVelocity.normalized;
        }
    }


    public Vector2 CurrentVelocity
    {
        get { return currentVelocity; }
        set {
            currentVelocity = value;
            currentSpeed = currentVelocity.magnitude;
        }
    }

    public static implicit operator Vector2(Velocity v)
    {
        return v.CurrentVelocity;
    }

    public static implicit operator Velocity(Vector2 v)
    {
        Velocity res=new Velocity();
        res.CurrentVelocity = v;
        return res;
    }

    public static implicit operator float(Velocity v)
    {
        return v.CurrentSpeed;
    }


    // 速度調整
    public void Adjust(Vector2 acceleration, float arriveSpeed, float decelerate)
    {

        if (this.CurrentSpeed <= arriveSpeed)
        {
            this.CurrentVelocity += acceleration;
            if (this.CurrentSpeed > arriveSpeed)
            {
                this.CurrentSpeed = arriveSpeed;
            }
        }
        else
        {
            // 現在のスピードを保持
            float tmpSpeed = this.CurrentSpeed;

            // 加速度を反映
            this.CurrentVelocity += acceleration;

            // 最大速度を超えている場合、調整
            if (this.CurrentSpeed > arriveSpeed)
            {
                // 現在のスピード分から減らす
                this.CurrentSpeed = tmpSpeed - decelerate;

                if (this.CurrentSpeed < arriveSpeed)
                {
                    this.currentSpeed = arriveSpeed;
                }
            }
        }
    }
}
