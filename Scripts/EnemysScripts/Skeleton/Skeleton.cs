using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : StateMachineBehaviour
{
    [SerializeField] private float _patrolSpeed;
    [SerializeField] private int _detectRange;

    private Transform[] _patrolPoints;
    private Enemy _enemy;
    private Rigidbody2D _rigidBody2D;
    private int _currentPoint = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemy = animator.GetComponent<Enemy>();
        _patrolPoints = _enemy.GetPatrolPoints();
        _rigidBody2D = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Patrol(animator);
    }

    private void Patrol(Animator animator)
    {
        _enemy.SetMovementTarget(_patrolPoints[_currentPoint].position);
        Vector2 lookSide = _enemy.Flip();
        Detect(animator, lookSide);

        Transform target = _patrolPoints[_currentPoint];

        _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, target.position, _patrolSpeed * Time.deltaTime);

        if (_enemy.transform.position == target.position)
        {
            _currentPoint++;

            if (_currentPoint >= _patrolPoints.Length)
            {
                _currentPoint = 0;
            }
        }
    }

    private void Detect(Animator animator, Vector2 lookSide)
    {
        RaycastHit2D hit = Physics2D.Raycast(_enemy.transform.position, lookSide, _detectRange, LayerMask.GetMask("Player"));

        if (hit.collider != null)
        {
            animator.SetBool("InVision", true);
        }

        Debug.DrawRay(_enemy.transform.position, lookSide * _detectRange, Color.red);

    }
}