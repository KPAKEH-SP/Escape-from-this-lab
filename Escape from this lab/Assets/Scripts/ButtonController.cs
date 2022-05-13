using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private bool _player;
    [SerializeField] private bool _box;

    [SerializeField] private bool _pistonActivate;
    [SerializeField] private bool _doorActivate;
    [SerializeField] private bool _dropBox;

    [SerializeField] private List<DoorController> _doors;
    [SerializeField] private List<GameObject> _lights;

    [SerializeField] private Animator _pistonAnim;

    [SerializeField] private GameObject _boxPrefab;
    [SerializeField] private Transform _spawnPos;

    [SerializeField] private Animator _anim;

    private bool _buttonUsed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _player == true || collision.gameObject.tag == "Box" && _box == true)
        {
            _anim.Play("Activate");

            if (_doorActivate == true)
            {
                for (var i = 0; i < _doors.Count; i++)
                {
                    _doors[i].pressedButtonsCount++;
                    _doors[i].CheckButtons();
                }
            }

            if (_pistonActivate == true)
            {
                _pistonAnim.Play("PistonUp");
            }

            if (_dropBox == true && _buttonUsed == false)
            {
                GameObject newBox = Instantiate(_boxPrefab);
                newBox.transform.position = _spawnPos.position;
                _buttonUsed = true;
            }

            if (_lights != null)
            {
                foreach (var i in _lights)
                {
                    i.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _player == true || collision.gameObject.tag == "Box" && _box == true)
        {
            _anim.Play("Deactivate");

            if (_doorActivate == true)
            {
                for (var i = 0; i < _doors.Count; i++)
                {
                    _doors[i].pressedButtonsCount--;
                    _doors[i].CheckButtons();
                }
            }

            if (_lights != null)
            {
                foreach (var i in _lights)
                {
                    i.SetActive(false);
                }
            }
        }
    }
}
