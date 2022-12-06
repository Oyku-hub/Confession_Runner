using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class TextAnim : MonoBehaviour
{
    public Transform text;
    public string stringText;

    private void Start()
    {
        textVFX();
    }
    void textVFX()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(text.DOScale(text.localScale * 1.2f, 1));
        mySequence.Append(text.DOScale(Vector3.one * 1.7f, 1));
        mySequence.SetLoops(-1);
    }
}
