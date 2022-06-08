using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    //[SerializeField, Tooltip("プレイヤーの状態制御")] StateControllerBase<Player> stateController = default;
    StateControllerBase<Player> stateController;
    Rigidbody2D rb;
    Gravity gravity;
    Move move;
    Jump jump;
    StandOnGround standOnGround;
    VelocityControll velocityControll;
    [SerializeField,ReadOnly] Vector2 currentVelocity;
    [SerializeField]LayerMask groundLayer;

    CapsuleCollider2D capsuleCollider;

    public enum Controller{
        KeyBoard,
        GameManager
    }

    PlayerController currentController;
    Dictionary<int, PlayerController> controllers = new Dictionary<int, PlayerController>();
    Animator animator;

    bool isDead = false;

     Collider2D[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        move = this.GetComponent<Move>();
        standOnGround = this.GetComponent<StandOnGround>();
        jump = this.GetComponent<Jump>();
        gravity=this.GetComponent<Gravity>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

        stateController = new PlayerStateController();
        stateController.Initalize(this, (int)PlayerStateController.StateType.Idle);

        colliders = GetComponents<Collider2D>();
        controllers[(int)Controller.KeyBoard] = GetComponent<KeyboardController>();
        controllers[(int)Controller.GameManager] = GetComponent<GameManagerController>();
    }

    private void FixedUpdate()
    {
        stateController.FixedUpdateSequence(this);
    }
    // Update is called once per frame
    void Update()
    {
        stateController.UpdateSequence(this);

    }

    private void LateUpdate()
    {
        this.rb.velocity = Vector2.zero;
    }

    public void Dead()
    {
        this.isDead = true;
    }

    public int GetCurrentState()
    {
        return stateController.GetCurrentState();
    }

    public void ChangeController(Controller controller)
    {
       currentController = controllers[(int)controller];
    }
}
