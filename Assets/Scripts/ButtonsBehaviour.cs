using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ButtonTypes
{
    NULL,
    PLAY,
    LOAD,
    PAUSE,
    MENU,
    ABOUT,
    RETURN,
    EXIT
}

public class ButtonsBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] ButtonTypes _typeBtn;
    public ButtonTypes typeBtn { get { return _typeBtn; } }  

    Vector3 _defaultScale;
    Tween _tween;

    private void Awake()
    {
        _defaultScale = transform.localScale;
    }
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        _tween = transform.DOScale(_defaultScale * 1.3f, .5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tween.Kill();

        transform.localScale = _defaultScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (_typeBtn)   
        {
            case ButtonTypes.PLAY:
                MainMenuBehaviour.Instance.ShowMenu(MenuPieces.MAIN, typeBtn);
                break;
            case ButtonTypes.LOAD:
                MainMenuBehaviour.Instance.ShowMenu(MenuPieces.MAIN, typeBtn);
                break;
            case ButtonTypes.PAUSE:
                MainMenuBehaviour.Instance.ShowMenu(MenuPieces.PAUSE, typeBtn);
                break;
            case ButtonTypes.MENU:
                MainMenuBehaviour.Instance.ShowMenu(MenuPieces.OPTIONS, typeBtn);
                break;
            case ButtonTypes.EXIT:
                MainMenuBehaviour.Instance.ShowMenu(MenuPieces.MAIN, typeBtn);
                break;
            case ButtonTypes.ABOUT:
                MainMenuBehaviour.Instance.ShowMenu(MenuPieces.ABOUT, typeBtn);
                break;
            case ButtonTypes.RETURN:
                MainMenuBehaviour.Instance.ShowMenu(MenuPieces.NULL, typeBtn);
                break;
        }
    }
}
