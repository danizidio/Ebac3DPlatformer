using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using CommonMethodsLibrary;
using System;

public class ChangeScene : Timer
{
    public delegate void _onChangeSceneByName(string sceneName);
    public static _onChangeSceneByName OnChangeSceneByName;

    public delegate void _onChangeSceneByIndex(int sceneIndex);
    public static _onChangeSceneByIndex OnChangeSceneByIndex;

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

        SetTimer(timer, () => StartCoroutine(LoadingNextSceneByName(_nextSceneName)));
    }

    public void ReloadScene()
    {
        StartCoroutine(LoadingNextSceneByName(_nextSceneName));
    }

    //BUTTON METHOD
    public void SelectingNextSceneByName(string sceneName)
    {
        StartCoroutine(LoadingNextSceneByName(sceneName));
    }

    public void SelectingNextSceneByIndex(int sceneIndex)
    {
        StartCoroutine(LoadingNextSceneByIndex(sceneIndex));
    }

    public void LoadingPlayerProgressStage()
    {
        StartCoroutine(LoadingNextSceneByIndex(SaveManager.Instance.saveGame.lastStage + 1));
    }

    //BUTTON METHOD
    public void ExitGame()
    {
        Application.Quit();
    }

    public IEnumerator LoadingNextSceneByName(string sceneName)
    {
        yield return new WaitForSeconds(2);

        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);

        while (!loading.isDone)
        {           
            //ANIMACAO PARA LOADING

            yield return null;
        }
    }

    public IEnumerator LoadingNextSceneByIndex(int sceneIndex)
    {
        yield return new WaitForSeconds(2);

        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneIndex);

        while (!loading.isDone)
        {
            //ANIMACAO PARA LOADING

            yield return null;
        }
    }
    private void OnEnable()
    {
        OnChangeSceneByName += SelectingNextSceneByName;
        OnChangeSceneByIndex += SelectingNextSceneByIndex;
        OnReloadScene = ReloadScene;
    }
    private void OnDisable()
    {
        OnChangeSceneByName -= SelectingNextSceneByName;
        OnChangeSceneByIndex -= SelectingNextSceneByIndex;
    }
}
