using UnityEngine;

public class FirstContact : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _spriteRenderer.flipX = false;
            _rigidbody2D.velocity += Vector2.right * _speed;
        }

        else if (collision.TryGetComponent<Door>(out Door door))
            Destroy(gameObject);
    }
}