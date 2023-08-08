using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactor : MonoBehaviour
{
    protected PlayerInput _input;

    private void Awake()
    {
        _input = PlayerInput.GetInstance();
        Debug.Log("Awake called");
    }
    void Update()
    {
        Interact();
    }

    public abstract void Interact();
}
