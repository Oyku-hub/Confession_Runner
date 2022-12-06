using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EndMultiplier : MonoBehaviour
{

    public List<GameObject> multiplierList;
    public AnimationChar anim;

    public CollectOBJ player;
    public float multiplyValue;
    public float startValue, estimatedDestination, estimatedValue;
    public UIVFX uIVFX;
    public GameObject WinCanvas;
    private void Start()
    {
        DOTween.SetTweensCapacity(1000, 1000);
    }
    private void Update()
    {
        if (anim == null)
        {
            anim = GameObject.FindGameObjectWithTag("Player").GetComponent<AnimationChar>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<CollectOBJ>();
        }
    }
    public void multiplyStack()
    {
        if (estimatedValue > 0)
        {
            multiplyValue = player.score / 10f;
            estimatedDestination = startValue + (estimatedValue * multiplyValue) + 7f;

        }

        else if (estimatedValue < 0)
        {
            multiplyValue = player.score / 10f;
            estimatedDestination = startValue + (estimatedValue * multiplyValue) - 7f;

        }

        anim.m_Animator.SetBool("stopWalkIdle", false);


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DOTween.KillAll();
            GameObject camBab = GameObject.Find("camBab").gameObject;
            camBab.transform.DORotate(GameObject.Find("camRot").transform.rotation.eulerAngles, 5f).SetEase(Ease.OutSine);



            if (player.score >= 50)
            {
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                GameObject tempOBJ = other.gameObject.GetComponent<SwerveMovementSystem>().progressBarBab;
                GameObject tempText = player.statusText.gameObject;
                other.gameObject.GetComponent<SwerveInputSystem>().enabled = false;
                multiplyStack();
                other.GetComponent<SwerveMovementSystem>().progressBarAnim(-player.score, 5f);
                other.transform.DOMove(other.gameObject.transform.position + new Vector3(0, 0, estimatedDestination), 5f).SetEase(Ease.OutSine).OnComplete(() =>
                {
                    anim.m_Animator.SetTrigger("victory");
                    WinCanvas.SetActive(true);
                    tempText.SetActive(false);
                    tempOBJ.SetActive(false);
                    uIVFX.textBox.gameObject.SetActive(false);
                    uIVFX.completeBox.gameObject.SetActive(false);
                    uIVFX.startCoinVFX();
                });

            }
            else if (player.score < 50)
            {
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.gameObject.GetComponent<SwerveInputSystem>().enabled = false;
                GameObject tempOBJ = other.gameObject.GetComponent<SwerveMovementSystem>().progressBarBab;
                GameObject tempText = player.statusText.gameObject;
                multiplyStack();
                other.transform.DOMove(other.gameObject.transform.position + new Vector3(0, 0, estimatedDestination), 5f).SetEase(Ease.OutSine).OnComplete(() =>
           {
               anim.m_Animator.SetTrigger("sad");
               WinCanvas.SetActive(true);
               tempOBJ.SetActive(false);
               tempText.SetActive(false);
               uIVFX.textBox.gameObject.SetActive(false);
               uIVFX.completeBox.gameObject.SetActive(false);
               uIVFX.startCoinVFX();
           });


            }

        }
    }
}
