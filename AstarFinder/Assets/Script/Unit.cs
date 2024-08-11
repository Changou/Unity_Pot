using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] Transform _target;

    float _speed = 20;
    Vector3[] _path;
    int _targetIndex;

    private void Start()
    {
        PathRequestManager.RequestPath(transform.position, _target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath,bool pathsuccessful)
    {
        if (pathsuccessful)
        {
            _path = newPath;
            _targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
    IEnumerator FollowPath()
    {
        Vector3 currentWayPoint = _path[0];
        while (true)
        {
            if(transform.position == currentWayPoint)
            {
                _targetIndex++;
                if(_targetIndex >= _path.Length)
                {
                    yield break;
                }
                currentWayPoint = _path[_targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWayPoint
                                                    , _speed * Time.deltaTime);
            yield return null;
        }
    }
    private void OnDrawGizmos()
    {
        if(_path != null)
        {
            for(int n = _targetIndex; n < _path.Length; n++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(_path[n], Vector3.one);

                if (n == _targetIndex)
                    Gizmos.DrawLine(transform.position, _path[n]);
                else
                    Gizmos.DrawLine(_path[n - 1], _path[n]);
            }
        }
    }
}
