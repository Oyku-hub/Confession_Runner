using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerningController : MonoBehaviour
{
    [SerializeField] float _forwardSpeed = 5f;
    [SerializeField] float _lerpSpeed = 5f;
    [SerializeField] float _playerXValue=2f;
    [SerializeField] Vector2 _clampValues = new Vector2(-4.75f, 4.75f);
    private Rigidbody _rb;
    private float _newXPos;
    private float _startXPos;



    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _startXPos = transform.position.x;


    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _newXPos = Mathf.Clamp(transform.position.x + Input.mousePosition.x * _playerXValue,_startXPos+_clampValues.x, _startXPos + _clampValues.y); 
        }

        
    }

    private void FixedUpdate()
    {

        _rb.MovePosition(new Vector3(Mathf.Lerp(transform.position.x,_newXPos,_lerpSpeed*Time.fixedDeltaTime), _rb.velocity.y, transform.position.z + _forwardSpeed * Time.fixedDeltaTime));


    }


}
