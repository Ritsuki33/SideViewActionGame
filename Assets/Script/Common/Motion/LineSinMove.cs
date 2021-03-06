using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//　2点間直線SIN波移動
public class LineSinMove: MonoBehaviour
{

    [SerializeField, Tooltip("角速度")] float speed;
    [SerializeField, Tooltip("距離")] float distance;
    [SerializeField, Tooltip("方向(y軸正方向から時計回り)")] float angle = 0;

    Rigidbody2D rb;
    Velocity velocity;

    Vector2 position;

    //sin波の制御制御
    float omega;

    // Start is called before the first frame update
    void Start()
    {
        rb=this.GetComponent<Rigidbody2D>();
        velocity = new Velocity();
        position = this.transform.position;
        omega = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = new Vector2(1.0f, 0);
        direction = Quaternion.Euler(0, 0, angle) * direction;
        omega += speed*Time.deltaTime;
        omega %= 360;

        // sin(at)をt(時間)で微分すると、a×cos(at)
        rb.velocity = speed* Mathf.Cos(omega) * distance * direction.normalized;
    }
}

