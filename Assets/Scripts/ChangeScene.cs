using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using CommonMethodsLibrary;
using System;

public class ChangeScene : Timer
{
    public delegate void _onChangeScene(string sceneName);
    public static _onChangeScene OnChangeScene;

    public static Action OnReloadScene;

    [SerializeField] bool _useTimer, _automaticSceneChange;

    [SerializeField] string _nextSceneName;

    private void Start()
    {
       if(_automaticSceneChange) ChangingScene(_useTimer, GetTimer);
    }

    private void Update()
    {
        CountDown();
    }
    void ChangingScene(bool useTime, float timer)
    {

        if (!useTime)
        {
            timer = 0;
        }

        SetTimer(timer, () => StartCoroutine(LoadingNextScene(_nextSceneName)));
    }

    public void ReloadScene()
    {
        StartCoroutine(LoadingNextScene(_nextSceneName));
    }

    //BUTTON METHOD
    public void SelectingNextScene(string sceneName)
    {
        StartCoroutine(LoadingNextScene(sceneName));
    }

    //BUTTON METHOD
    public void ExitGame()
    {
        Application.Quit();
    }

    public IEnumerator LoadingNextScene(string sceneName)
    {
        yield return new WaitForSeconds(2);

        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);

        while (!loading.isDone)
        {           
            //ANIMACAO PARA LOADING

            yield return null;
        }
    }

    private void OnEnable()
    {
        OnChangeScene += SelectingNextScene;
        OnReloadScene = ReloadScene;
    }
    private void OnDisable()
    {
        OnChangeScene -= SelectingNextScene;
    }
}
