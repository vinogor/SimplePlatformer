using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorHandler : MonoBehaviour
{
    private static readonly int PlayerState = Animator.StringToHash("PlayerState");

    private Animator _animator;

    private PlayerHealth _playerHealth;

    private void OnEnable()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _playerHealth.HealthDecreased += HandleHealthDecreased;
    }

    private void OnDisable()
    {
        _playerHealth.HealthDecreased -= HandleHealthDecreased;
    }

    private void HandleHealthDecreased(float _)
    {
        SetDamaged();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetIdle()
    {
        _animator.SetInteger(PlayerState, 1);
    }

    public void SetRun()
    {
        _animator.SetInteger(PlayerState, 2);
    }

    public void SetJump()
    {
        _animator.SetInteger(PlayerState, 3);
    }

    public void SetAttack()
    {
        _animator.Play("PlayerAttack");
    }

    public void SetDamaged()
    {
        _animator.Play("PlayerDamaged");
    }
}