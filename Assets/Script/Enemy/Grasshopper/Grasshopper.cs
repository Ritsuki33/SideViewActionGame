using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Grasshopper : MonoBehaviour
{
    [SerializeField,Tooltip("ƒWƒƒƒ“ƒv•ûŒü")] Vector2 JumpVelocity;
    [SerializeField, Tooltip("…•½•ûŒüˆÚ“®")] Vector2 verticalVelocity;
    [SerializeField,Tooltip("‘Ò‹@ŠÔ")] float idleTime;

    Rigidbody2D rb;
    Animator animator;
    Jump jump;
    Gravity gravity;
    Move move;
    [SerializeField]CollisionTurn collisionTurn;
    bool isLeft;
    GrasshopperStateController grasshopperStateController;
    // Start is called before the first frame update
    void Start()
    {
        isLeft = true;
        animator = GetComponent<Animator>();
        jump = GetComponent<Jump>();
        gravity = GetComponent<Gravity>();
        rb=GetComponent<Rigidbody2D>();
        move = GetComponent<Move>();
        grasshopperStateController = new GrasshopperStateController();
        grasshopperStateController.Initalize(this, (int)GrasshopperStateController.StateType.Fall);
    }

    private void FixedUpdate()
    {
        this.grasshopperStateController.FixedUpdateSequence(this);
    }
    // Update is called once per frame
    void Update()
    {
        this.grasshopperStateController.UpdateSequence(this);

    }
    private void LateUpdate()
    {
        this.rb.velocity = Vector2.zero;
    }
}
