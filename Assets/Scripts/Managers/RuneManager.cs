using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour
{
    public PressureRune[] runes;
    public GameObject portcullis;
    public UIManager UIManager;

    public void CheckRunes()
    {
        foreach (var rune in runes)
        {
            if (!rune.isRuneActivated) return;
        }

        RaisePortcullis();
        UIManager.UpdateInfoText("A portcullis has raised somewhere!");
        DeactivateRunes();
    }

    private void RaisePortcullis()
    {

        portcullis.transform.Translate(0, 10f, 0);
    }

    private void DeactivateRunes()
    {
        foreach (var rune in runes)
        {
            rune.DeactivateRune();
        }
    }
}
