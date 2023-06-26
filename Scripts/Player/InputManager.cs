using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private InputSystem _input;
    private Movement _movement;
    private CombatSystem _combatSystem;
    private static InputManager _instance;

    private bool _isInteracted = false;
    private bool _isSubmite = false;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
            Destroy(_instance);
        }

        _instance = this;
        _input = new InputSystem();
        _movement = GetComponent<Movement>();
        _combatSystem = GetComponent<CombatSystem>();
    }

    public static InputManager GetInstance()
    {
        return _instance;
    }

    public bool GetInteractPressed()
    {
        bool result = _isInteracted;
        _isInteracted = false;
        return result;
    }

    public bool GetSubmitPressed()
    {
        bool result = _isSubmite;
        _isSubmite = false;
        return result;
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Move.performed += ctx => OnMovePerformed(ctx.ReadValue<Vector2>());
        _input.Player.Move.canceled += ctx => OnMoveCanceled();
        _input.Player.Jump.performed += _ => OnJumpPerformed();
        _input.Player.Attack.performed += _ => AttackState();
        _input.Player.Block.performed += _ => BlockPerfromed();
        _input.Player.Block.canceled += _ => BlockCanceled();
        _input.Player.Interact.performed += _ => OnInteractPerformed();
        _input.Player.Interact.canceled += _ => OnInteractCanceled();
        _input.Player.Submit.performed += _ => OnSubmitPerformed();
        _input.Player.Submit.canceled += _ => OnSubmiteCanceled();
        _input.Player.OnClick.performed += _ => OnClick();
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Move.performed -= ctx => OnMovePerformed(ctx.ReadValue<Vector2>());
        _input.Player.Move.canceled -= ctx => OnMoveCanceled();
        _input.Player.Jump.performed -= _ => OnJumpPerformed();
        _input.Player.Attack.performed -= _ => AttackState();
        _input.Player.Block.performed -= _ => BlockPerfromed();
        _input.Player.Block.canceled -= _ => BlockCanceled();
        _input.Player.Interact.performed -= _ => OnInteractPerformed();
        _input.Player.Interact.canceled -= _ => OnInteractCanceled();
        _input.Player.Submit.performed -= _ => OnSubmitPerformed();
        _input.Player.Submit.canceled -= _ => OnSubmiteCanceled();
        _input.Player.OnClick.performed -= _ => OnClick();
    }

    private void OnMovePerformed(Vector2 value) { _movement.OnMovePerformed(value); }
    private void OnMoveCanceled() { _movement.OnMoveCanceled(); }
    private void OnJumpPerformed() { _movement.JumpPerformed(); }
    private void OnInteractPerformed() { _isInteracted = true; }
    private void OnInteractCanceled() { _isInteracted = false; }
    private void OnSubmitPerformed() { _isSubmite = true; }
    private void OnSubmiteCanceled() { _isSubmite = false; }
    private void BlockCanceled() { _combatSystem.BlockState(false); }
    private void OnClick() { }

    private void AttackState()
    {
        if (!DialogueManager.GetInstance().IsDialoguePlay)
            _combatSystem.AttackState();
    }

    private void BlockPerfromed()
    {
        if (!DialogueManager.GetInstance().IsDialoguePlay)
            _combatSystem.BlockState(true);
    }
}