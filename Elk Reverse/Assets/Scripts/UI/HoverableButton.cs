using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class HoverableButton : Selectable, IPointerClickHandler, ISubmitHandler, IPointerEnterHandler, IPointerExitHandler {

    [Serializable]
    public class ButtonEnterEvent : UnityEvent<GameObject> { }
    [Serializable]
    public class ButtonExitEvent : UnityEvent { }

    [SerializeField]
    private ButtonEnterEvent m_OnEnter = new ButtonEnterEvent();
    [SerializeField]
    private ButtonExitEvent m_OnExit = new ButtonExitEvent();


    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (IsInteractable())
            m_OnEnter.Invoke(this.gameObject);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (IsInteractable())
            m_OnExit.Invoke();
    }

    #region Button.cs code
    [Serializable]
    public class ButtonClickedEvent : UnityEvent { }

    // Event delegates triggered on click.
    [FormerlySerializedAs("onClick")]
    [SerializeField]
    private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

    protected HoverableButton()
    { }

    public ButtonClickedEvent onClick
    {
        get { return m_OnClick; }
        set { m_OnClick = value; }
    }

    private void Press()
    {
        if (!IsActive() || !IsInteractable())
            return;

        m_OnClick.Invoke();
    }

    // Trigger all registered callbacks.
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Press();
    }

    public virtual void OnSubmit(BaseEventData eventData)
    {
        Press();

        // if we get set disabled during the press
        // don't run the coroutine.
        if (!IsActive() || !IsInteractable())
            return;

        DoStateTransition(SelectionState.Pressed, false);
        StartCoroutine(OnFinishSubmit());
    }

    private IEnumerator OnFinishSubmit()
    {
        var fadeTime = colors.fadeDuration;
        var elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        DoStateTransition(currentSelectionState, false);
    }
    #endregion
}
