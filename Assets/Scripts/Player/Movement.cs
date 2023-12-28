using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AnimatorHandler), typeof(GroundLocator))]
[RequireComponent(typeof(SpriteRenderer), typeof(AttackEnemy))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int jumpForce;

    private SpriteRenderer _renderer;
    private AnimatorHandler _animatorHandler;
    private GroundLocator _groundLocator;
    private Rigidbody2D _rigidbody2D;
    private AttackEnemy _attackEnemy;
    private bool _isOnGround;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animatorHandler = GetComponent<AnimatorHandler>();
        _groundLocator = GetComponent<GroundLocator>();
        _attackEnemy = GetComponent<AttackEnemy>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _renderer.flipX = false;
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _renderer.flipX = true;
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W");
        }

        if (Input.GetKeyDown(KeyCode.W) && _groundLocator.IsStandOnGround())
        {
            Debug.Log("W + ground");
            _rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _attackEnemy.Hit();
        }
    }

    private void Update()
    {
        _isOnGround = _groundLocator.IsStandOnGround();

        if (_isOnGround == false)
        {
            _animatorHandler.SetJump();
        }

        else if (_isOnGround && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)))
        {
            _animatorHandler.SetRun();
        }
        else if (_isOnGround && Input.GetKey(KeyCode.Space))
        {
            _animatorHandler.SetAttack();
        }
        else
        {
            _animatorHandler.SetIdle();
        }
    }
}