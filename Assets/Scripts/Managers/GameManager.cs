using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager[] _levels;

    public static GameManager _instance;

    private GameState _currentstate;
    private LevelManager _currentLevel;
    private int _currentLevelIndex = 0;
    private bool _isInputActive = true;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }

    public static GameManager GetInstance()
    {
        return _instance;
    }
    public bool IsInputActive()
    {
        return _isInputActive;
    }
    private void Start()
    {
        if (_levels.Length > 0)
        {
            ChangeState(GameState.Briefing, _levels[_currentLevelIndex]);
        }
    }
    //Change Current Game State

    public void ChangeState(GameState state, LevelManager level)
    {
        _currentstate = state;
        _currentLevel = level;

        Debug.Log("Current state is " + _currentstate);

        switch (_currentstate)
        {
            case GameState.Briefing:
                StartBriefing();
                Debug.Log("Game state is briefing");
                break;
            case GameState.LevelStart:
                InitiateLevel();
                Debug.Log("Game state is start");
                break;
            case GameState.LevelIn:
                RunLevel();
                Debug.Log("Game state is level in");
                break;
            case GameState.LevelEnd:
                CompleteLevel();
                Debug.Log("Game state is level end");
                break;
            case GameState.GameOver:
                GameOver();
                Debug.Log("Game state is game over");
                break;
            case GameState.GameEnd:
                GameEnd();
                Debug.Log("Game state is game end");
                break;
        }
    }
    private void StartBriefing()
    {
        Debug.Log("Briefing Started....");

        //Disable Player Input
        _isInputActive = false;

        //Start The Level
        ChangeState(GameState.LevelStart, _currentLevel);
    }

    private void InitiateLevel()
    {
        Debug.Log("Level Started");

        _isInputActive = true;

        _currentLevel.StartLevel();
        ChangeState(GameState.LevelIn, _currentLevel);
    }

    private void RunLevel()
    {
        Debug.Log("The current level running is " + _currentLevel.gameObject.name);
    }

    private void CompleteLevel()
    {
        Debug.Log("Level Ends here");

        //Go TO The NExt Level
        ChangeState(GameState.LevelStart, _levels[++_currentLevelIndex]);
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
    }

    private void GameEnd()
    {
        Debug.Log("Game has ended, you win!");
    }

    public LevelManager GetCurrentLevel()
    {
        return _currentLevel;
    }
    public enum GameState
    {
        Briefing,
        LevelStart,
        LevelIn,
        LevelEnd,
        GameOver,
        GameEnd
    }
}
