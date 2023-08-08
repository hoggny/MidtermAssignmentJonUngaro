using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorKey : MonoBehaviour
{
    public UnityEvent onKeyPicked;
    public UIManager UImanager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onKeyPicked?.Invoke();
            Destroy(gameObject);
            UImanager.UpdateInfoText("An arcane lock releases...");
        }
    }
}
