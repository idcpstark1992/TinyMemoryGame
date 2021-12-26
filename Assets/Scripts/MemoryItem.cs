using UnityEngine;

[RequireComponent(typeof(TouchScript.Gestures.TapGesture))]
public class MemoryItem : MonoBehaviour
{
    [SerializeField] TouchScript.Gestures.TapGesture TapGestura;
    [SerializeField] AudioItem Audio_Item;
    private void    Start()
    {
        TapGestura          = GetComponent<TouchScript.Gestures.TapGesture>();
        TapGestura.Tapped   += OnTapGestura;
        gameObject.transform.localScale = Vector3.zero;
        Audio_Item = GetComponent<AudioItem>();
        
    }
    private void    OnEnable()
    {
        DelegatesHelpers.Register_OnReset += OnResetGame;
        DelegatesHelpers.Register_OnHideAllObjects += OnResetGame;
    }
    private void    OnDisable()
    {
        TapGestura.Tapped -= OnTapGestura;
        DelegatesHelpers.Register_OnReset -= OnResetGame;
        DelegatesHelpers.Register_OnHideAllObjects -= OnResetGame;
        SecuenceController.Instance.RemoveElement(gameObject.name);
    }
    private void    OnResetGame()
    {
        gameObject.transform.localScale = Vector3.zero;
        SecuenceController.Instance.RemoveElement(gameObject.name);
    }
    private void    OnTapGestura(object obj, System.EventArgs e)
    {
        if (SecuenceController.Instance.PlayerTurn)
        {
            SecuenceController.Instance.CheckUserAnswerInput(gameObject.name);
            LeanTween.scale(gameObject, Vector3.one * .2f, .3f).setLoopPingPong(1);
            Audio_Item.ShotSounOnTape();
        }
    }
    public void     BlinkOnSelected()
    {
        LeanTween.scale(gameObject, Vector3.one * .9f, .2f).setLoopPingPong(1);
        Audio_Item.ShotSounOnTape();
    }
    public void     Emerge(float _delay)
    {
        LeanTween.scale(gameObject, Vector3.one *.5f, .2f).setEaseInBounce().setDelay(_delay);
        SecuenceController.Instance.RegisterElement(gameObject.name, gameObject.GetComponent<MemoryItem>());
    }
}
