using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Gravity : MonoBehaviour
{

    [SerializeField, Tooltip("地面のレイヤー")] LayerMask groundLayer;
    CircleCollider2D circleCollider;
    StandOnGround standOnGround;

    public bool IsOnGround { get; private set; }
    public Vector2 Acceleration;
    public float Decelerate;
    public float MaxSpeed;
    public Vector2 CurrentVelocity;
    public bool enable;

    public bool Enable {
        get
        {
            return enable;
        }
        set
        {
            if (value == false) velocity.CurrentSpeed = 0;
            this.enable = value;
        }
    }

    public LayerMask GetGroundLayer()
    {
        return groundLayer;
    }
    Velocity velocity;
    Rigidbody2D rb;

    private void Start()
    {
        velocity = new Velocity();
        Acceleration = new Vector2(0,-0.5f);
        Decelerate = 1.0f;
        MaxSpeed = 25.0f;
        circleCollider = GetComponent<CircleCollider2D>();
        this.rb = GetComponent<Rigidbody2D>();
        standOnGround=this.GetComponent<StandOnGround>();
        this.enable = true;
    }

    public void FixedUpdate()
    {
        if (standOnGround)
        {
            IsOnGround = standOnGround.IsOnGround;
        }
        else
        {
            IsOnGround = CheckHitBottom(groundLayer);
        }

        if (IsOnGround || !enable)
        {
            velocity = Vector2.zero;
        }
        else
        {
            velocity += Acceleration;
            velocity.Adjust(Acceleration, MaxSpeed, Decelerate);
        }
        CurrentVelocity = velocity;
        this.rb.velocity += velocity;
    }

    private void Update()
    {


    }

    // 地面との衝突判定
    public RaycastHit2D CheckHitBottom(LayerMask target)
    {

        Vector3 circlePoint = this.transform.position + (Vector3)circleCollider.offset;
        float radius = circleCollider.radius;

        RaycastHit2D hit = Physics2D.CircleCast(circlePoint, radius, Acceleration.normalized, 0.05f, groundLayer);

        return hit;
    }
}
