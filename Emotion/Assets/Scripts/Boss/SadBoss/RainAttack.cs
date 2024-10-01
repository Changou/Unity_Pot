  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainAttack : Pattern
{
    [SerializeField] Transform _SpawnPoint;
    [SerializeField] GameObject _prefab;
    [SerializeField] float _posX;
    [SerializeField] float _attackDelay = 0.5f;
    [SerializeField] float _duration = 5f;

    Vector3 _spawn;

    protected override IEnumerator Attack()
    {
        float time = 0;
        while (time < _duration) {
            float ranX = Random.Range(-_posX, _posX + 1);

            _spawn = new Vector3(ranX, _SpawnPoint.position.y, 0);
            GameObject projectile = Instantiate(_prefab);
            projectile.transform.position = _spawn;
            yield return new WaitForSeconds(_attackDelay);
            time += _attackDelay;

            yield return null;
        }
        PatternOn(false);
    }
}
