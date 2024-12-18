using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gride : MonoBehaviour
{
    [System.Serializable]
    public class TerrainType
    {
        public LayerMask _terrainMask;
        public int _terrainPenalty;
    }
    [SerializeField] LayerMask _unWalkableMask;
    [SerializeField] Vector2 _gridWorldSize;
    [SerializeField] float _nodeRadius = 0.5f;
    [Header("가중치 설정")]
    [SerializeField] TerrainType[] _walkableRegions;
    [SerializeField] int _obstacleProximityPenalty = 10;
    [Header("Debug Display Param")]
    [SerializeField] bool _onlyDisplayPathGizmos = false;

    Dictionary<int, int> _walkableRegionsDictionary;
    LayerMask _walkableMask;
    int _penaltyMin = int.MaxValue;
    int _penaltyMax = int.MinValue;

    Node[,] _grid;

    float _nodeDiameter;
    int _gridSizeX, _gridSizeY;

    public List<Node> _path { get; set; }
    public int _maxSize
    {
        get
        {
            return _gridSizeX * _gridSizeY;
        }
    }

    private void Awake()
    {
        _nodeDiameter = _nodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter);
        
        _walkableRegionsDictionary = new Dictionary<int, int>();
        foreach(TerrainType region in _walkableRegions)
        {
            _walkableMask.value |= region._terrainMask.value;
            _walkableRegionsDictionary.Add((int)Mathf.Log(region._terrainMask.value,2),region._terrainPenalty);
        }

        //맵정보 생성
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
                
                int movementPenalty = 0;

                Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down);
                RaycastHit rHit;
                if(Physics.Raycast(ray, out rHit, 100, _walkableMask))
                {
                        _walkableRegionsDictionary.TryGetValue(rHit.collider.gameObject.layer, out movementPenalty);
                }
                if (!walkable)
                    movementPenalty += _obstacleProximityPenalty;

                _grid[x, y] = new Node(walkable, worldPoint, x, y, movementPenalty);
            }
        }

        // 여기서 길에 따른 점진적 가중치 설정
        BlurPenaltyMap(1);
        
    }

    void BlurPenaltyMap(int blurSize)
    {
        int kernelSize = blurSize * 2 + 1;
        int kernelExtents = (kernelSize - 1) / 2;

        int[,] penaltiesHorizontalPass = new int[_gridSizeX, _gridSizeY];
        int[,] penaltiesVerticalPass = new int[_gridSizeX, _gridSizeY];

        for (int y = 0; y < _gridSizeY; y++) 
        {
            for(int x = -kernelExtents; x <= kernelExtents; x++)
            {
                int sampleX = Mathf.Clamp(x, 0, kernelExtents);
                penaltiesHorizontalPass[0, y] += _grid[sampleX, y]._movementPenalty;
            }
            for(int x = 1; x < _gridSizeX; x++)
            {
                int removeIndex = Mathf.Clamp(x - kernelExtents - 1, 0, _gridSizeX);
                int addIndex = Mathf.Clamp(x + kernelExtents, 0, _gridSizeX - 1);

                penaltiesHorizontalPass[x, y] = penaltiesHorizontalPass[x - 1, y] 
                                                - _grid[removeIndex, y]._movementPenalty + _grid[addIndex, y]._movementPenalty;
            }
        }
        for(int x = 0; x < _gridSizeX; x++)
        {
            for(int y = -kernelExtents; y <= kernelExtents; y++)
            {
                int sampleY = Mathf.Clamp(y, 0, kernelExtents);
                penaltiesVerticalPass[x, 0] += penaltiesHorizontalPass[x, sampleY];
            }
            int blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, 0] / (kernelSize * kernelSize));
            _grid[x, 0]._movementPenalty = blurredPenalty;
            for(int y = 1; y < _gridSizeY; y++)
            {
                int removeIndex = Mathf.Clamp(y - kernelExtents - 1, 0, _gridSizeY);
                int addIndex = Mathf.Clamp(y - kernelExtents, 0, _gridSizeY - 1);

                penaltiesVerticalPass[x, y] = penaltiesVerticalPass[x, y - 1]
                    - penaltiesHorizontalPass[x, removeIndex] + penaltiesHorizontalPass[x, addIndex];
                blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, y] / (kernelSize * kernelSize));

                _grid[x, y]._movementPenalty = blurredPenalty;

                if (blurredPenalty > _penaltyMax)
                    _penaltyMax = blurredPenalty;
                if (blurredPenalty < _penaltyMin)
                    _penaltyMin = blurredPenalty;
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + _gridWorldSize.x / 2) / _gridWorldSize.x;
        float percentY = (worldPosition.z + _gridWorldSize.y / 2) / _gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);

        return _grid[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node._gridX + x;
                int checkY = node._gridY + y;
                if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY)
                    neighbours.Add(_grid[checkX, checkY]);
            }
        }
        return neighbours;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(_gridWorldSize.x, 1, _gridWorldSize.y));
        if (_onlyDisplayPathGizmos)
        {
            if (_grid != null)
            {
                foreach (Node node in _grid)
                {
                    Gizmos.color = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(_penaltyMin, _penaltyMax, node._movementPenalty));
                    Gizmos.color = node._walkable ? Gizmos.color : Color.black;
                    Gizmos.DrawCube(node._worldPosition, Vector3.one * (_nodeDiameter - 0.1f));
                }
            }
        }
        //else
        //{
        //    if (_grid != null)
        //    {
        //        foreach (Node node in _grid)
        //        {
        //            Gizmos.color = (node._walkable) ? Color.white : Color.red;
        //            if (_path != null)
        //            {
        //                if (_path.Contains(node))
        //                    Gizmos.color = Color.black;
        //            }
        //            Gizmos.DrawCube(node._worldPosition, Vector3.one * (_nodeDiameter - 0.1f));
        //        }
        //    }
        //}
    }
}
