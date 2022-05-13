using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Box" && player._itemInHands == false || collision.gameObject.tag == "Platform")
        {
            player.isGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Box" && player._itemInHands == false || collision.gameObject.tag == "Platform")
        {
            player.isGround = false;
        }
    }
}
