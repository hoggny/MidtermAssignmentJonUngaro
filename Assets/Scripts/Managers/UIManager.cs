using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;

    [Header("UI Elements")]
    public TMP_Text txtHealth;
    public GameObject gameOverText;
    public TMP_Text txtInfo;
    // Start is called before the first frame update
    void Start()
    {
        gameOverText.SetActive(false);
    }

    private void OnEnable()
    {
        _playerHealth.OnHealthUpdated += OnHealthUpdate;
        _playerHealth.OnDeath += OnDeath;
    }

    private void OnDestroy()
    {
        _playerHealth.OnHealthUpdated -= OnHealthUpdate;
    }

    void OnHealthUpdate(float health)
    {
        txtHealth.text = "Health: " + Mathf.Floor(health).ToString();    }

    void OnDeath()
    {
        gameOverText.SetActive(true);
    }

    public void UpdateInfoText(string message)
    {
        txtInfo.text = message;
        Invoke("ClearInfoText", 5.0f);
    }

    private void ClearInfoText()
    {
        txtInfo.text = "";
    }
}
