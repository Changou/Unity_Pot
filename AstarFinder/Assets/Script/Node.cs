using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public bool _walkable { get; set; }
    public Vector3 _worldPosition;
    public int _gridX { get; set; }
    public int _gridY { get; set; }
    
    public int _gCost{ get; set; }
    public int _hCost { get; set; }
    public int fCost { get { return _gCost + _hCost; } }

    public Node _parent;

    int _heapIdx;

    public int _heapIndex
    {
        get { return _heapIdx; }
        set { _heapIdx = value; }
    }

    public Node (bool walkable, Vector3 worldPos, int gridX, int gridY)
    {
        _walkable = walkable;
        _worldPosition = worldPos;
        _gridX = gridX;
        _gridY = gridY;
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
            compare = _hCost.CompareTo(nodeToCompare._hCost);

        return -compare;
    }
}
