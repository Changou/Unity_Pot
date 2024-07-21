using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool _walkable { get; set; }
    public Vector3 _worldPosition;
    public int _gridX { get; set; }
    public int _gridY { get; set; }
    
    public int _gCost{ get; set; }
    public int _hCost { get; set; }
    public int fCost { get { return _gCost + _hCost; } }

    public Node _parent;

    public Node (bool walkable, Vector3 worldPos, int gridX, int gridY)
    {
        _walkable = walkable;
        _worldPosition = worldPos;
        _gridX = gridX;
        _gridY = gridY;
    }
}
