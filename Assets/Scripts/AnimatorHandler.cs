using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    // TODO: ??? private + const = CamelCase ?
    private const string ParameterName = "PlayerState";
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetIdle()
    {
        _animator.SetInteger(ParameterName, 1);
    }

    public void SetRun()
    {
        _animator.SetInteger(ParameterName, 2);
    }

    public void SetJump()
    {
        _animator.SetInteger(ParameterName, 3);
    }

    public void SetAttack()
    {
        _animator.SetInteger(ParameterName, 4);
    }
}