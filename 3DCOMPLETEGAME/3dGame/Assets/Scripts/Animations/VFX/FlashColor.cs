using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    [Header("Setup")]
    public Color color = Color.red;
    public float timeEmit = .1f;

    private Color defaultColor;

    private Tween _curTween;

    private void Start()
    {
        defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    }


    [NaughtyAttributes.Button]
    public void Flash()
    {
        if (!_curTween.IsActive()) 
        {
            _curTween = meshRenderer.material.DOColor(color, "_EmissionColor", timeEmit).SetLoops(2, LoopType.Yoyo);
        }
    }
}
