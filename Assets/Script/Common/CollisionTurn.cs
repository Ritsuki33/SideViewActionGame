using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTurn : MonoBehaviour
{
    [SerializeField]bool isLeft=true;
    bool exit=true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!exit) return;
        isLeft ^= true;
        Debug.Log(isLeft);
        exit = false;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("exit");

        exit = true;

    }

    public bool CheckIsLeft()
    {
        return isLeft;
    }
}
