using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    [SerializeField] private Vector3 attackOffset;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask attackMask;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Enemy _enemy;

    private Vector2 _direction;
    private Quaternion _quaternion;

    public void Attack()
    {
        Vector3 position = transform.position;
        position += transform.right * attackOffset.x;
        position += transform.up * attackOffset.y;

        Collider2D collider = Physics2D.OverlapCircle(position, attackRange, attackMask);

        if (collider != null)
        {
            collider.GetComponent<Player>().ApplyDamage(attackDamage);
        }
    }

    public void Shoot()
    {
        GetDirection();

        Bullet bullet = Instantiate(_bullet, _shootPoint.position, _quaternion).GetComponent<Bullet>();
        bullet.Init(_direction);
    }

    private void GetDirection()
    {
        if (!_enemy.FacingRight)
        {
            _direction = Vector2.left;
            _quaternion = Quaternion.Euler(0, 0, 0);
        }

        else
        {
            _direction = Vector2.right;
            _quaternion = Quaternion.Euler(0, 180, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 position = transform.position;
        position += transform.right * attackOffset.x;
        position += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(position, attackRange);
    }
}