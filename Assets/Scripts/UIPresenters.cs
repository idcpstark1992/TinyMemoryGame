using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UIPresenters : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button OnStartButton;
    [SerializeField] private UnityEngine.UI.Button OnResetButton;
    [SerializeField] private TMPro.TextMeshProUGUI PrintRounds;
    [SerializeField] private TMPro.TextMeshProUGUI PrintBackCounter;
    [SerializeField] private RectTransform         FailedPanel;

    private List <System.Action> SubscribeActions = new List<System.Action>();

    private void Start()
    {
        PrintBackCounter.gameObject.transform.localScale    = Vector3.zero;
        FailedPanel.gameObject.transform.localScale         = Vector3.zero;
    }
    /// <summary>
    /// Subscribe an Action To be executed on UI , 0 = GameStart  , 2 = game Reset , 3 = New Game
    /// </summary>
    /// <param name="_innerActionToSubs"></param>
    public void SubscribeCallBackActions(System.Action _innerActionToSubs)
    {
        SubscribeActions.Add(_innerActionToSubs);
    }
    private void OnDisable()
    {
        SubscribeActions.Clear();
    }
    public void OnCountDown(string _TextToPresent)
    {
        LeanTween.scale(PrintBackCounter.gameObject, Vector3.one, .2f).setLoopPingPong(1);
        PrintBackCounter.text = _TextToPresent;
    }
    public void OnNewRoundPrint(string _newRoundNumber)
    {
        PrintRounds.text = _newRoundNumber;
    }
    public void OnFailedQuestion()
    {
        LeanTween.scale(FailedPanel, Vector3.one, .3f).setEaseInBounce();
        LeanTween.scale(OnResetButton.gameObject, Vector3.one, .2f).setEaseInBounce().setDelay(.4f);
    }
    public void OnCallNewGame()
    {
        SubscribeActions[0]?.Invoke();
        LeanTween.scale(OnStartButton.gameObject, Vector3.zero, .2f).setEaseOutCirc();
    }
    public void OnCallOnReset()
    {
        DelegatesHelpers.Register_OnReset?.Invoke();
        LeanTween.scale(OnResetButton.gameObject, Vector3.zero, .2f).setEaseOutBounce();
        LeanTween.scale(FailedPanel, Vector3.zero, .3f).setEaseOutCirc().setDelay(.2f);
        OnCallNewGame();
    }
}
