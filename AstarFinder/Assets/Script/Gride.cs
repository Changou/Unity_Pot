using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gride : MonoBehaviour
{
    [SerializeField] LayerMask _unWalkableMask;
    [SerializeField] Vector2 _gridWorldSize;
    [SerializeField] float _nodeRadius = 0.5f;

    Node[,] _grid;

    float _nodeDiameter;
    int _gridSizeX, _gridSizeY;

    private void Awake()
    {
        _nodeDiameter = _nodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter);

        //¸ÊÁ¤º¸ »ý¼º
        CreateGrid();
    }

    void CreateGrid()
    {
        _grid = new Node[_gridSizeX, _gridSizeY];
        Vector3 worldBottomLeft = 
            transform.position - Vector3.right * _gridWorldSize.x / 2 - Vector3.forward * _gridWorldSize.y / 2;

        for(int x = 0; x < _gridSizeX; x++)
        {
            for(int y = 0; y < _gridSizeY; y++)
            {
                Vector3 worldPoint = 
                    worldBottomLeft + Vector3.right * (x * _nodeDiameter + _nodeRadius) 
                                    + Vector3.forward * (y * _nodeDiameter + _nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, _nodeRadius, _unWalkableMask));
                _grid[x, y] = new Node(walkable, worldPoint);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(_gridWorldSize.x, 1, _gridWorldSize.y));
        if (_grid != null)
        {
            foreach(Node node in _grid)
            {
                Gizmos.color = (node._walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(node._worldPosition, Vector3.one * (_nodeDiameter - 0.1f));
            }
        }
    }
}
