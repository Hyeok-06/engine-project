using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class TorchLightAnimation : MonoBehaviour
{
    [SerializeField]
    private bool _changeRadius;

    [SerializeField]
    private float _intensityRandom, _radiusRandom, _timeRandom;

    private float _baseIntensity;
    private float _baseTime = 0.3f;
    private float _baseRadius;

    private Sequence _seq = null;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D _light;

    private void Awake()
    {
        _baseIntensity = _light.intensity;
        _baseRadius = _light.pointLightOuterRadius;
    }

    private void OnEnable()
    {
        ShakeLight();
    }

    private void ShakeLight()
    {
        float targetInten = _baseIntensity + Random.Range(-_intensityRandom, +_intensityRandom);
        float targetTime = _baseTime + Random.Range(-_timeRandom, +_timeRandom);

        if (!gameObject.activeSelf) return;
        _seq = DOTween.Sequence();
        _seq.Append(DOTween.To(() =>
                    _light.intensity, value =>
                    _light.intensity = value,
                    targetInten,
                    targetTime
        ));
        _seq.AppendCallback(() => ShakeLight());
    }
}