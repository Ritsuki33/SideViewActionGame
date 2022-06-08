using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//@2“_ŠÔ’¼üSIN”gˆÚ“®
public class LineSinMove: MonoBehaviour
{

    [SerializeField, Tooltip("Šp‘¬“x")] float speed;
    [SerializeField, Tooltip("‹——£")] float distance;
    [SerializeField, Tooltip("•ûŒü(y²³•ûŒü‚©‚çŒv‰ñ‚è)")] float angle = 0;

    Rigidbody2D rb;
    Velocity velocity;

    Vector2 position;

    //sin”g‚Ì§Œä§Œä
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

        // sin(at)‚ğt(ŠÔ)‚Å”÷•ª‚·‚é‚ÆAa~cos(at)
        rb.velocity = speed* Mathf.Cos(omega) * distance * direction.normalized;
    }
}

