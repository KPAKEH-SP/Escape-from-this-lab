using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    private bool _playerInZone;
    private bool _leverUsed;
    private SpriteRenderer _sr;

    [SerializeField] private DoorController _doorController;
    [SerializeField] private List<GameObject> _lights;
    [SerializeField] private Sprite _usedLeverTexture;
    [SerializeField] private Sprite _defaultLeverTexture;
    [SerializeField] private bool _trueLever;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_playerInZone == true && Input.GetKeyDown(KeyCode.E))
        {
            //
            //Включение
            //
            if (_leverUsed == false)
            {
                if (_trueLever == true)
                {
                    _doorController.usedLeverCount++;
                }

                else
                {
                    _doorController.usedLeverCount--;
                }

                _doorController.CheckLevers();
                _leverUsed = true;
                _sr.sprite = _usedLeverTexture;
                //
                //Включение света
                //
                if (_lights != null)
                {
                    foreach (var i in _lights)
                    {
                        i.SetActive(true);
                    }
                }
            }
            //
            //Выключение
            //
            else
            {
                if (_trueLever == true)
                {
                    _doorController.usedLeverCount--;
                }

                else
                {
                    _doorController.usedLeverCount++;
                }

                _doorController.CheckLevers();
                _leverUsed = false;
                _sr.sprite = _defaultLeverTexture;
                //
                //Выключение света
                //
                if (_lights != null)
                {
                    foreach (var i in _lights)
                    {
                        i.SetActive(false);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerInZone = false;
        }
    }
}
