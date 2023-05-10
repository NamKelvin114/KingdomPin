using System.Collections;
using CodeStage.AdvancedFPSCounter;
using DG.Tweening;
using Pancake.GameService;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public LevelController LevelController;
    public GameState GameState;
    public PlaySoundEvent WinPopupSound;
    public PlaySoundEvent LostPopupSound;
    public AFPSCounter AFPSCounter => GetComponent<AFPSCounter>();

    void Awake()
    {
        Application.targetFrameRate = 80;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        ReturnHome();
        EventController.CurrentLevelChanged += UpdateScore;
    }

    public void PlayCurrentLevel()
    {
        WaitForPrepareGame();
        // PrepareLevel();
        // StartGame();
    }

    public void UpdateScore()
    {
        if (AuthService.Instance.isLoggedIn && AuthService.Instance.IsCompleteSetupName)
        {
            AuthService.UpdatePlayerStatistics("RANK_LEVEL", Data.CurrentLevel);
        }
    }

    private void FixedUpdate()
    {
        if (GameState == GameState.PlayingGame)
        {
            AdsManager.TotalTimesPlay += Time.deltaTime;
        }
    }

    public void PrepareLevel()
    {
        GameState = GameState.PrepareGame;
        LevelController.PrepareLevel();
    }

    public void ReturnHome()
    {
        PrepareLevel();
        DOTween.KillAll();
        PopupController.Instance.HideAll();
        PopupController.Instance.Show<PopupBackground>();
        PopupController.Instance.Show<PopupHome>();
    }

    public void ReplayGame()
    {
        WaitForPrepareGame();
        // PrepareLevel();
        // StartGame();
    }

    public void BackLevel()
    {
        Data.CurrentLevel--;
        WaitForPrepareGame();
        // PrepareLevel();
        // StartGame();
    }

    public void NextLevel()
    {
        Data.CurrentLevel++;
        WaitForPrepareGame();
        // PrepareLevel();
        // StartGame();
    }

    void WaitForPrepareGame()
    {
        PrepareLevel();
        StartGame();
    }

    public void StartGame()
    {
        FirebaseManager.OnStartLevel(Data.CurrentLevel, LevelController.Instance.CurrentLevel.gameObject.name);
        GameState = GameState.PlayingGame;
        PopupController.Instance.HideAll();
        DOTween.KillAll();
        PopupController.Instance.Show<PopupInGame>();
        LevelController.Instance.CurrentLevel.gameObject.SetActive(true);
    }

    public void OnWinGame(float delayPopupShowTime = 1.5f)
    {
        if (GameState == GameState.LoseGame || GameState == GameState.WinGame) return;
        GameState = GameState.WinGame;
        EventController.OnWinLevel?.Invoke();
        // Data setup

        // Effect and sounds

        // Event invoke
        LevelController.OnWinGame();
        DOTween.Sequence().AppendInterval(delayPopupShowTime).AppendCallback(() =>
        {
            FirebaseManager.OnWinGame(Data.CurrentLevel, LevelController.Instance.CurrentLevel.gameObject.name);
            AdsManager.TotalLevelWinLose++;
            Data.CurrentLevel++;
            PopupController.Instance.HideAll();
            PopupWin popupWin = PopupController.Instance.Get<PopupWin>() as PopupWin;
            popupWin.SetupMoneyWin(LevelController.CurrentLevel.BonusMoney);
            popupWin.Show();
            WinPopupSound.Raise();
        });
    }

    public void OnLoseGame(float delayPopupShowTime = 1.5f)
    {
        if (GameState == GameState.LoseGame || GameState == GameState.WinGame) return;
        GameState = GameState.LoseGame;
        EventController.OnLoseLevel?.Invoke();
        // Data setup

        // Effect and sounds

        // Event invoke
        DOTween.Sequence().AppendInterval(delayPopupShowTime).AppendCallback(() =>
        {
            FirebaseManager.OnLoseGame(Data.CurrentLevel, LevelController.Instance.CurrentLevel.gameObject.name);
            AdsManager.TotalLevelWinLose++;
            PopupController.Instance.Hide<PopupInGame>();
            PopupController.Instance.Show<PopupLose>();
            LostPopupSound.Raise();
        });
    }

    public void ChangeAFPSState()
    {
        if (Data.IsTesting)
        {
            AFPSCounter.enabled = !AFPSCounter.isActiveAndEnabled;
        }
    }
}

public enum GameState
{
    PrepareGame,
    PlayingGame,
    LoseGame,
    WinGame,
}