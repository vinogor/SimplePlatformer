using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AnimatorHandler), typeof(GroundLocator))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private int jumpForce = 8;

    private SpriteRenderer _renderer;
    private AnimatorHandler _animatorHandler;
    private GroundLocator _groundLocator;
    private Rigidbody2D _rigidbody2D;
    private bool _isOnGround;

    // TODO: !!! добавить анмиацию прыжка
    // TODO: + добавить разворот анимации когде бежит/прыгает в другую сторону

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animatorHandler = GetComponent<AnimatorHandler>();
        _groundLocator = GetComponent<GroundLocator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            // Debug.Log("input key D");
            _renderer.flipX = false;
            _animatorHandler.SetTriggerRun();
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            // Debug.Log("input key A");
            _renderer.flipX = true;
            _animatorHandler.SetTriggerRun();
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
        }

        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     Debug.Log("input key W");
        // }

        if (Input.GetKeyDown(KeyCode.W) && _groundLocator.IsStandOnGround())
        {
            // Debug.Log("input key W + IsStandOnGround = true");
            _rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}