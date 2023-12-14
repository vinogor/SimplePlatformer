using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetTriggerRun()
    {
        _animator.SetTrigger("Run");
    }
}