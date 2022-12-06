using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class UIVFX : MonoBehaviour
{
    public TextMeshProUGUI textBox, completeBox;
    public List<string> mTextList, fTextList, fFillerList, mFillerList;
    public float animTime;
    public List<RectTransform> transforms;
    public Image prefab;
    public SwerveMovementSystem player;
    public RectTransform lastCoin;
    public Text lastCoinText;
    public string quadText;
    public List<AudioClip> mAudioClips, fAudioClips, mFillerClips, fFillerClips;
    public AudioSource audioSource;
    public int audioIndex, textIndex;
    public void runAudioClip(List<AudioClip> audioList)
    {
        bool tempBool = player.gameObject.GetComponent<CollectOBJ>().isMale;
        AudioClip clip = audioList[audioIndex];
        audioSource.clip = clip;
        audioSource.Play();
        audioIndex += 1;
        if (tempBool)
        {
            AudioClip fillerClip = mFillerClips[audioIndex];
            StartCoroutine(turnFiller(audioSource, fillerClip, audioList));
        }
        else
        {
            AudioClip fillerClip = fFillerClips[audioIndex];
            StartCoroutine(turnFiller(audioSource, fillerClip, audioList));
        }

    }
    public void startFillerClip()
    {
        bool tempBool = player.gameObject.GetComponent<CollectOBJ>().isMale;
        if (tempBool)
        {
            audioSource.clip = mFillerClips[0];
        }
        else
        {
            audioSource.clip = fFillerClips[0];
        }
        audioSource.Play();
    }
    IEnumerator turnFiller(AudioSource audio, AudioClip fillerClip, List<AudioClip> audioList)
    {

        yield return new WaitWhile(() => audio.isPlaying);
        audio.clip = fillerClip;
        audio.Play();

    }
    #region textAnimation
    public void startVFX(List<string> myList)
    {
        textBox.alpha = 255;
        bool tempBool = player.gameObject.GetComponent<CollectOBJ>().isMale;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(textBox.DOText(myList[textIndex], animTime));
        mySequence.AppendInterval(1f);
        textIndex += 1;

    }
    public void startVFXFiller(List<string> myList, List<string> myListforStart)
    {
        completeBox.alpha = 255;
        bool tempBool = player.gameObject.GetComponent<CollectOBJ>().isMale;
        Sequence tSequence = DOTween.Sequence();
        tSequence.Append(completeBox.DOText(myList[textIndex - 1], animTime));
        tSequence.AppendInterval(1f).OnComplete(() =>
        {
            textBox.DOFade(0, animTime);
        });

        tSequence.Append(completeBox.DOFade(0, animTime)).OnComplete(() =>
        {
            startVFX(myListforStart);
        });
    }
    #endregion textAnimation
    public void startCoinVFX()
    {
        for (int i = 0; i < transforms.Count; i++)
        {
            Image temp = Instantiate(prefab);
            temp.transform.SetParent(this.gameObject.transform);
            temp.rectTransform.position = this.gameObject.GetComponent<RectTransform>().position;
            Sequence mySeq = DOTween.Sequence();
            mySeq.Append(temp.transform.DOMove(transforms[i].position, 0.4f).SetEase(Ease.OutSine));
            mySeq.AppendInterval(1);
            mySeq.Append(temp.transform.DOMove(lastCoin.position, 0.4f).SetEase(Ease.OutSine)).OnComplete(() =>
            {
                Destroy(temp.gameObject);
                transforms.Remove(temp.gameObject.GetComponent<RectTransform>());
            });
        }
        lastCoin.DOScale(Vector3.one * 3, 0.2f).SetEase(Ease.OutSine).SetLoops(transforms.Count + 1).SetDelay(1.8f).OnComplete(() =>
        {
            lastCoin.DOScale(Vector3.one * 2.5f, 0.2f);
        });
        int tempInt = int.Parse(lastCoinText.text);
        DOTween.To(() => tempInt, x => tempInt = x, player.GetComponent<CollectOBJ>().score, 1f).SetEase(Ease.OutSine).SetDelay(2f).OnUpdate(() =>
        {
            lastCoinText.text = tempInt.ToString();
        });

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startVFX(mFillerList);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            startVFXFiller(mTextList, mFillerList);
        }
    }
}
