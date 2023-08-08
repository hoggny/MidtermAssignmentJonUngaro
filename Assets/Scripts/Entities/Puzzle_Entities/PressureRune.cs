using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureRune : MonoBehaviour
{
    [SerializeField] private GameObject rune;
    public bool isRuneActivated;
    private Renderer runeRenderer;
    private Color originalColor;
    private Color customColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    public RuneManager runeManager;
    private Collider runeCollider;

    void Start()
    {
        runeRenderer = rune.GetComponent<Renderer>();
        runeCollider = rune.GetComponent<Collider>();
        originalColor = runeRenderer.material.GetColor("_EmissionColor");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeActivationStatus();
            runeManager.CheckRunes();
        }
    }

    private void ChangeActivationStatus()
    {
        if (isRuneActivated)
        {
            runeRenderer.material.SetColor("_EmissionColor", originalColor);
            isRuneActivated = false;
        }
        else
        {
            runeRenderer.material.SetColor("_EmissionColor", customColor);
            isRuneActivated = true;
        }
    }

    public void DeactivateRune()
    {
        runeCollider.enabled = false;
        runeRenderer.material.SetColor("_EmissionColor", customColor);
    }
}
