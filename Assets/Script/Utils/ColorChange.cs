using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChange : MonoBehaviour
{
    public float duration = .2f;
    public MeshRenderer meshRenderer;

    public Color startColor = Color.white;

    private Color _correctcolor;

    // Unity Message | 0 references
    private void OnValidate()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Unity Message | 0 references
    private void Start()
    {
        _correctcolor = meshRenderer.materials[0].GetColor("_BaseColor");
        LerpColor();
    }

    // 2 references
    private void LerpColor()
    {
        meshRenderer.materials[0].SetColor("_BaseColor", startColor);
        meshRenderer.materials[0].DOColor(_correctcolor, duration).SetDelay(.5f);
    }

    // Unity Message | 0 references
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            LerpColor();
        }
    }
}
