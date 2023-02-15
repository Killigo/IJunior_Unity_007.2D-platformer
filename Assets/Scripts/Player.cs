using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _jumpForce = 450f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

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
        _animator.SetInteger("State", 0);

        if (!_isGrounded)
            _animator.SetInteger("State", 2);

        if (Input.GetButton("Horizontal"))
            Run();

        if (_isGrounded && Input.GetButton("Jump"))
            Jump();
    }

    private void Run()
    {
        if (_isGrounded)
            _animator.SetInteger("State", 1);

        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);
        _spriteRenderer.flipX = direction.x < 0.0f;
    }

    private void Jump()
    {
        _animator.SetInteger("State", 2);
        _rigidbody.AddForce(transform.up * _jumpForce);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        _isGrounded = colliders.Length > 1;
        Debug.Log("Коллайдеров: " + colliders.Length);
    }
}