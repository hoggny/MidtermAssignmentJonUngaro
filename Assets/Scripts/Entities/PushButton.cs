using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    //[SerializeField] private Material _defaultColour;
    //[SerializeField] private Material _hoverColour;
    //[SerializeField] private MeshRenderer _renderer;

    public UnityEvent onPush;
    public UnityEvent onHoverEnter;
    public UnityEvent onHoverExit;
    public void OnHoverEnter()
    {
        //_renderer.material = _hoverColour;
        onHoverEnter?.Invoke();
    }

    public void OnHoverExit()
    {
        //_renderer.material = _defaultColour;
        onHoverExit?.Invoke();
    }

    public void OnSelect()
    {
        onPush?.Invoke();
    }


}
