using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, ISelectable
{
    [SerializeField] private GameObject switchObject;
    [SerializeField] private List<Switch> neighbors;

    public bool isActivated;
    private Renderer switchRenderer;
    private Color originalColor;
    private Color customColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    public UIManager UIManager;
    [SerializeField] private SwitchManager SwitchManager;

    // Start is called before the first frame update
    void Start()
    {
        switchRenderer = switchObject.GetComponent<Renderer>();
        originalColor = switchRenderer.material.GetColor("_EmissionColor");
    }

    public void OnSelect()
    {
        ChangeActivationStatus(false);
    }

    public void ChangeActivationStatus(bool isNeighbor)
    {
        isActivated = !isActivated;
        switchRenderer.material.SetColor("_EmissionColor", isActivated ? customColor : originalColor);

        // Change the state of the neighbors as well, but do not propagate further
        if (!isNeighbor)
        {
            foreach (Switch neighbor in neighbors)
            {
                neighbor.ChangeActivationStatus(true);
            }
        }
        SwitchManager.CheckSwitches();
    }

    public void OnHoverEnter()
    {
    }

    public void OnHoverExit()
    {
    }
}
