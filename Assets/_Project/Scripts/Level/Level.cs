using Pancake;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private CompleteLevelEvent completeLevelEvent;
    public PlayMusicEvent IngameMusic;
    [ReadOnly] public int BonusMoney;
    private bool _isFingerDown;
    private bool _isFingerDrag;
    [SerializeField] public int Target;
#if UNITY_EDITOR
    [Button]
    private void StartLevel()
    {
        Data.CurrentLevel = Utility.GetNumberInAString(gameObject.name);

        EditorApplication.isPlaying = true;
    }
#endif

    // void OnEnable()
    // {
    //     Lean.Touch.LeanTouch.OnFingerDown += HandleFingerDown;
    //     Lean.Touch.LeanTouch.OnFingerUp += HandleFingerUp;
    //     Lean.Touch.LeanTouch.OnFingerUpdate += HandleFingerUpdate;
    //     
    // }
    //
    // void OnDisable()
    // {
    //     Lean.Touch.LeanTouch.OnFingerDown -= HandleFingerDown;
    //     Lean.Touch.LeanTouch.OnFingerUp -= HandleFingerUp;
    //     Lean.Touch.LeanTouch.OnFingerUpdate -= HandleFingerUpdate;
    // }
    //
    // void HandleFingerDown(Lean.Touch.LeanFinger finger)
    // {
    //     Debug.Log("OnFingerDown");
    //     if (!finger.IsOverGui)
    //     {
    //         _isFingerDown = true;
    //     }
    //     
    //     // var ray = finger.GetRay(Camera);
    //     // var hit = default(RaycastHit);
    //     //
    //     // if (Physics.Raycast(ray, out hit, float.PositiveInfinity)) { //ADDED LAYER SELECTION
    //     //     Debug.Log(hit.collider.gameObject);
    //     // }
    // }
    //
    // void HandleFingerUp(Lean.Touch.LeanFinger finger)
    // {
    //     Debug.Log("OnFingerUp");
    //     _isFingerDown = false;
    // }
    //
    // void HandleFingerUpdate(Lean.Touch.LeanFinger finger)
    // {
    //     if (_isFingerDown)
    //     {
    //         _isFingerDrag = true;
    //         Debug.Log("OnFingerDrag");
    //     }
    // }

    private void Start()
    {
        IngameMusic.Raise();
        GameManager.Instance.StartGame();
    }

    private void OnDestroy()
    {
    }

    public void OnWinGame()
    {
        GameManager.Instance.OnWinGame();
    }

    public void SetWin()
    {
        Target = 0;
        SetTarget();
    }

    public void SetTarget()
    {
        Target--;
        if (Target <= 0)
        {
            completeLevelEvent?.Raise(completeLevelEvent);
        }
    }

    public void OnLoseGame()
    {
        GameManager.Instance.OnLoseGame();
    }
}