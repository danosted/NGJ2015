using Assets.src.Input;
using Assets.src.Managers.Entities;
using UnityEngine;
using System.Collections;

public class JoypadInputhandler : MonoBehaviour
{

    private Player _player;
    public int gamePad = 1;
    public float speedBoost = 3f;
    private Vector3 _directionalVector = Vector3.zero ;
    private MouseLook2D look2d;
	// Use this for initialization
	void Start ()
	{
	    _player = GetComponent<Player>();
	    if (_player.gamePad != gamePad)
	    {
	        _player = null;
	    }
        if (!look2d)
        {
            look2d = GetComponent<MouseLook2D>();
        }
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Vector3 direction = Vector3.zero;
	    if (gamePad == 1)
	    {
            if (_player == null) return;
            if (Input.GetAxis("Joy1-Horizontal") != 0)
	        {
	            float magAxis = Input.GetAxis("Joy1-Horizontal");
                //Debug.LogWarning("magAxis: " + magAxis);
                //_player.OnJoy1Horizontal(magAxis);
	            direction.x = magAxis;

	        }
	        if (Input.GetAxis("Joy1-Vertical") != 0)
	        {
	            float magAxis = Input.GetAxis("Joy1-Vertical");
                //Debug.LogWarning("magAxis: " + magAxis);
                //_player.OnJoy1Vertical(magAxis);
	            direction.y = magAxis;
	        }
	        if (Input.GetAxis("Joy1-Fire") != 0)
	        {
	            //Debug.Log ("Attack1 " + Input.GetAxis("Joy1-Fire"));
	            _player.OnJoy1FirePressed();
	        }
            if (Input.GetAxis("Joy1-Look-Vertical") != 0f)
            {
                _directionalVector.y = Input.GetAxis("Joy1-Look-Vertical");

            }
            if (Input.GetAxis("Joy1-Look-Horizontal") != 0f)
            {
                //Debug.Log("Look- " + Input.GetAxis("Joy1-Look-Horizontal") * Vector3.right);
                _directionalVector.x = Input.GetAxis("Joy1-Look-Horizontal");

            }
            _player.GamePadDirection(direction * speedBoost);
	        if (_directionalVector.magnitude > 0.2f)
	        {
	            look2d.directionalVector = _directionalVector;
	        }
	    }
	    else if(gamePad == 2)
	    {
            if (_player == null) return;
	        if (Input.GetAxis("Joy2-Horizontal") != 0)
	        {
	            float magAxis = Input.GetAxis("Joy2-Horizontal");
                //Debug.LogWarning("magAxis: " + magAxis);
                //_player.OnJoy2Horizontal(magAxis);
	            direction.x = magAxis;

	        }
	        if (Input.GetAxis("Joy2-Vertical") != 0)
	        {
	            float magAxis = Input.GetAxis("Joy2-Vertical");
	            Debug.LogWarning("magAxis: " + magAxis);
                //_player.OnJoy2Vertical(magAxis);
	            direction.y = magAxis;

	        }
	        if (Input.GetAxis("Joy2-Fire") != 0f)
	        {
	            //Debug.Log ("Attack2 " + Input.GetAxis("Joy1-Fire"));
	            _player.OnJoy2FirePressed();
	        }
            if (Input.GetAxis("Joy2-Look-Vertical") != 0f)
            {
                Debug.Log("Look- " + Input.GetAxis("Joy2-Look-Vertical"));
                _directionalVector.y = Input.GetAxis("Joy2-Look-Vertical");
            }
            if (Input.GetAxis("Joy2-Look-Horizontal") != 0f)
            {
                Debug.Log("Look- " + Input.GetAxis("Joy2-Look-Horizontal"));
                _directionalVector.x = Input.GetAxis("Joy2-Look-Horizontal");
            }
            _player.GamePadDirection(direction * speedBoost);
            look2d.directionalVector = _directionalVector;
	    }
	}


}
