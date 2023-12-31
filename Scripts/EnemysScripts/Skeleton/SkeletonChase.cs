using UnityEngine;

public class SkeletonChase : StateMachineBehaviour
{
    [SerializeField] private float _chaseSpeed;
    [SerializeField] float _attackRange;
    [SerializeField] float _chaseRange = 10f;

    private Player _target;
    private Rigidbody2D _rigidbody2D;
    private Enemy _enemy;

    private int _animatorAttackHash;
    private int _animatorInVisionHash;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rigidbody2D = animator.GetComponent<Rigidbody2D>();
        _enemy = animator.GetComponent<Enemy>();
        _target = _enemy.GetTarget();

        _animatorAttackHash = Animator.StringToHash("Attack");
        _animatorInVisionHash = Animator.StringToHash("InVision");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_target == null)
            return;
        _enemy.Flip();
        _enemy.SetMovementTarget(_target.transform.position);

        Vector2 target = new Vector2(_target.transform.position.x, _rigidbody2D.position.y);
        Vector2 newPosition = Vector2.MoveTowards(_rigidbody2D.position, target, _chaseSpeed * Time.fixedDeltaTime);
        _rigidbody2D.MovePosition(newPosition);

        if (Vector2.Distance(_target.transform.position, _rigidbody2D.position) <= _attackRange)
        {
            animator.SetTrigger(_animatorAttackHash);
        }

        if (Vector2.Distance(_target.transform.position, _rigidbody2D.position) >= _chaseRange)
        {
            animator.SetBool(_animatorInVisionHash, false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(_animatorAttackHash);
    }
}
