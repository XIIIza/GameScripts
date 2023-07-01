using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(Movement), typeof(CombatSystem))]
public class Player : MonoBehaviour
{
    [Header("PlayerStats")]
    [SerializeField] private int _health;

    public bool IsGrounded = false;
    public bool IsBlocked = false;
    public Animator PlayerAnimator;

    private int _currentHealth;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction Dying;

    private int _animatorBlockedHash;
    private int _animatorHurtHash;
    private int _animatorDeadHash;

    private void Awake()
    {
        _currentHealth = _health;
        PlayerAnimator = GetComponent<Animator>();
        _animatorBlockedHash = Animator.StringToHash("Blocked");
        _animatorDeadHash = Animator.StringToHash("Dead");
        _animatorHurtHash = Animator.StringToHash("Hurt");
    }

    public void ApplyDamage(int damage)
    {
        if (IsBlocked)
            PlayerAnimator.SetTrigger("Blocked");

        else
        { 
            PlayerAnimator.SetTrigger("Hurt");
            _currentHealth -= damage;
            HealthChanged?.Invoke(_currentHealth, _health);
        }

        if (_currentHealth <= 0)
        {
            Dying?.Invoke();
            PlayerAnimator.SetTrigger("Dead");
            Destroy(gameObject);
        }
    }

    public void SetData(int health)
    {
        _currentHealth = health;

        HealthChanged?.Invoke(_currentHealth, _health);
    }

    public int GetData()
    {
        return _currentHealth;
    }

    public void UpdateStats()
    {
        HealthChanged?.Invoke(_currentHealth, _health);
    }
}