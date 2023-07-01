using UnityEngine;

public class Hunter : StateMachineBehaviour
{

    [SerializeField] private float _speed = 2.5f;
    [SerializeField] private float attackRange = 1f;

    private float _timeBetweenShoot;
    private Player _target;
    private Rigidbody2D _rigidbody2D;
    private Enemy _enemy;

    private int _animatorAttackHash;
    private int _animatorShootHash;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rigidbody2D = animator.GetComponent<Rigidbody2D>();
        _enemy = animator.GetComponent<Enemy>();
        _target = _enemy.GetTarget();

        _animatorAttackHash = Animator.StringToHash("Attack");
        _animatorShootHash = Animator.StringToHash("Shoot");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_target == null)
            return;

        _timeBetweenShoot += Time.deltaTime;
        _enemy.Flip();
        _enemy.SetMovementTarget(_target.transform.position);

        Vector2 target = new Vector2(_target.transform.position.x, _rigidbody2D.position.y);
        Vector2 newPosition = Vector2.MoveTowards(_rigidbody2D.position, target, _speed * Time.fixedDeltaTime);
        _rigidbody2D.MovePosition(newPosition);

        if (Vector2.Distance(_target.transform.position, _rigidbody2D.position) <= attackRange)
        {
            animator.SetTrigger(_animatorAttackHash);
        }

        if(_timeBetweenShoot >= 3)
        {
            animator.SetTrigger(_animatorShootHash);
            _timeBetweenShoot = 0;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(_animatorAttackHash);
        animator.ResetTrigger(_animatorShootHash);
    }
}