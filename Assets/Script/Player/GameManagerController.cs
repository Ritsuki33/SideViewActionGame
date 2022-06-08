using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour, PlayerController
{
    [SerializeField] GameManager gameManager;
    private void Start()
    {
    }

    public bool RightRun()
    {
        return gameManager.CheckInput(GameManager.Input.RightRun);
    }

    public bool LeftRun()
    {
        return gameManager.CheckInput(GameManager.Input.LeftRun);
    }

    public bool RightDash()
    {
        return gameManager.CheckInput(GameManager.Input.RightDash);
    }

    public bool LeftDash()
    {
        return gameManager.CheckInput(GameManager.Input.LeftDash);
    }

    public bool Jump()
    {
        return gameManager.CheckInput(GameManager.Input.Jump);
    }
}
