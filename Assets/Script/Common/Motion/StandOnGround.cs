using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandOnGround : MonoBehaviour
{
    [SerializeField]bool isOnGround;

    [SerializeField, Tooltip("着地できる最大傾斜")] float maxSlope = 60.0f;
    [SerializeField, Tooltip("着地判定の大きさ")] float landJudgeDistance = 0.1f;
    [SerializeField, Tooltip("地上レイヤー")] LayerMask groundLayer;

    CircleCollider2D circleCollider;
    Rigidbody2D rb;
    RaycastHit2D hit;

    Vector2 circlePoint;

    Velocity velocity;
    float radius;

    //public bool Enable
    //{
    //    get;set;
    //}
    public bool IsOnGround
    {
        get {
            if (!enabled)
            {
                this.isOnGround = false;
                return false;
            }
            else return isOnGround;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = this.GetComponent<CircleCollider2D>();
        rb = this.GetComponent<Rigidbody2D>();
        velocity = new Velocity();
        //Enable = true;
    }

    private void FixedUpdate()
    {
        //if (!Enable) return;

        hit = CollideManager.StepOn(this.transform.position, circleCollider, landJudgeDistance, groundLayer);

        if (!hit)
        {
            this.isOnGround = false;
            return;
        }

        Vector2 circlePoint = this.rb.position + circleCollider.offset;
        float radius = circleCollider.radius;

        // 着地時速度補正
        float intervalY = Mathf.Floor((circlePoint.y - hit.point.y - radius * hit.normal.y) * 100) / 100;

        // 傾斜角度取得
        float slope = Vector3.Angle(Vector3.up, hit.normal);
        Debug.DrawLine(this.transform.position, this.transform.position + (Vector3)hit.normal,Color.red);
        if (slope < maxSlope)
        {
            this.isOnGround = true;
            Vector2 onPlaneVelocity = Vector3.ProjectOnPlane(this.rb.velocity, hit.normal);
            onPlaneVelocity += new Vector2(0, -intervalY / Time.deltaTime);
            velocity = onPlaneVelocity;


        }
        else
        {
            Vector2 onPlaneVelocity = Vector3.ProjectOnPlane(Vector3.down, hit.normal);
            velocity += onPlaneVelocity * 2 / 3;
        }

        Debug.DrawLine(this.transform.position, (Vector2)this.transform.position + velocity);

        this.rb.velocity = velocity;
    }

}
