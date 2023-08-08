using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementBehaviour : MonoBehaviour
{

    private PlayerInput _input;

    [Header("Player Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _sprintMultiplier;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundCheckDistance;


    private CharacterController _characterController;

    private Vector3 _playerVelocity;

    private float _moveMultiplier = 1;

    public bool isGrounded { get; private set; }
    void Start()
    {
        _input = PlayerInput.GetInstance();
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        MovePlayer();
    }

    void MovePlayer()
    {
        _characterController.Move((transform.forward * _input.vertical + transform.right * _input.horizontal) * _moveSpeed * Time.deltaTime);

        //Ground Check
        if (isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }
        //Set Player Velocity
        // v = u + a*t  v = g* t
        _playerVelocity.y += _gravity * Time.deltaTime;

        // V = 1/2 * a * t^2
        _characterController.Move(_playerVelocity * Time.deltaTime);

    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckDistance, _groundMask);
    }

    public void SetYVelocity(float value)
    {
        _playerVelocity.y = value;
    }

    public float GetForwardSpeed()
    {
        return _input.vertical * _moveSpeed * _moveMultiplier;
    }
}
