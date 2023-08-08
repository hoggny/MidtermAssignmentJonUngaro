using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementBehaviour))]
public class PlayerJumpBehaviour : Interactor
{
    [Header("Jump")]
    [SerializeField] private float _jumpVelocity;

    private PlayerMovementBehaviour _moveBehaviour;


    private void Start()
    {
        _moveBehaviour = GetComponent<PlayerMovementBehaviour>();
        Debug.Log("Start Called");
    }
    public override void Interact()
    {
        //if (_moveBehaviour == null)
        //    _moveBehaviour = GetComponent<PlayerMovementBehaviour>();


        if (_input.jumpPressed && _moveBehaviour.isGrounded)
            _moveBehaviour.SetYVelocity(_jumpVelocity);
    }


}
