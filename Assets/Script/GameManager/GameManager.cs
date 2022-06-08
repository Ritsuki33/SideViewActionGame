using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject ganeOverObject;
    [SerializeField] CameraManager cameraManager;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = playerObject.GetComponent<Player>();
        player.ChangeController(Player.Controller.KeyBoard);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetCurrentState() == (int)Player.PlayerStateController.StateType.Dead)
        {

            ganeOverObject.SetActive(true);
            cameraManager.enabled = false;
        }
    }


    byte playerInput;
    public enum Input
    {
        None = 0,
        RightRun = 1,
        LeftRun = 2,
        RightDash = 4,
        LeftDash = 8,
        Jump = 16
    }

    public void PlayerInput(Input input)
    {
        playerInput = (byte)input;
    }

    public bool CheckInput(Input input)
    {
        return (playerInput & (byte)input) != 0;
    }
}
