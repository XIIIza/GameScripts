using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Player _targetPlayer;

    [Header("PatrolSettings")]
    public Transform[] _path;

    public Player Target => _targetPlayer;
    public bool FacingRight;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction Dying;

    private Vector3 _movementTarget;
    private Animator _animator;
    private int _currentHealth;
    private Vector2 _lookside = Vector2.right;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("IsAlive", true);
        _currentHealth = _health;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("I Hurt");
        _animator.SetTrigger("Hurt");
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
        {
            _animator.SetBool("IsAlive", false);
            Dying?.Invoke();
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
            StartCoroutine(EnemyDying());
        }
    }

    public Vector2 Flip()
    {
        Vector3 facing = transform.localScale;

        facing.z *= -1f;

        if (transform.position.x > _movementTarget.x && FacingRight)
        {
            transform.localScale = facing;
            transform.Rotate(0f, 180f, 0f);
            FacingRight = false;
            _lookside = Vector2.left;
        }

        else if (transform.position.x < _movementTarget.x && !FacingRight)
        {
            transform.localScale = facing;
            transform.Rotate(0f, 180f, 0f);
            FacingRight = true;
            _lookside = Vector2.right;
        }

        return _lookside;
    }

    public Player GetTarget()
    {
        return _targetPlayer;
    }

    public Transform[] GetPatrolPoints()
    {
        return _path;
    }

    public void SetMovementTarget(Vector3 movementTarget)
    {
        _movementTarget = movementTarget;
    }

    public void SetReady()
    {
        _animator.SetBool("IsReady", true);
    }

    private IEnumerator EnemyDying()
    {
        _animator.SetTrigger("Die");
        yield return new WaitForSeconds(3f);
        
        Destroy(gameObject);
    }
}
