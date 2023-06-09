using DG.Tweening;
using Pancake;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCustom : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Button.ButtonClickedEvent OnClick;
    public Button.ButtonClickedEvent OnPress;
    public PlaySoundEvent PlaySoundEvent;

    public bool CanClick = true;
    public bool HavePressEffect = true;
    [ReadOnly] public bool IsMoveEnter;
    [ReadOnly] public Vector3 LocalScale;
    
    private void Awake()
    {
        LocalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanClick)
        {
            OnPress?.Invoke();
            PlaySoundEvent.Raise();
            if (HavePressEffect) transform.DOScale(LocalScale-(Vector3.one*0.1f), .01f).SetEase(Ease.OutQuint);
            IsMoveEnter = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (CanClick)
        {
            transform.localScale = LocalScale;
            if (IsMoveEnter)
            {
                OnClick.Invoke();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsMoveEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsMoveEnter = false;
    }
}