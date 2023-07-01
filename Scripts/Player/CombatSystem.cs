using UnityEngine;
using UnityEngine.Events;

public class CombatSystem : MonoBehaviour
{
    [Header("AttackPoint Settings")]
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _enemyLayer;

    [Header("Stamina Settings")]
    [SerializeField] private int _maxStamina;
    [SerializeField] private int _staminaPerAttack;
    [SerializeField] private int _staminaRestoreCount;
    [SerializeField] private float _delayStaminaRestore;
    private int _currentStamina;

    [Header("Attack Settings")]
    [SerializeField] private int _damage;
    [SerializeField] private float _minDelayBetweenAttack;
    [SerializeField] private float _maxDelayBetweenCombo;
    private float _timeBetweenAttack;
    private float _comboCount;

    private Player _player;
    private int _animatorFirstAttackHash;
    private int _animatorSecondAttackHash;
    private int _animatorThirdAttackHash;
    private int _animatorIsBlockedHash;

    public event UnityAction<int, int> StaminaChanged;

    private void Start()
    {
        _player = GetComponent<Player>();
        _currentStamina = _maxStamina;

        _animatorFirstAttackHash = Animator.StringToHash("Attack1");
        _animatorSecondAttackHash = Animator.StringToHash("Attack2");
        _animatorThirdAttackHash = Animator.StringToHash("Attack3");
        _animatorIsBlockedHash = Animator.StringToHash("isBlocked");
    }

    private void FixedUpdate()
    {
        _timeBetweenAttack += Time.deltaTime;

        if (_timeBetweenAttack >= _maxDelayBetweenCombo)
            _comboCount = 0;

        if (_timeBetweenAttack >= _delayStaminaRestore && _currentStamina != _maxStamina)
            StaminaRestore();
    }

    public void BlockState(bool state)
    {
        _player.IsBlocked = state;
        _player.PlayerAnimator.SetBool(_animatorIsBlockedHash, state);
    }

    public void AttackState()
    {
        if (_minDelayBetweenAttack <= _timeBetweenAttack && _comboCount == 0)
            Attack(1, _animatorFirstAttackHash);

        else if (_minDelayBetweenAttack <= _timeBetweenAttack && _comboCount == 1)
            Attack(2, _animatorSecondAttackHash);

        else if (_minDelayBetweenAttack <= _timeBetweenAttack && _comboCount == 2)
            Attack(3, _animatorThirdAttackHash);
    }

    private void Attack(int attackModifire, int animatorHash)
    {
        _player.PlayerAnimator.SetTrigger(animatorHash);
        _currentStamina -= _staminaPerAttack;

        if (_currentStamina <= 0)
            _currentStamina = 0;

        _timeBetweenAttack = 0;
        _comboCount++;

        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);

        foreach (Collider2D hitEnemy in hitEnemys)
        {
            if (hitEnemy.TryGetComponent(out Enemy enemy))
            {
               enemy.TakeDamage(_damage * attackModifire * StaminaCoofificent());
            }
        }

        if (_comboCount == 3)
            _comboCount = 0;

        StaminaChanged?.Invoke(_currentStamina, _maxStamina);
    }

    private int StaminaCoofificent()
    {
        float cooficient = _currentStamina;

        if(cooficient >= 80)
        {
            return 4;
        }

        else if (cooficient >= 60 && cooficient < 80)
        {
            return 3;
        }

        else if (cooficient >= 30 && cooficient < 60)
        {
            return 2;
        }

        else
        {
            return 1;
        }
    }

    private void StaminaRestore()
    {
        _currentStamina += _staminaRestoreCount;

        if(_currentStamina > _maxStamina)
        {
            _currentStamina = _maxStamina;
        }

        StaminaChanged?.Invoke(_currentStamina, _maxStamina);
    }
}