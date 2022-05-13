using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private GameObject _boxObj;

    private PlayerController _player;
    private bool _playerInZone;

    private void Update()
    {
        if (_playerInZone == true && Input.GetKeyDown(KeyCode.E))
        {
            _player.TakeItem(_boxObj, new Vector2(0, 0.453f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player = collision.gameObject.GetComponent<PlayerController>();
            _playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerInZone = false;
            _player = null;
        }
    }
}
