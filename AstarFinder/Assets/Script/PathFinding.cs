using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

public class PathFinding : MonoBehaviour
{
    //[SerializeField] Transform _seeker;
    //[SerializeField] Transform _target;

    Gride _grid;

    private void Awake()
    {
        _grid = GetComponent<Gride>();
    }

    //private void Update()
    //{
    //    FindePath(_seeker.position, _target.position);
    //}

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = _grid.NodeFromWorldPoint(startPos);
        Node targetNode = _grid.NodeFromWorldPoint(targetPos);

        startNode._parent = startNode;

        if(startNode._walkable && targetNode._walkable)
        {
            Heap<Node> openSet = new Heap<Node>(_grid._maxSize);
            HashSet<Node> closeSet = new HashSet<Node>();
            openSet.Add(startNode);

            while(openSet._count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closeSet.Add(currentNode);
                if(currentNode == targetNode)
                {
                    sw.Stop();
                    print("Path found " + sw.ElapsedMilliseconds + " ms");
                    pathSuccess = true;
                    break;
                }

                foreach(Node neighbour in _grid.GetNeighbours(currentNode))
                {
                    if (!neighbour._walkable || closeSet.Contains(neighbour))
                        continue;

                    int newCostToNeighbour = currentNode._gCost + GetDistance(currentNode, neighbour) + neighbour._movementPenalty;
                    if (newCostToNeighbour < neighbour._gCost || !openSet.Contains(neighbour))
                    {
                        neighbour._gCost = newCostToNeighbour;
                        neighbour._hCost = GetDistance(neighbour, targetNode);
                        neighbour._parent = currentNode;
                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }

        yield return null;

        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }
        PathRequestManager._instance.FinishedProcessingPath(waypoints, pathSuccess);
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA._gridX - nodeB._gridX);
        int dstY = Mathf.Abs(nodeA._gridY - nodeB._gridY);
        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);

        return 14 * dstX + 10 * (dstY - dstX);
    }

    //void FindePath(Vector3 startPos, Vector3 targetPos)
    //{
    //    Node startNode = _grid.NodeFromWorldPoint(startPos);
    //    Node targetNode = _grid.NodeFromWorldPoint(targetPos);

    //    //List<Node> openSet = new List<Node>();
    //    Heap<Node> openSet = new Heap<Node>(_grid._maxSize);
    //    HashSet<Node> closedSet = new HashSet<Node>();
    //    openSet.Add(startNode);

    //    while(openSet._count > 0)
    //    {
    //        Node node = openSet.RemoveFirst();
    //        //for(int n = 1; n < openSet._count; n++)
    //        //{
    //        //    if(openSet[n].fCost < node.fCost || openSet[n].fCost == node.fCost)
    //        //    {
    //        //        if (openSet[n]._hCost < node._hCost)
    //        //        {
    //        //            node = openSet[n];
    //        //        }
    //        //    }
    //        //}
    //        //openSet.Remove(node);
    //        closedSet.Add(node);

    //        if(node == targetNode)
    //        {
    //            //길을 찾음. 그러니 path를 만들자.
    //            RetracePath(startNode, targetNode);
    //            return;
    //        }
    //        foreach(Node neighbour in _grid.GetNeighbours(node))
    //        {
    //            if (!neighbour._walkable || closedSet.Contains(neighbour))
    //                continue;

    //            int newCostToNeighbour = node._gCost + GetDistance(node, neighbour);
    //            if(newCostToNeighbour < neighbour._gCost || !openSet.Contains(neighbour))
    //            {
    //                neighbour._gCost = newCostToNeighbour;
    //                neighbour._hCost = GetDistance(neighbour, targetNode);
    //                neighbour._parent = node;
    //                if (!openSet.Contains(neighbour))
    //                    openSet.Add(neighbour);
    //            }
    //        }
    //    }
    //}

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> wayPoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        wayPoints.Add(path[0]._worldPosition);

        for(int n = 1; n< path.Count; n++)
        {
            Vector2 directionNew = new Vector2(path[n - 1]._gridX - path[n]._gridX
                                               , path[n - 1]._gridY - path[n]._gridY);
            if(directionNew != directionOld)
            {
                wayPoints.Add(path[n]._worldPosition);
            }
            directionOld = directionNew;
        }
        return wayPoints.ToArray();
    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode._parent;
        }
        Vector3[] wayPoints = SimplifyPath(path);
        Array.Reverse(wayPoints);

        return wayPoints;
    }
}
