using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CollectOBJ : MonoBehaviour
{
    public bool isMale;
    public int score, perCollect;
    public TextMeshProUGUI statusText;
    public SwerveMovementSystem swerveMovementSystem;
    public bool isAnimatedRed, isAnimatedGreen;
    public ParticleSystem particleEffect;
    public GameObject playerCam;
    public GameObject winCanvas;
    public ParticleSystem tParticle, fParticle;
    Vector3 tempVector;
    public AudioSource mySource;
    private void Start()
    {
        mySource = GetComponent<AudioSource>();
        swerveMovementSystem = GetComponent<SwerveMovementSystem>();
        EndMultiplier collectOBJ = FindObjectOfType<EndMultiplier>();
        collectOBJ.player = this;
        List<GameObject> collectableOBJ = new List<GameObject>();
        tempVector = statusText.gameObject.transform.localScale;
        foreach (var item in GameObject.FindGameObjectsWithTag("CollectableM"))
        {
            collectableOBJ.Add(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("CollectableF"))
        {
            collectableOBJ.Add(item);
        }
        foreach (var item in collectableOBJ)
        {
            Sequence mySeq = DOTween.Sequence();

            mySeq.Append(item.transform.DORotate(new Vector3(-90, 90, 0), 0.7f).SetEase(Ease.Linear));
            mySeq.Append(item.transform.DORotate(new Vector3(-90, 180, 0), 0.7f).SetEase(Ease.Linear));
            mySeq.Append(item.transform.DORotate(new Vector3(-90, 270, 0), 0.7f).SetEase(Ease.Linear));
            mySeq.Append(item.transform.DORotate(new Vector3(-90, 360, 0), 0.7f).SetEase(Ease.Linear)).SetLoops(-1);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectableM"))
        {
            if (isMale)
            {
                mySource.Play();
                score += perCollect;
                swerveMovementSystem.progressBarAnim(perCollect,0.2f);
                Instantiate(tParticle, other.transform);


            }
            else if (!isMale)
            {
                mySource.Play();
                score -= perCollect;
                swerveMovementSystem.progressBarAnim(-perCollect,0.2f);
                Instantiate(fParticle, other.transform);
            }
            other.transform.DOMove(this.gameObject.transform.position, 0.3f).SetEase(Ease.OutSine);
            other.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                Destroy(other.gameObject);
            });

        }
        else if (other.CompareTag("CollectableF"))
        {
            if (isMale)
            {
                mySource.Play();
                score -= perCollect;
                swerveMovementSystem.progressBarAnim(-perCollect,0.2f);
                Instantiate(tParticle, other.transform);


            }
            else if (!isMale)
            {
                mySource.Play();
                score += perCollect;
                swerveMovementSystem.progressBarAnim(perCollect,0.2f);
                Instantiate(fParticle, other.transform);
            }
            other.transform.DOMove(this.gameObject.transform.position, 0.3f).SetEase(Ease.OutSine);
            other.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                Destroy(other.gameObject);
            });
        }
    }
    [System.Obsolete]
    private void Update()
    {
        if (score < 50)
        {
            if (!isAnimatedRed)
            {
                isAnimatedRed = true;
                isAnimatedGreen = false;
                titleAnimate(Color.red);
                statusText.text = "Sad";
            }
        }
        else
        {
            if (!isAnimatedGreen)
            {
                isAnimatedGreen = true;
                isAnimatedRed = false;
                titleAnimate(Color.green);
                statusText.text = "Happy";
            }

        }
        if (score > 100)
        {
            score = 100;
        }
        else if (score < 0)
        {
            score = 0;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            score += 25;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            score -= 25;
        }
    }

    [System.Obsolete]
    public void titleAnimate(Color myColor)
    {
        particleEffect.startColor = myColor;
        particleEffect.Play();
        Sequence mySeq = DOTween.Sequence();
        Sequence colorSeq = DOTween.Sequence();
        
        mySeq.Append(statusText.gameObject.transform.DOScale(tempVector * 3.5f, 0.3f));
        colorSeq.Append(statusText.DOColor(myColor, 0.3f).SetEase(Ease.OutSine));
        mySeq.AppendInterval(0.3f);
        colorSeq.AppendInterval(0.3f);
        mySeq.Append(statusText.gameObject.transform.DOScale(tempVector, 0.3f));
        colorSeq.Append(statusText.DOColor(Color.white, 0.3f).SetEase(Ease.OutSine));
    }
}
