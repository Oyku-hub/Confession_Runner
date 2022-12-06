using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwerveMovementSystem : MonoBehaviour
{
    private SwerveInputSystem _swerveInputSystem;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;
    public float minX, maxX;
    public float verticalSpeed;
    public Rigidbody rb;
    public int turnTime;
    public bool isOnXAxis;
    public Image progressBar;
    public GameObject progressBarBab;
    public bool isGameStarted;
    public AnimationChar anim;



    private void Start()
    {
        anim = GetComponent<AnimationChar>();
        rb = GetComponent<Rigidbody>();
        PlayerPrefs.SetInt("sceneIndex", SceneManager.GetActiveScene().buildIndex);
        UIVFX uIVFX = FindObjectOfType<UIVFX>();
        uIVFX.player = this;
        
    }
    private void Awake()
    {
        _swerveInputSystem = GetComponent<SwerveInputSystem>();

    }
    public void progressBarAnim(float amountOfPoint,float animTime)
    {
        float tempFloat = progressBar.fillAmount + amountOfPoint / 100;
        DOTween.To(() => progressBar.fillAmount, x => progressBar.fillAmount = x, tempFloat, animTime).SetEase(Ease.OutSine);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Corner"))
        {
            _swerveInputSystem._lastFrameFingerPositionX = 0;
            _swerveInputSystem._moveFactorX = 0;
            CornerTurn myCorner = other.GetComponent<CornerTurn>();
            this.gameObject.transform.DORotate(new Vector3(this.gameObject.transform.rotation.x, myCorner.rotation, this.gameObject.transform.rotation.z), 0.8f).OnComplete(() =>
            {
                minX = myCorner.minX;
                maxX = myCorner.maxX;
                isOnXAxis = myCorner.isXAxis;
            });
            other.gameObject.SetActive(false);

        }
        else if (other.CompareTag("ConfettiGround"))
        {
            other.GetComponent<ConfettiBlow>().BlowConfetti();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            other.enabled = false;
        }
    }
    private void Update()
    {
        checkLimit();
        if (isGameStarted)
        {
            rb.isKinematic = false;
        }
        if (_swerveInputSystem.IsTouching)
        {
            rb.velocity = transform.forward * verticalSpeed;
            anim.m_Animator.SetBool("stopWalkIdle" , false);
        }
        else
        {
            rb.velocity = Vector3.zero;
            anim.FixRotation();
            anim.m_Animator.SetBool("stopWalkIdle" , true);
        }
        
    }
    void checkLimit()
    {
        if (isOnXAxis)
        {
            if (transform.position.x <= maxX && transform.position.x >= minX)
            {
                float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem._moveFactorX;
                swerveAmount = Mathf.Clamp(value: swerveAmount, min: -maxSwerveAmount, maxSwerveAmount);
                transform.Translate(x: swerveAmount, y: 0, z: 0);
            }
            if (transform.position.x > maxX)
            {
                transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < minX)
            {
                transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            }
        }
        else
        {
            if (transform.position.z <= maxX && transform.position.z >= minX)
            {
                float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem._moveFactorX;
                swerveAmount = Mathf.Clamp(value: swerveAmount, min: -maxSwerveAmount, maxSwerveAmount);
                transform.Translate(x: swerveAmount, y: 0, z: 0);
            }

            if (transform.position.z > maxX)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxX);
            }
            else if (transform.position.z < minX)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, minX);
            }
        }
    }
}
