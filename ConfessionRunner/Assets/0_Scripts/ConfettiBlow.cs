using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiBlow : MonoBehaviour
{
    public List<ParticleSystem> confettiList;
    public void BlowConfetti()
    {
        confettiList[0].Play();
        confettiList[0].gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
        confettiList[0].gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
        confettiList[1].Play();
        confettiList[1].gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
        confettiList[1].gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
        
    }
}
