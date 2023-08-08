using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayer : MonoBehaviour, ISelectable
{
    [SerializeField] private PlayableDirector director;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = PlayerInput.GetInstance();
    }
    public void StartTimeline()
    {
        director.Play();
        playerInput.canMove = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnSelect()
    {
        StartTimeline();
    }


    public void OnHoverEnter()
    {
    }

    public void OnHoverExit()
    {
 
    }
}
