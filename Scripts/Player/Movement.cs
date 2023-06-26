using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private bool _facingRight = true;
    private Vector2 _direction;
    private Rigidbody2D _rigidBody2D;

    private void Start()
    {
        _player = GetComponent<Player>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (DialogueManager.GetInstance().IsDialoguePlay || _player.IsBlocked)
        {
            _rigidBody2D.velocity = Vector2.zero;
            _player.PlayerAnimator.SetFloat("Speed", Mathf.Abs(_direction.x));
        }
        else
        {
            _player.IsGrounded = Physics2D.OverlapCircle(_groundCheck.transform.position, 0.1f, _groundLayer);
            _rigidBody2D.velocity = new Vector2(_direction.x * _moveSpeed, _rigidBody2D.velocity.y);
            _player.PlayerAnimator.SetBool("isGrounded", _player.IsGrounded);
            _player.PlayerAnimator.SetFloat("Speed", Mathf.Abs(_direction.x));
        }
    }

    public void OnMovePerformed(Vector2 direction)
    {
        FlipCheck(direction.x);

        _direction = direction;
    }

    public void OnMoveCanceled()
    {
        _direction = Vector2.zero;
    }

    public void JumpPerformed()
    {
        if (DialogueManager.GetInstance().IsDialoguePlay)
            return;

        if (_player.IsGrounded)
        {
            _rigidBody2D.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            _player.PlayerAnimator.SetTrigger("Jump");
        }
    }

    private void FlipCheck(float direction)
    {
        if (direction > 0 && !_facingRight) Flip();

        if (direction < 0 && _facingRight) Flip();
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        _facingRight = !_facingRight;
    }
}