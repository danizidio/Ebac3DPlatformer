using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using NaughtyAttributes;
using UnityEngine.Rendering.Universal;
using System;

public class PostProcessInteractions : MonoBehaviour
{
    public static Action<float> OnFlashScreen;

    [SerializeField] Volume _volume;

     Vignette _vignette;

    ColorAdjustments _colorAdjustments;

    [SerializeField] Color _flashColor, _defaultVignetteColor, _flashAdj, _defaultAdjColor;

    [SerializeField] float _duration;

    float _intensityValue;

    private void Awake()
    {
        _volume = GetComponent<Volume>();

        Vignette temVignette;

        if (_volume.profile.TryGet(out temVignette))
        {
            _vignette = temVignette;

            _intensityValue = _vignette.intensity.value;
        }

        ColorAdjustments tempCol;

        if (_volume.profile.TryGet(out tempCol))
        {
            _colorAdjustments = tempCol;

            _defaultAdjColor = _colorAdjustments.colorFilter.value;
        }
    }

    public void Flash(float recovery)
    {
        StartCoroutine(FlashVignette(recovery));
    }

    IEnumerator FlashVignette(float recovery)
    {
        float t = 0;

        while(t < _duration)
        {
            _vignette.color.Override(Color.Lerp(_defaultVignetteColor, _flashColor, t / _duration));

            _colorAdjustments.colorFilter.Override(Color.Lerp(_defaultAdjColor, _flashAdj, t / _duration));

            _vignette.intensity.Override(_intensityValue + .1f);

            t += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        t = 0;

        while (t < _duration)
        {
            t += Time.deltaTime;

            _vignette.intensity.Override(_intensityValue + .1f - t);

            _colorAdjustments.colorFilter.Override(Color.Lerp(_flashAdj, _defaultAdjColor, t / _duration));

            _vignette.color.Override(Color.Lerp(_flashColor, _defaultVignetteColor, t / recovery));

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnEnable()
    {
        OnFlashScreen = Flash;
    }
}
