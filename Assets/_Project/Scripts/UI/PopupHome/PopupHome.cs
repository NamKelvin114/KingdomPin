using System;
using UnityEngine;

public class PopupHome : Popup
{
    public PlayMusicEvent MusicHome;

    // private void OnEnable()
    // {
    //     Debug.Log("pla");
    //     MusicHome.Raise();
    // }

    protected override void BeforeShow()
    {
        base.BeforeShow();
        MusicHome.Raise();
        PopupController.Instance.Show<PopupUI>();
    }

    protected override void BeforeHide()
    {
        base.BeforeHide();
        PopupController.Instance.Hide<PopupUI>();
    }

    public void OnClickStart()
    {
        GameManager.Instance.StartGame();
    }

    public void OnClickDebug()
    {
        PopupController.Instance.Show<PopupDebug>();
    }

    public void OnClickSetting()
    {
        FirebaseManager.OnClickButtonSetting();
        PopupController.Instance.Show<PopupSetting>();
    }

    public void OnClickDailyReward()
    {
        FirebaseManager.OnClickButtonDailyReward();
        PopupController.Instance.Show<PopupDailyReward>();
    }

    public void OnClickShop()
    {
        FirebaseManager.OnClickButtonShop();
        PopupController.Instance.Show<PopupShop>();
    }

    public void OnClickTest()
    {
        PopupController.Instance.Show<PopupTest>();
    }
}