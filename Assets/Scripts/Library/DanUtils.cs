using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace CommonMethodsLibrary
{
    public static class DanUtils
    {
        public static Tween MakeScaleAnimation(this Transform t, float time, float desirableSize = 1, bool changePrefabSize = false)
        {
            if (changePrefabSize)
            {
                t.transform.localScale = Vector3.zero;
                return t.transform.DOScale(desirableSize, time);
            }
            else
            {
                return t.DOScale(0, time).SetEase(Ease.OutBack).From();
            }
        }

        public static Tween MakeFlashColor(Material characterMesh, Color flashColor, float time, string materialProperty = null)
        {
            if (materialProperty != null)
            {
                return characterMesh.DOColor(flashColor, materialProperty, time).SetLoops(2, LoopType.Yoyo);
            }
            else
            {
                return characterMesh.DOColor(flashColor, "_Color", time).SetLoops(2, LoopType.Yoyo);
            }
        }
        public static void MakeScale(this Transform t, Vector3 desirableSize)
        {
            t.localScale = desirableSize;
        }

        public static T MakeRandomItemArray<T>(this T[] array)
        {
            if (array.Length == 0) return default(T);
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

        public static GameObject MakeSpawnSimpleProjectile(GameObject itemToSpawn, Transform whereSpawn, bool hasParent = false, Transform parent = null)
        {
            GameObject temp = null;

            if (!hasParent)
            {
                temp = MonoBehaviour.Instantiate(itemToSpawn, whereSpawn.position, Quaternion.identity);
            }
            else
            {
                temp = MonoBehaviour.Instantiate(itemToSpawn, whereSpawn.position, Quaternion.identity, parent);
            }

            temp.transform.rotation = whereSpawn.rotation;

            return temp;
        }


        public static string AppVersion()
        {
            return "Version: " + Application.version.ToString();
        }
    }
}