using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PC : MonoBehaviour
{
    [SerializeField] private List<GameObject> _controlledObject;
    [SerializeField] private List<Transform> _focusList;
    [SerializeField] private GameObject _player;
    private bool _playerInZone;

    [Header("Настройки движения")]

    [SerializeField] private float _speed;
    [SerializeField] private float _maxY, _minY;
    private bool _canMoveUp, _canMoveDown; 
    private bool _canRotateRight, _canRotateLeft;

    [Header("Настройки интерфейса")]

    [SerializeField] private GameObject _canvas;

    [Header("Настройки камеры")]

    [SerializeField] private CinemachineVirtualCamera _camera;
    private int _selectedObectId = 0;

    [Header("Настройки колеса")]

    [SerializeField] private bool _circle;
    [SerializeField] private List<float> _trueCirclePos;
    [SerializeField] private List<GameObject> _lights;
    [SerializeField] private GameObject _door;
    [SerializeField] private float _workRange;
    private List<bool> _completeCircles = new List<bool> { false, false, false };
    private List<float> _objectZRotation = new List<float> { 0f, 0f, 0f };

    private void Update()
    {
        //Движение

        ObjectBehavior.MoveUp(_speed, _maxY, _controlledObject[_selectedObectId], _canMoveUp);
        ObjectBehavior.MoveDown(_speed, _minY, _controlledObject[_selectedObectId], _canMoveDown);

        ObjectBehavior.RotationRight(_speed, _controlledObject[_selectedObectId], _canRotateRight, _selectedObectId, _objectZRotation);
        ObjectBehavior.RotationLeft(_speed, _controlledObject[_selectedObectId], _canRotateLeft, _selectedObectId, _objectZRotation);

        //Управление интерфейсом

        InterfaceBehavior.OpenInterface(_canvas, _player, _camera, _focusList[_selectedObectId], _playerInZone);

        //Проверки

        CheckCircle();
    }

    //Изменение состояний движения

    public void ChangeMoveUpState()
    {
        _canMoveUp = !_canMoveUp;
    }

    public void ChangeMoveDownState()
    {
        _canMoveDown = !_canMoveDown;
    }

    public void ChangeRotateRightState()
    {
        _canRotateRight = !_canRotateRight;
    }

    public void ChangeRotateLeftState()
    {
        _canRotateLeft = !_canRotateLeft;
    }

    //Управление фокусировки камеры

    public void NextFocus()
    {
        if (_selectedObectId < _focusList.Count-1)
        {
            _selectedObectId++;
            Debug.Log(_selectedObectId);
            Debug.Log(_focusList.Count);
        }

        else
        {
            _selectedObectId = 0;
        }

        _camera.Follow = _focusList[_selectedObectId];
    }

    public void BackFocus()
    {
        if (_selectedObectId > 0)
        {
            _selectedObectId--;
        }

        else
        {
            _selectedObectId = _focusList.Count - 1;
        }

        _camera.Follow = _focusList[_selectedObectId];
    }

    //Управление интерфейсом

    public void ClosePC()
    {
        InterfaceBehavior.CloseInterface(_canvas, _player, _camera);
    }

    //Отслеживание игрока

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //_player = collision.gameObject;
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

    //Проверка колеса

    private void CheckCircle()
    {
        if (_circle == true)
        {
            if (_objectZRotation[_selectedObectId] <= _trueCirclePos[_selectedObectId] + _workRange && 
                _objectZRotation[_selectedObectId] >= _trueCirclePos[_selectedObectId] - _workRange)
            {
                _lights[_selectedObectId].SetActive(true);
                _completeCircles[_selectedObectId] = true;

                if (_completeCircles[0] == true && _completeCircles[1] == true && _completeCircles[2] == true)
                {
                    _door.SetActive(false);
                }

                else
                {
                    _door.SetActive(true);
                }
            }

            else
            {
                _lights[_selectedObectId].SetActive(false);
                _completeCircles[_selectedObectId] = false;

                if (_completeCircles[0] == true && _completeCircles[1] == true && _completeCircles[2] == true)
                {
                    _door.SetActive(false);
                }

                else
                {
                    _door.SetActive(true);
                }
            }
        }
    }
}

public static class ObjectBehavior
{
    private static float _objectYPosition;

    public static void MoveUp(float speed, float maxY, GameObject controlledObject, bool canMove)
    {
        if (_objectYPosition < maxY && canMove)
        {
            controlledObject.transform.position = new Vector2(controlledObject.transform.position.x, controlledObject.transform.position.y + speed * Time.deltaTime);
            _objectYPosition += speed * Time.deltaTime;
        }
    }

    public static void MoveDown(float speed, float minY, GameObject controlledObject, bool canMove)
    {
        if (_objectYPosition > minY && canMove)
        {
            controlledObject.transform.position = new Vector2(controlledObject.transform.position.x, controlledObject.transform.position.y - speed * Time.deltaTime);
            _objectYPosition -= speed * Time.deltaTime;
        }
    }

    public static void RotationRight(float speed, GameObject controlledObject, bool canRotate, int objectId, List<float> objectZRotation)
    {
        if (objectZRotation[objectId] < 360 && canRotate)
        {
            controlledObject.transform.Rotate(0, 0, -speed * Time.deltaTime);
            objectZRotation[objectId] += speed * Time.deltaTime;
            //Debug.Log(objectZRotation[0] + ":" + objectZRotation[1] + ":" + objectZRotation[2]);
        }
    }

    public static void RotationLeft(float speed, GameObject controlledObject, bool canRotate, int objectId, List<float> objectZRotation)
    {
        if (objectZRotation[objectId] > 0 && canRotate)
        {
            controlledObject.transform.Rotate(0, 0, speed * Time.deltaTime);
            objectZRotation[objectId] -= speed * Time.deltaTime;
            //Debug.Log(objectZRotation[0] + ":" + objectZRotation[1] + ":" + objectZRotation[2]);
        }
    }
}

public static class InterfaceBehavior
{
    public static void OpenInterface(GameObject canvas, GameObject player, CinemachineVirtualCamera camera, Transform focusObject, bool canOpen)
    {
        if (canOpen == true && Input.GetKeyDown(KeyCode.E))
        {
            canvas.SetActive(true);
            player.GetComponent<PlayerController>().ChangeMoveState();
            camera.Follow = focusObject;
        }
    }

    public static void CloseInterface(GameObject canvas, GameObject player, CinemachineVirtualCamera camera)
    {
        canvas.SetActive(false);
        player.GetComponent<PlayerController>().ChangeMoveState();
        camera.Follow = player.transform;
    }
}