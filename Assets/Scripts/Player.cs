using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _jumpForce = 5f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private int _stateHash = Animator.StringToHash("State");
    private const string _horizontal = "Horizontal";
    private bool _isGrounded;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        _animator.SetInteger(_stateHash, 0);

        if (!_isGrounded)
            _animator.SetInteger(_stateHash, 2);

        if (Input.GetButton(_horizontal))
            Run();

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void Run()
    {
        if (_isGrounded)
            _animator.SetInteger(_stateHash, 1);

        Vector3 direction = transform.right * Input.GetAxis(_horizontal);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);
        _spriteRenderer.flipX = direction.x < 0.0f;
    }

    private void Jump()
    {
        _animator.SetInteger(_stateHash, 2);
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        _isGrounded = colliders.Length > 1;
    }
}