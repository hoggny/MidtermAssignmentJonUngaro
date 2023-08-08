using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public LineRenderer laserRenderer;
    public float laserMaxDistance = 100.0f;
    public float rotationSpeed = 20f;
    public float beamAngle = 30f;
    [SerializeField] private float damage = 10f;
    private bool _attack = true;
    public float damageCooldown = 1.0f; // damage every 1 second
    private float lastDamageTime;


    private TurretState idleState;
    private TurretState attackState;
    private TurretState state;


    void Start()
    {
        laserRenderer = GetComponent<LineRenderer>();
        idleState = new IdleState(this);
        attackState = new AttackState(this);
        state = _attack ? attackState : idleState;
        state.EnterState();
    }

    void Update()
    {
        state.Update();

        // Switch states when conditions are met
        // E.g. switch to Attack state if an enemy is spotted
        if (_attack && state != attackState)
        {
            state = attackState;
            state.EnterState();

        }
        // E.g. switch back to Idle state if no enemies are in sight
        else if (!_attack && state != idleState)
        {
            state = idleState;
            state.EnterState();
        }
    }

    public void FireLaser()
    {
        // Calculate the direction of the beam
        Vector3 beamDirection = Quaternion.AngleAxis(-beamAngle, transform.right) * transform.forward;

        // Perform a raycast
        if (Physics.Raycast(transform.position, beamDirection, out var hitInfo, laserMaxDistance))
        {
            // Set the laser's start and end points
            laserRenderer.positionCount = 2;
            laserRenderer.SetPosition(0, transform.position);
            laserRenderer.SetPosition(1, hitInfo.point);

            // If the raycast hit the player, deduct health
            Health playerHealth = hitInfo.transform.GetComponent<Health>();
            if (playerHealth != null)
            {
                if (Time.time >= lastDamageTime + damageCooldown)
                {
                    Debug.Log("Direct hit!");
                    playerHealth.DeductHealth(damage);
                    lastDamageTime = Time.time;
                }
            }
        }
        else
        {
            // If the raycast didn't hit anything, draw the laser to its maximum distance
            laserRenderer.positionCount = 2;
            laserRenderer.SetPosition(0, transform.position);
            laserRenderer.SetPosition(1, transform.position + beamDirection * laserMaxDistance);
        }
    }
}
