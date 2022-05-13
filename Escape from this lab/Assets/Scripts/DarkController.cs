using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DarkController : MonoBehaviour
{
    [SerializeField] private bool _dontOffRoom;
    [SerializeField] private GameObject _roomObj;

    private Animator _darkAnimator;
    private PlayerController _playerController;
    
    private void Start()
    {
        //Инициализация полей

        _darkAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Отключение тьмы

            _darkAnimator.Play("DarkOff");
            _roomObj.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Включение тьмы

            _darkAnimator.Play("DarkOn");

            if (!_dontOffRoom)
            {
                _roomObj.SetActive(false);
            }
        }
    }
}
