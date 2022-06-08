using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    Player player;
    private void Start()
    {
        player = this.transform.root.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.Dead();
    }
}
