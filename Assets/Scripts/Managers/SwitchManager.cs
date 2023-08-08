using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    public Switch[] switches;
    public GameObject chest;
    public UIManager UIManager;
    private bool isFirstTimeActivated = true;
    public bool isSolutionStateOn = true;

    public void CheckSwitches()
    {
        // Check if all switches are in the desired "on" state
        foreach (var switchObject in switches)
        {
            if (switchObject.isActivated != isSolutionStateOn) return;
        }

        RevealChest();
        if (isFirstTimeActivated)
        {
            UIManager.UpdateInfoText("A rune appears on the floor...");
            isFirstTimeActivated = false;
        }
    }

    private void RevealChest()
    {
        chest.SetActive(true);
    }
}
