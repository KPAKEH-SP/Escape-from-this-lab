using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private PlatfromPointsGizmos _path;
    [SerializeField] private float _speed = 1;
    private float _maxDistance = .1f;

    private IEnumerator<Transform> _pointInPath;

    private void Start()
    {
        if (_path == null)
        {
            Debug.Log("���� �� ������");
            return;
        }

        _pointInPath = _path.GetNextPoint();

        _pointInPath.MoveNext();

        if (_pointInPath.Current == null)
        {
            Debug.Log("� ���� ��� �����");
            return;
        }

        transform.position = _pointInPath.Current.position;
    }

    private void Update()
    {
        if (_pointInPath == null || _pointInPath.Current == null)
        {
            Debug.Log("���� �������");
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, _pointInPath.Current.position, _speed * Time.deltaTime);

        var distanceSquare = (transform.position - _pointInPath.Current.position).sqrMagnitude;

        if (distanceSquare < _maxDistance * _maxDistance)
        {
            _pointInPath.MoveNext();
        }
    }
}
