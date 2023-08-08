using UnityEngine;

public abstract class TurretState
{
    protected Turret turret;

    protected TurretState(Turret turret)
    {
        this.turret = turret;
    }

    public abstract void EnterState();

    public abstract void Update();
}

public class IdleState : TurretState
{
    public IdleState(Turret turret) : base(turret)
    {
    }

    public override void EnterState()
    {
        // Hide the laser (assuming a LineRenderer is being used to draw the laser)
        turret.laserRenderer.enabled = false;
    }

    public override void Update()
    {
        // Implement scanning or idle behavior here
    }
}

public class AttackState : TurretState
{
    public AttackState(Turret turret) : base(turret)
    {
    }

    public override void EnterState()
    {
        // Show the laser
        turret.laserRenderer.enabled = true;
        Debug.Log("Attack State Entered");
    }

    public override void Update()
    {
        // Rotate the turret around its up axis
        turret.transform.Rotate(Vector3.up, turret.rotationSpeed * Time.deltaTime);

        // Calculate the direction of the beam
        Vector3 beamDirection = Quaternion.AngleAxis(-turret.beamAngle, turret.transform.right) * turret.transform.forward;

        Debug.DrawRay(turret.transform.position, beamDirection * turret.laserMaxDistance, Color.red);

        if (Physics.Raycast(turret.transform.position, beamDirection, out var hitInfo, turret.laserMaxDistance))
        {
            turret.laserRenderer.positionCount = 2;
            turret.laserRenderer.SetPosition(0, turret.transform.position);
            turret.laserRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            turret.laserRenderer.positionCount = 2;
            turret.laserRenderer.SetPosition(0, turret.transform.position);
            turret.laserRenderer.SetPosition(1, turret.transform.position + beamDirection * turret.laserMaxDistance);
        }

        // Fire the laser
        turret.FireLaser();
    }
}
