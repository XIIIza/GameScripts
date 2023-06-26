using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstContact : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _runAwayPoint;
    [SerializeField] private float _speed;

    private WaitForSeconds _timeBewtweenShoot = new WaitForSeconds(1f);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _spriteRenderer.flipX = false;
            _rigidbody2D.velocity += Vector2.right * _speed;
            _animator.SetFloat("Speed", _rigidbody2D.velocity.x);
        }

        else if (collision.TryGetComponent<Door>(out Door door))
            Destroy(gameObject);
    }
}
