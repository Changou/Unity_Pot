using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [SerializeField] Transform _seeker;
    [SerializeField] Transform _target;

    Gride _grid;

    private void Awake()
    {
        _grid = GetComponent<Gride>();
    }

    private void Update()
    {
        FindePath(_seeker.position, _target.position);
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA._gridX - nodeB._gridX);
        int dstY = Mathf.Abs(nodeA._gridY - nodeB._gridY);
        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);

        return 14 * dstX + 10 * (dstY - dstX);
    }

    void FindePath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = _grid.NodeFromWorldPoint(startPos);
        Node targetNode = _grid.NodeFromWorldPoint(targetPos);

        //List<Node> openSet = new List<Node>();
        Heap<Node> openSet = new Heap<Node>(_grid._maxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while(openSet._count > 0)
        {
            Node node = openSet.RemoveFirst();
            //for(int n = 1; n < openSet._count; n++)
            //{
            //    if(openSet[n].fCost < node.fCost || openSet[n].fCost == node.fCost)
            //    {
            //        if (openSet[n]._hCost < node._hCost)
            //        {
            //            node = openSet[n];
            //        }
            //    }
            //}
            //openSet.Remove(node);
            closedSet.Add(node);

            if(node == targetNode)
            {
                //길을 찾음. 그러니 path를 만들자.
                RetracePath(startNode, targetNode);
                return;
            }
            foreach(Node neighbour in _grid.GetNeighbours(node))
            {
                if (!neighbour._walkable || closedSet.Contains(neighbour))
                    continue;

                int newCostToNeighbour = node._gCost + GetDistance(node, neighbour);
                if(newCostToNeighbour < neighbour._gCost || !openSet.Contains(neighbour))
                {
                    neighbour._gCost = newCostToNeighbour;
                    neighbour._hCost = GetDistance(neighbour, targetNode);
                    neighbour._parent = node;
                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode._parent;
        }
        path.Reverse();

        _grid._path = path;
    }
}
