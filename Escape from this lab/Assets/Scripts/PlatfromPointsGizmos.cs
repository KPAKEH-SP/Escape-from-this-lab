using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromPointsGizmos : MonoBehaviour
{
    private enum PathTypes
    {
        linear,
        loop
    }

    [SerializeField] private int _direction = 1;
    [SerializeField] private int _moveTo = 0;
    [SerializeField] private Transform[] _points;
    [SerializeField] private PathTypes _pathType;

    private void OnDrawGizmos()
    {
        if (_points == null || _points.Length < 2)
        {
            return;
        }

        for (var i = 1; i < _points.Length; i++)
        {
            //
            // Отрисовка всех линий
            //
            Gizmos.DrawLine(_points[i - 1].position, _points[i].position);
        }

        if (_pathType == PathTypes.loop)
        {
            //
            // Отрисовка замкнутого пути
            //
            Gizmos.DrawLine(_points[0].position, _points[_points.Length - 1].position);
        }
    }

    public IEnumerator<Transform> GetNextPoint()
    {
        if (_points == null || _points.Length < 1)
        {
            yield break;
        }

        while (true)
        {
            yield return _points[_moveTo];

            if (_points.Length == 1)
            {
                continue;
            }

            if (_pathType == PathTypes.linear)
            {
                if (_moveTo <= 0)
                {
                    _direction = 1;
                }

                else if (_moveTo >= _points.Length - 1)
                {
                    _direction = -1;
                }
            }

            _moveTo = _moveTo + _direction;

            if (_pathType == PathTypes.loop)
            {
                if (_moveTo >= _points.Length)
                {
                    _moveTo = 0;
                }

                if (_moveTo < 0)
                {
                    _moveTo = _points.Length - 1;
                }
            }
        }
    }
}
