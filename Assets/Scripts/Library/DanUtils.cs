using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace CommonMethodsLibrary
{
    public static class DanUtils
    {
        public static Tween MakeScaleAnimation(this Transform t, float desirableSize, float time)
        {
            t.transform.localScale = Vector3.zero;
            return t.transform.DOScale(desirableSize, time);
        }
        public static void MakeScale(this Transform t, Vector3 desirableSize)
        {
            t.localScale = desirableSize;
        }
        public static T MakeRandomItemArray<T>(this T[] array)
        {
            if(array.Length == 0) return default(T);
            return (T)array[Random.Range(0, array.Length)];
        }
        public static T MakeRandomItemList<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
        public static IEnumerator MakeChangeScene(this string sceneName)
        {
            AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);

            while (!loading.isDone)
            {
                //ANIMACAO PARA LOADING

                yield return null;
            }
        }

        public static string MakeReturnSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}