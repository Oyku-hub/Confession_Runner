using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChar : MonoBehaviour
{
    public Animator m_Animator;

    void Start()
    {

        m_Animator = gameObject.transform.GetChild(2).GetComponent<Animator>();
    }
    private void Update()
    {
        // if (Input.GetMouseButton(0))
        // {
        //     m_Animator.SetTrigger("walk");
        // }
    }

    public void FixRotation()
    {
        this.gameObject.transform.GetChild(2).rotation = new Quaternion(0, 0, 0, 0);
        this.gameObject.transform.GetChild(2).localPosition = new Vector3(0, this.gameObject.transform.GetChild(2).localPosition.y, 0);
    }
}
