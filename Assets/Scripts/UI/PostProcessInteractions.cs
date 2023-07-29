using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//using UnityEngine.Rendering.PostProcessing;
using NaughtyAttributes;
using UnityEngine.Rendering.Universal;

public class PostProcessInteractions : MonoBehaviour
{
    [SerializeField] Volume _volume;

    [SerializeField] Vignette _vignette;

    [SerializeField] Color _flashColor, _defaultColor;

    [SerializeField] float _duration;

    [Button]
    public void Flash()
    {
        StartCoroutine(FlashVignette());
    }

    IEnumerator FlashVignette()
    {
        Vignette temp;

        if(_volume.profile.TryGet<Vignette>(out temp))
        //if(_volume.profile.TryGetSettings<Vignette>(out temp))
        {
            _vignette = temp;
        }
        ColorParameter p;


        float t = 0;

        while(t < _duration)
        {
            //p.value = Color.Lerp(_defaultColor, _flashColor, t/1);
            _vignette.color.Override(Color.Lerp(_defaultColor, _flashColor, t / _duration));
            t += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        t = 0;

        while (t < _duration)
        {
            //p.value = Color.Lerp(_flashColor, _flashColor, t / 1);
            t += Time.deltaTime;

            _vignette.color.Override(Color.Lerp(_flashColor, _defaultColor, t / _duration));

            yield return new WaitForEndOfFrame();
        }
        //_vignette.color = p;

        //yield return new WaitForSeconds(1);

        //p.value = _defaultColor;

        //_vignette.color = p;
    }
}
