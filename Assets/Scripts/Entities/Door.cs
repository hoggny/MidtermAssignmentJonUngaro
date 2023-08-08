using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, ISelectable
{

    [SerializeField] private Animator _doorAnim;

    private bool _isLocked = true;
    public UIManager uIManager;

    public void LockDoor()
    {
        _isLocked = true;
    }

    public void UnlockDoor()
    {
        _isLocked = false;
        Debug.Log("Door is Unlocked");
    }

    public void OnSelect()
    {
        Debug.Log("Door Selected");
        if (!_isLocked)
        {
            _doorAnim.SetBool("open", true);
        }
        else
        {
            uIManager.UpdateInfoText("You must release the arcane lock first");
        }    
        
    }

    public void OnHoverEnter()
    {
        Debug.Log("Hover Enter");
    }

    public void OnHoverExit()
    {
        Debug.Log("Hover Exit");
    }
}
