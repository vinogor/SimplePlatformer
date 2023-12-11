using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 3;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private bool _isOnGround;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _animator.SetTrigger("Run");
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _animator.SetTrigger("Run");
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
        }

        // TODO: прыжки порой получаются с разной силой
        //       наверное потому что импульс успевает несколько раз пройти пока _isOnGround = true
        if (Input.GetKey(KeyCode.W) && _isOnGround)
        {
            _rigidbody2D.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _isOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _isOnGround = false;
        }
    }
}