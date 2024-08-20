using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectileAttack : Pattern
{
    [SerializeField] GameObject _prefab;
    [SerializeField] Transform _target;
    [SerializeField] float _speed;
    [SerializeField] Transform _trans;

    private void OnEnable()
    {
        GameObject projectile = Instantiate(_prefab);
        projectile.transform.position = _trans.position;
        projectile.GetComponent<Rigidbody2D>().isKinematic = true;

        float target_dist = Vector3.Distance(_trans.position, _target.position);

        target_dist /= 3;

        Vector3 dir = _target.position - _trans.position;

        Vector3 target = _target.position;

        if(dir.x < 0)
        {
            target_dist *= -1;
        }

        projectile.transform.DOMoveX(_trans.position.x + target_dist, _speed).SetEase(Ease.InQuad).OnComplete(()=>
        projectile.transform.DOMoveX(target.x, _speed).SetEase(Ease.OutSine));
        
        projectile.transform.DOMoveY(_trans.position.y + 2, _speed).SetEase(Ease.OutQuad).OnComplete(()=>
        projectile.transform.DOMoveY(target.y - 2, _speed).SetEase(Ease.InQuad));
    }
}
