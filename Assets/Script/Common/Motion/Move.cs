using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    public enum Direction
    {
        None=0,
        Right = 1,
        Left = 2,
        Up = 4,
        Down = 8
    }
   
    //public Direction direction { get; private set; }

    private byte inputDirection;

    public float MaxSpeed
    {
        get;set;
    }

    public float Acceleration { get; set; }
    public float Decelerate { get; set; }

    Velocity velocity;

    Rigidbody2D rb;

    public float CurrentSpeed
    {
        get { return velocity; }
    }
    public Vector2 CurrentVelocity
    {
        get { return velocity; }
    }
    void Start(){

        inputDirection =0;
        velocity = new Velocity();
        this.rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (InputCheck(Direction.None)){
                // velocity.CurrentSpeed -= Decelerate;
                velocity.Adjust(Vector2.zero, 0, Decelerate);

        }
        else
        {
            // x軸方向
            float tx = 0;
            if (InputCheck(Direction.Right))
            {
                tx = Acceleration;
            }
            else if (InputCheck(Direction.Left))
            {
                tx = -Acceleration;
            }

            // y軸方向
            float ty = 0;
            if (InputCheck(Direction.Up))
            {
                ty = Acceleration;
            }
            else if (InputCheck(Direction.Down))
            {
                ty = -Acceleration;
            }

            // 加速度決定
            Vector2 acceleration = new Vector2(tx, ty);

            velocity.Adjust(acceleration, MaxSpeed, Decelerate);


        }

        this.rb.velocity = velocity;

    }

    private void Update()
    {
        //inputDirection =0;
    }

    private bool InputCheck(Direction direction)
    {
        if (direction == Direction.None)
        {
            return inputDirection == 0;
        }
        return (inputDirection & (byte)direction) != 0;
    }

    public void Input(Direction direction)
    {
        if (direction == Direction.None)
        {
            inputDirection = 0;
        }
        if (direction == Direction.Right)
        {
            inputDirection &= 0xFF^(byte)Direction.Left;
            inputDirection |= (byte)direction;

        }
        if (direction == Direction.Left)
        {
            inputDirection &= 0xFF^(byte)Direction.Right;
            inputDirection |= (byte)direction;

        }
        if (direction == Direction.Up)
        {
            inputDirection ^= (byte)Direction.Down;
            inputDirection |= (byte)direction;

        }
        if (direction == Direction.Down)
        {
            inputDirection ^= (byte)Direction.Up;
            inputDirection |= (byte)direction;

        }
        else
        {
            inputDirection |= (byte)direction;
        }
    }

   
}
