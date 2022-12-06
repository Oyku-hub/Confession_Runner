using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveInputSystem : MonoBehaviour
{
    public float _lastFrameFingerPositionX;
    public float _moveFactorX;
    public float MoveFactorX => _moveFactorX;
    public bool IsTouching;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPositionX = Input.mousePosition.x;
            IsTouching = true;
            // Yürüme Animasyonu
        }

        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
            _lastFrameFingerPositionX = Input.mousePosition.x;
            IsTouching = true;
            // Yürüme Animasyonu

        }

        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0f;
            IsTouching = false;
            // Idle Animasyonu
        }
    }



}
