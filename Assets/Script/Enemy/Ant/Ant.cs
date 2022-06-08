using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Ant : MonoBehaviour
{
    StateControllerBase<Ant> stateController;
    Rigidbody2D rb;

    Move move;
    Gravity gravity;
    StandOnGround standOnGround;
    CriffCheck criffCheck;
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        move = this.GetComponent<Move>();
        gravity = this.GetComponent<Gravity>();
        criffCheck=this.GetComponent<CriffCheck>();
        standOnGround=this.GetComponent<StandOnGround>();
        stateController = new AntStateController();
        stateController.Initalize(this, (int)AntStateController.StateType.Float);

        // オブジェクト状態初期化
        this.move.Acceleration = 1.0f;
        this.move.Decelerate = 0.5f;
        this.move.MaxSpeed = 2.5f;

        this.move.Input(Move.Direction.Left);
      
    }

    // Update is called once per frame
    void FixedUpdate()
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
}
