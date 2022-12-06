using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG.Infrastructure.Utils.Swipe;

public class Demo : MonoBehaviour
{
    [SerializeField] private SwipeListener _swipeListener;

    private void OnEnable() {
        _swipeListener.OnSwipe.AddListener(OnSwipe);
    }

    private void OnSwipe(string swipe){
        Debug.Log(swipe);
    }

    private void OnDisable() {
        _swipeListener.OnSwipe.RemoveListener (OnSwipe);
    }
}
