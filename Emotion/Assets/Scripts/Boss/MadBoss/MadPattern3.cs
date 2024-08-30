using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadPattern3 : Pattern
{
    [SerializeField] Transform[] _spawnPoints;

    private void OnEnable()
    {
        Vector3 pos = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;

        transform.GetComponent<BossMad>().Spawn(pos, BossMad.PATTERNTYPE.SPAWN);
        PatternOn(false);
    }
}
