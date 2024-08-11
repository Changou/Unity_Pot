using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequestManager : MonoBehaviour
{
    static PathRequestManager _uniqueInstance;

    struct PathRequest
    {
        public Vector3 _pathStart;
        public Vector3 _pathEnd;
        public Action<Vector3[], bool> _callBack;

        public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> callback)
        {
            _pathStart = start;
            _pathEnd = end;
            _callBack = callback;
        }
    }

    Queue<PathRequest> _pathRequsestQueue;
    PathRequest _currentPathRequest;
    PathFinding _pathFinding;
    bool _isProcessingPath;

    public static PathRequestManager _instance { get { return _uniqueInstance; } }

    private void Awake()
    {
        _pathRequsestQueue = new Queue<PathRequest>();
        _uniqueInstance = this;
        _pathFinding = GetComponent<PathFinding>();
    }


    void TryProcessNext()
    {
        if (!_isProcessingPath && _pathRequsestQueue.Count > 0)
        {
            _currentPathRequest = _pathRequsestQueue.Dequeue();
            _isProcessingPath = true;
            //여기서 길찾기 시작

            _pathFinding.StartFindPath(_currentPathRequest._pathStart, _currentPathRequest._pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        _currentPathRequest._callBack(path, success);
        _isProcessingPath = false;
        TryProcessNext();
    }

    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart,pathEnd,callback);
        _instance._pathRequsestQueue.Enqueue(newRequest);
        _instance.TryProcessNext();
    }
}
