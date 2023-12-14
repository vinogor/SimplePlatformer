using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 3;

    private AnimatorHandler _animatorHandler;
    private GroundLocator _groundLocator;
    private Rigidbody2D _rigidbody2D;
    private bool _isOnGround;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animatorHandler = GetComponent<AnimatorHandler>();
        _groundLocator = GetComponent<GroundLocator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _animatorHandler.SetTriggerRun();
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _animatorHandler.SetTriggerRun();
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
        }

        // TODO: прыжки порой получаются с разной силой
        //       наверное потому что импульс успевает несколько раз пройти пока IsStandOnGround = true
        if (Input.GetKey(KeyCode.W) && _groundLocator.IsStandOnGround())
        {
            _rigidbody2D.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }
    }
}