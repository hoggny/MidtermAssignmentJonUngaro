using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _moveSpeed; //Player Movement
    [SerializeField] private float _turnSpeed; //Camera Movement
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private bool _invertMouse; //Camera Movement
    [SerializeField] private float _gravity = -9.81f; //Player Movement
    [SerializeField] private float _jumpVelocity; //Player jump
    [SerializeField] private float _sprintMultiplier; //Player Movement
    [SerializeField] private float _yTurnMin; //Camera Movement
    [SerializeField] private float _yTurnMax; //Camera Movement

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck; //Player Movement
    [SerializeField] private LayerMask _groundMask; //Player Movement
    [SerializeField] private float _groundCheckDistance; //Player Movement

    [Header("Shoot")]
    [SerializeField] private Rigidbody _bulletPrefab; //Player Shoot
    [SerializeField] private Rigidbody _rocketPrefab; 
    [SerializeField] private Transform _spawnPoint; //Player Shoot
    [SerializeField] private float _shootForce; //Player Shoot

    [Header("Interact")]
    [SerializeField] private Camera _cam;
    [SerializeField] private float _interactionDistance;
    [SerializeField] private LayerMask _interactionLayer;

    [Header("Pick and Drop")]
    [SerializeField] private Transform _attachTransform;
    [SerializeField] private LayerMask _pickableLayer;
    [SerializeField] private float _pickableDistance;

    private CharacterController _characterController; //Player Movement

    private float _horizontal, _vertical; //Player Input
    private float _mouseX, _mouseY; //Player Input
    private float _camXRotation; //Camera Movement
    private Vector3 _playerVelocity; //Player Movement
    private bool _isGrounded; //Player Movement
    private float _moveMultiplier = 1; //Player Movement


    //Raycast
    private RaycastHit _raycastHit;
    private ISelectable _selectable;

    //Pick and Drop
    private bool _isPicked = false;
    private IPickable _pickable;
    void Start()
    {
        _characterController = GetComponent<CharacterController>(); //Player Movement

        //Hide Mouse
        Cursor.lockState = CursorLockMode.Locked; //Camera Movement
        Cursor.visible = false; //Camera Movement
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        GroundCheck();
        MovePlayer();
        TurnPlayer();
        Jump();
        Shoot();
        ShootRocket();

        Interact();
        PickAndDrop();
    }

    void GetInput() 
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _moveMultiplier = Input.GetButton("Sprint") ? _sprintMultiplier : 1;
    } //Done with Input

    void MovePlayer()
    {
        _characterController.Move((transform.forward * _vertical + transform.right * _horizontal) * _moveSpeed * Time.deltaTime);

        //Ground Check
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }
        //Set Player Velocity
        // v = u + a*t  v = g* t
        _playerVelocity.y += _gravity * Time.deltaTime;

        // V = 1/2 * a * t^2
        _characterController.Move(_playerVelocity * Time.deltaTime);

    } //Done With Player Movement

    void TurnPlayer()
    {
        // Player turn Movement
        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime * _mouseX);

        //Camera Up/Down Movement
        _camXRotation += Time.deltaTime * _mouseY * _turnSpeed * (_invertMouse ? 1 : -1);
        _camXRotation = Mathf.Clamp(_camXRotation, _yTurnMin, _yTurnMax);
        _cameraTransform.localRotation = Quaternion.Euler(_camXRotation, 0, 0);
    }//Done With Turn Player

    void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckDistance, _groundMask);
    }//Done With Player Movement and Ground Check

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))

            _playerVelocity.y = _jumpVelocity;        
        
    }//Done with Jump

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bulletRb = Instantiate(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);
            bulletRb.AddForce(_spawnPoint.forward * _shootForce, ForceMode.Impulse);
            Destroy(bulletRb.gameObject, 5f);
        }
    }//Done 

    void ShootRocket()
    {
        if (Input.GetButtonDown("Fire2")) 
        {
            Rigidbody rocketRB = Instantiate(_rocketPrefab, _spawnPoint.position, _spawnPoint.rotation);
            rocketRB.AddForce(_spawnPoint.forward * _shootForce, ForceMode.Impulse);
            Destroy(rocketRB.gameObject, 5f);
        }
    }//Done

    void Interact()
    {   
        //Get Ray details from middle of screen 
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray,out _raycastHit, _interactionDistance, _interactionLayer))
        {
            _selectable = _raycastHit.transform.GetComponent<ISelectable>();

            if (_selectable != null)
            {
                _selectable.OnHoverEnter();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _selectable.OnSelect();
                }
            }
        }

        if (_raycastHit.transform == null && _selectable != null)
        {
            _selectable.OnHoverExit();
            _selectable = null;
        }
    }//Done

    void PickAndDrop()
    {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray,out _raycastHit, _pickableDistance, _pickableLayer))
        {
            if (Input.GetKeyDown(KeyCode.E) && !_isPicked)
            {
                _pickable = _raycastHit.transform.GetComponent<IPickable>();
                if (_pickable == null) return;

                _pickable.OnPicked(_attachTransform);
                _isPicked = true;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && _isPicked && _pickable != null)
        {
            _pickable.OnDropped();
            _isPicked = false;
        }
    }//Done
}
