using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private Transform _nextLift;
    [SerializeField] private Transform _liftRoom;

    private bool _playerInZone;
    private GameObject _playerObj;
    private Animator _animator;

    private void Start()
    {
        //Инициализация полей

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerInZone == true)
        {
            StartCoroutine(Teleport());
        }
    }

    //Присутствие игрока возле лифта

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerInZone = true;
            _playerObj = collision.gameObject;
            _animator.Play("LiftOpen");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerInZone = false;
            _animator.Play("LiftClose");
        }
    }

    //Перемещение

    private IEnumerator Teleport()
    {
        _playerObj.GetComponent<PlayerController>().ChangeMoveState();
        _playerObj.transform.position = _liftRoom.position;
        Debug.Log("Игрок в комнатне лифта");
        yield return new WaitForSeconds(5);
        _playerObj.GetComponent<PlayerController>().ChangeMoveState();
        _playerObj.transform.position = _nextLift.position;
        Debug.Log("Игрок вышел");
    }
}
