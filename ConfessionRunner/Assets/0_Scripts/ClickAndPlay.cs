using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndPlay : MonoBehaviour
{
    public GameObject startCanvas, swipeArea;
    public AnimationChar anim;
    public SwerveMovementSystem _rigidbody;
    public UIVFX uIVFX;


    private void Update()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<SwerveMovementSystem>();
            anim = GameObject.FindGameObjectWithTag("Player").GetComponent<AnimationChar>();
        }
        if (uIVFX == null)
        {
            uIVFX = FindObjectOfType<UIVFX>();
        }
    }
    public void contGame()
    {
        _rigidbody.rb.isKinematic = false;
        anim.m_Animator.SetTrigger("walk");
        Time.timeScale = 1;
        startCanvas.SetActive(false);
        uIVFX.startFillerClip();
        bool tempBool = _rigidbody.gameObject.GetComponent<CollectOBJ>().isMale;
        if (tempBool)
        {
            uIVFX.startVFX(uIVFX.mFillerList);
        }
        else
        {
            uIVFX.startVFX(uIVFX.fFillerList);
        }
    }
    public void cont2Game()
    {
        anim.m_Animator.SetTrigger("walk");
    }
    public void haptic2Settings()
    {
        Vibration.Cancel();
    }

}
