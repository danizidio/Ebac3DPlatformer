using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CommonMethodsLibrary;

public enum MenuPieces
{
       NULL,
       MAIN,
       OPTIONS,
       PAUSE,
       ABOUT
}

public class MainMenuBehaviour : Singleton<MainMenuBehaviour>
{
    [SerializeField] MenuPieces _previousMenu, _currentMenu;
     MenuPieces _nextMenu;

    [SerializeField] List<GameObject> menuList;

    [SerializeField] GameObject _aboutSession;

    private void Start()
    {
        ShowMenu(MenuPieces.MAIN, ButtonTypes.NULL);

        SoundManager.Instance.PlayMusicByType(MusicType.MAINMENU);
    }

    public void ShowMenu(MenuPieces menuPieces, ButtonTypes buttonTypes)
    {
        if(buttonTypes == ButtonTypes.RETURN)
        {
            menuPieces = _previousMenu;
        }

        _previousMenu = _currentMenu;
        _nextMenu = menuPieces;

        switch (menuPieces)
        {
            case MenuPieces.MAIN:
                {
                    _aboutSession.SetActive(false);

                    menuList.ToArray();

                    for (int i = 0; i < menuList.Count; i++)
                    {
                        menuList[i].transform.DOScale(0, .3f).From();
                    }

                    break;
                }
            case MenuPieces.OPTIONS:
                {
                    for (int i = 0; i < menuList.Count; i++)
                    {
                        menuList[i].transform.DOScale(0, .3f).From();
                    }
                    break;
                }
            case MenuPieces.ABOUT:
                {
                    _aboutSession.SetActive(true);
                    _aboutSession.transform.DOScale(transform.localScale, .3f).From(0);

                    break;
                }
        }
        _currentMenu = _nextMenu;
    }
}
