using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour, PlayerController
{
    public bool RightRun()
    {
        return Input.GetKey(KeyCode.RightArrow);
    }
    public bool LeftRun()
    {
        return Input.GetKey(KeyCode.LeftArrow);

    }
    public bool RightDash()
    {
        return Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.RightArrow);

    }
    public bool LeftDash()
    {
        return Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftArrow);

    }
    public bool Jump()
    {
        return Input.GetKey(KeyCode.Space);
    }
}
