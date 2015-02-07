using UnityEngine;
using System.Collections;

public class KeyInputHandler : MonoBehaviour
{
	public delegate void OnLeftPressedDelegate();
	public event OnLeftPressedDelegate OnLeftPressed;

	public delegate void OnLeftReleasedDelegate();
	public event OnLeftReleasedDelegate OnLeftReleased;
	
	public delegate void OnRightPressedDelegate();
	public event OnRightPressedDelegate OnRightPressed;
	
	public delegate void OnRightReleasedDelegate();
	public event OnRightReleasedDelegate OnRightReleased;

	public delegate void OnUpPressedDelegate();
	public event OnUpPressedDelegate OnUpPressed;
	
	public delegate void OnUpReleasedDelegate();
	public event OnUpReleasedDelegate OnUpReleased;

	public delegate void OnDownPressedDelegate();
	public event OnDownPressedDelegate OnDownPressed;
	
	public delegate void OnDownReleasedDelegate();
	public event OnDownReleasedDelegate OnDownReleased;

	public delegate void OnSpacePressedDelegate();
	public event OnSpacePressedDelegate OnSpacePressed;

	public delegate void OnSpaceReleasedDelegate();
	public event OnSpaceReleasedDelegate OnSpaceReleased;
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
            Debug.Log("OnLeftPressed");
			if(OnLeftPressed != null)
			{
				OnLeftPressed();
			}
			else
			{
				Debug.Log("no listener to event");
			}
		}
		if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
            Debug.Log("OnRightPressed");
			if(OnRightPressed != null)
			{
				OnRightPressed();
			}
			else
			{
				Debug.Log("no listener to event");
			}
		}
		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
            Debug.Log("OnUpPressed");
			if(OnUpPressed != null)
			{
				OnUpPressed();
			}
			else
			{
				Debug.Log("no listener to event");
			}
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
            Debug.Log("OnSpacePressed");
			if(OnSpacePressed != null)
			{
				OnSpacePressed();
			}
			else
			{
				Debug.Log("no listener to event");
			}
		}
		if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
            Debug.Log("OnDownPressed");
			if(OnDownPressed != null)
			{
				OnDownPressed();
			}
			else
			{
				Debug.Log("no listener to event");
			}
		}
		if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
		{
            Debug.Log("OnLeftReleased");
			if(OnLeftReleased != null)
			{
				OnLeftReleased();
			}
			else
			{
				Debug.Log("no listener to event");
			}
		}
		if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
		{
            Debug.Log("OnRightReleased");
			if(OnRightReleased != null)
			{
				OnRightReleased();
			}
			else
			{
				Debug.Log("no listener to event");
			}
		}
		if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
		{
            Debug.Log("OnUpReleased");
			if(OnUpReleased != null)
			{
				OnUpReleased();
			}
			else
			{
				Debug.Log("no listener to event");
			}
		}
		if(Input.GetKeyUp(KeyCode.Space))
		{
            Debug.Log("OnSpaceReleased");
			if(OnSpaceReleased != null)
			{
				OnSpaceReleased();
			}
			else
			{
				Debug.Log("no listener to event");
			}
		}
		if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
		{
            Debug.Log("OnDownReleased");
			if(OnDownReleased != null)
			{
				OnDownReleased();
			}
			else
			{
				Debug.Log("no listener to event");
			}
		}
	}
}
