using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Gravity))]
public class Jump : MonoBehaviour
{
    [SerializeField, Tooltip("地面のレイヤー")] LayerMask groundLayer;

    private bool jumpFlag { get; set; }

    public Velocity velocity;

    public float Decelerate;
    private Vector2 JumpVelocity;

    Gravity gravity;
    CapsuleCollider2D capsuleCollider;
    
    RaycastHit2D hitHead;

    // ジャンプし始め
    bool startJump = false;

    public bool IsJumping{
        get { return this.velocity.CurrentSpeed > 0; }
    }

    public float Speed
    {
        get { return this.velocity.CurrentSpeed; } 
    }

    // ジャンプ速度が減速するか（足が地面から離れるまでは減速しないようにするため）
    private bool decreaseFlag { get; set; }
    
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Velocity();
        Decelerate = 1.0f;
        JumpVelocity = new Vector2(0, 30.0f);
        this.rb = GetComponent<Rigidbody2D>();
        capsuleCollider=GetComponent<CapsuleCollider2D>();
        gravity=GetComponent<Gravity>();
        decreaseFlag = true;
    }

    private void FixedUpdate()
    {

        if (jumpFlag && !startJump)
        {
            startJump = true;
            velocity = JumpVelocity;
            if(gravity)gravity.Enable = false;
        }
        

        if (startJump)
        {
            if (!jumpFlag)
            {
                velocity.CurrentSpeed /= 2;
                startJump = false;
            }
        }

        if (hitHead) 
        { 
            // ヒット時の角度と上単ベクトルとの角度が140度以上の場合、速度を０にする
            float angle = Vector2.Angle(Vector2.up, hitHead.normal);
            if (140 <= angle && angle <= 180)
            {
                velocity.CurrentSpeed = 0;
            }
        }

        if(decreaseFlag) velocity.Adjust(Vector2.zero, 0, this.Decelerate);

        if(velocity.CurrentSpeed == 0)
        {
            if (gravity)
            {
                gravity.Enable = true;
            }
            jumpFlag = false;
            startJump = false;
        }
        this.rb.velocity += velocity;
    }
    void Update()
    {
        hitHead = CollideManager.HitHead(this.transform, capsuleCollider, 0.5f, groundLayer);
    }

    public void SetJumpVelocity(Vector2 JumpVelocity)
    {
        this.JumpVelocity = JumpVelocity;
    }

    public void Input(bool jumpFlag)
    {
        this.jumpFlag = jumpFlag;
    }

    public void Decrease(bool decreaseFlag)
    {
        this.decreaseFlag = decreaseFlag;
    }

    public bool JumpSpeedIsZero()
    {
        return this.velocity.CurrentSpeed == 0;
    }
}
