using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public class PlayerState_Dead : StateChildBase<Player>
    {
        // “üêˆ—
        public override void OnEnter(Player player)
        {
            Debug.Log("Game Over");
            player.animator.SetBool("IsDead", true);
            player.standOnGround.enabled = false;
            player.move.Input(Move.Direction.None);
            foreach (Collider2D c in player.colliders)
            {
                c.enabled = false;
            }
            player.jump.Input(true);
        }

        public override int StateFixedUpdate(Player player)
        {
            player.jump.Input(false) ;
            return this.stateType;
        }

        // ‘Şêˆ—
        public override void OnExit(Player player) { }
    }
}