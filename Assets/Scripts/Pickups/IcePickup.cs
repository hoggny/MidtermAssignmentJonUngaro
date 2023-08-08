using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePickup : MonoBehaviour
{
    private PlayerInput playerInput;
    public UIManager uIManager;

    private void Awake()
    {
        playerInput = PlayerInput.GetInstance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickupWeapon();
            Destroy(gameObject);
        }
    }

    private void PickupWeapon()
    {
        playerInput.weapon1Available = true;
        uIManager.UpdateInfoText("You got the ice bolt! Click left mouse");

    }
}
