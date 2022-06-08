using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : VelocityControll
{
  

    private new void Start()
    {
        base.Start();
        this.acceleration = -1f;
        this.maxVelocity =3.0f;
    }

    private void FixedUpdate()
    {
        //CurrentVelocity += this.acceleration*Time.deltaTime;

        //this.rb.velocity = CurrentVelocity;
    }
}
