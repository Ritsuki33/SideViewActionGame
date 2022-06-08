using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion2 : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;

    //着地できる傾斜のMAX
    [SerializeField] float maxSlope = 60.0f;

    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    State state;
    // Start is called before the first frame update
    void Start()
    {
        state = new Idle();
        rb = this.GetComponent<Rigidbody2D>();
        circleCollider = this.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void OnDrawGizmos()
    {
        circleCollider = this.GetComponent<CircleCollider2D>();
        Vector3 circlePoint = this.transform.position + (Vector3)circleCollider.offset;
        float radius = circleCollider.radius;
        Gizmos.DrawWireSphere(circlePoint - new Vector3(0, 0.1f, 0), radius);
    }

    private void FixedUpdate()
    {
        
        Debug.Log(this.transform.position.ToString() + this.rb.position.ToString());
        float dt = Time.deltaTime;
        float vx = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vx = 4.0f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            vx = -4.0f;
        }


        this.rb.velocity = new Vector3(vx, this.rb.velocity.y, 0);

        Vector3 circlePoint = this.transform.position + (Vector3)circleCollider.offset;
        float radius = circleCollider.radius;

        RaycastHit2D hit = Physics2D.CircleCast(circlePoint, radius, Vector2.down, 0.1f, targetLayer);

        CorrectVelocityOnGround();

        if (Input.GetKeyDown(KeyCode.Space))
        {
           this.Jump(5.0f);
        }

        //if (hit)
        //{
        //    float intervalY = circlePoint.y - hit.point.y - radius * hit.normal.y;

        //    // rb.MovePosition(this.transform.position - new Vector3(0, intervalY, 0));

        //    // 傾斜角度60度以上なら進めない。
        //    float slope = Vector3.Angle(Vector3.up, hit.normal);
        //    if (slope < 60)
        //    {
        //        rb.velocity = new Vector3(0, -intervalY / dt, 0);
        //        Vector2 onPlaneVelocity = Vector3.ProjectOnPlane(move, hit.normal);
        //        rb.velocity += onPlaneVelocity;
        //    }
        //    else
        //    {
        //        Vector2 onPlaneVelocity = Vector3.ProjectOnPlane(Vector3.down, hit.normal);
        //        rb.velocity += onPlaneVelocity * 2 / 3;
        //    }

        //    // ジャンプ
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        rb.velocity = new Vector2(rb.velocity.x, 0);
        //        rb.velocity += new Vector2(0, 5f);
        //    }

        //    Debug.Log(rb.velocity);
        //}
        //else
        //{
        //    //rb.AddForce(new Vector2(0, -9.8f));
        //     rb.velocity = new Vector3(vx, rb.velocity.y, 0);
        //}


    }

       // 踏んでいるかどうか
    public RaycastHit2D StepOn(CircleCollider2D collider, LayerMask target)
    {

        Vector3 circlePoint = this.transform.position + (Vector3)circleCollider.offset;
        float radius = circleCollider.radius;

        RaycastHit2D hit = Physics2D.CircleCast(circlePoint, radius, Vector2.down, 0.1f, targetLayer);

        return hit;
    }

    // 地上に立った時の移動補正
    void CorrectVelocityOnGround()
    {

        Vector2 circlePoint = this.rb.position + circleCollider.offset;
        float radius = circleCollider.radius;

        RaycastHit2D hit = StepOn(circleCollider, targetLayer);

        if (!hit)
        {
            return;
        }


        // rb.MovePosition(this.transform.position - new Vector3(0, intervalY, 0));

        // 傾斜角度60度以上なら進めない。
        float slope = Vector3.Angle(Vector3.up, hit.normal);
        if (slope < maxSlope)
        {
            Vector2 onPlaneVelocity = Vector3.ProjectOnPlane(this.rb.velocity, hit.normal);
            this.rb.velocity = onPlaneVelocity;
            float intervalY = circlePoint.y - hit.point.y - radius * hit.normal.y;
            this.rb.velocity += new Vector2(0, -intervalY / Time.deltaTime);

        }
        else
        {
            Vector2 onPlaneVelocity = Vector3.ProjectOnPlane(Vector3.down, hit.normal);
            this.rb.velocity += onPlaneVelocity * 2 / 3;
        }

       
        Debug.Log(rb.velocity);

    }

    void Jump(float jumpPower)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += new Vector2(0, jumpPower);
    }

    //　状態の抽象クラス
    abstract class State
    {
        public abstract State Update(Motion motion);
        public abstract void FixedUpdate(Motion motion);
    }

    class Idle : State
    {
        public Idle(){}

        public override State Update(Motion motion)
        {

            if (Input.GetKey(KeyCode.RightArrow))
            {
                return new Run();
            }

            return this;
        }
        public override void FixedUpdate(Motion motion)
        {

        }

    }

    class Float : State
    {
        public Float() { }

        public override State Update(Motion motion)
        {

            return this;
        }
        public override void FixedUpdate(Motion motion)
        {

        }

    }

    class Run : State
    {
        public Run() { }
        public override State Update(Motion motion)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                return this;
            }

            return new Idle();
        }
        public override void FixedUpdate(Motion motion)
        {
        }
    }
}
