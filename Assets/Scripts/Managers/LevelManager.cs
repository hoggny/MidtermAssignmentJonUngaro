using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool _isFinalLevel;

    public UnityEvent _onLevelStart;
    public UnityEvent _onLevelEnd;


    public void StartLevel()
    {
        Debug.Log("Level has started");
        _onLevelStart?.Invoke();
    }

    public void EndLevel()
    {
        Debug.Log("Level has ended");
        _onLevelEnd?.Invoke();

        if (_isFinalLevel)
        {
            GameManager.GetInstance().ChangeState(GameManager.GameState.GameEnd, this);
        }
        else
        {
            GameManager.GetInstance().ChangeState(GameManager.GameState.LevelEnd, this);
        }
    }

    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        // This will quit the application
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
