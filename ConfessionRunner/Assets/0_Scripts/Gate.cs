using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public AnimationChar anim;
    public bool isMale;
    public int estimatedScore = 10;
    public int estimatedLose = 20;
    UIVFX uIVFX;
    private void Start()
    {
        uIVFX = FindObjectOfType<UIVFX>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim = other.GetComponent<AnimationChar>();
            anim.FixRotation();
            bool tempBool = other.GetComponent<CollectOBJ>().isMale;
            if (isMale && tempBool)
            {
                anim.m_Animator.SetTrigger("spin");
                other.GetComponent<CollectOBJ>().score += estimatedScore;
                other.GetComponent<SwerveMovementSystem>().progressBarAnim(estimatedScore, 0.2f);
                GetComponent<MeshCollider>().enabled = false;
                uIVFX.startVFXFiller(uIVFX.mTextList, uIVFX.mFillerList);
                uIVFX.runAudioClip(uIVFX.mAudioClips);
            }
            else if (!isMale && !tempBool)
            {
                anim.m_Animator.SetTrigger("spin");
                other.GetComponent<CollectOBJ>().score += estimatedScore;
                other.GetComponent<SwerveMovementSystem>().progressBarAnim(estimatedScore, 0.2f);
                GetComponent<MeshCollider>().enabled = false;
                uIVFX.startVFXFiller(uIVFX.fTextList, uIVFX.fFillerList);
                uIVFX.runAudioClip(uIVFX.fAudioClips);

            }
            else
            {
                anim.m_Animator.SetTrigger("sadSpin");
                other.GetComponent<CollectOBJ>().score -= estimatedLose;
                other.GetComponent<SwerveMovementSystem>().progressBarAnim(-estimatedScore, 0.2f);
                GetComponent<MeshCollider>().enabled = false;
                if (tempBool)
                {
                    uIVFX.startVFXFiller(uIVFX.fTextList, uIVFX.mFillerList);
                    uIVFX.runAudioClip(uIVFX.fAudioClips);
                }
                else
                {
                    uIVFX.startVFXFiller(uIVFX.mTextList, uIVFX.fFillerList);
                    uIVFX.runAudioClip(uIVFX.mAudioClips);
                }
            }
        }
    }
}
