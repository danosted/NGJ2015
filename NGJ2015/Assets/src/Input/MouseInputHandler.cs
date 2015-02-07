using UnityEngine;
using System.Collections;

public class MouseInputHandler : MonoBehaviour {
	
	public delegate void OnClickDelegate();
	public event OnClickDelegate OnClick;

	public delegate void OnPressedObjectDelegate();
    public event OnPressedObjectDelegate OnPressedObject;

	public delegate void OnExitDelegate();
	public event OnExitDelegate OnExit;
	
    public delegate void OnPressDelegate();
	public event OnPressDelegate OnPress;

	public delegate void OnReleaseDelegate();
	public event OnReleaseDelegate OnRelease;

	public delegate void OnFaceLeftDelegate();
	public event OnFaceLeftDelegate OnFaceLeft;

	public delegate void OnFaceRightDelegate();
	public event OnFaceRightDelegate OnFaceRight;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (OnPress != null)
            {
                OnPress();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (OnRelease != null)
            {
                OnRelease();
            }
        }
    }

    void OnMouseUp()
	{
		if(OnClick != null)
		{
			OnClick();
		}
		else
		{
			Debug.Log ("no listener to event", gameObject);
		}
	}

	void OnMouseExit()
	{
		if(OnExit != null)
		{
			OnExit();
		}
		else
		{
			Debug.Log ("no listener to event", gameObject);
		}
	}

	void OnMouseDown()
	{
        if (OnPressedObject != null)
		{
            OnPressedObject();
		}
		else
		{
			Debug.Log ("no listener to event", gameObject);
		}
	}

	void OnMouseEnter()
	{
		if(Input.GetMouseButton(0))
		{
			if(OnPress != null)
			{
				OnPress();
			}
			else
			{
				Debug.Log ("no listener to event", gameObject);
			}
		}
	}

	void OnMouseOver()
	{
		if(Input.GetMouseButtonUp(0))
		{
			if(OnClick != null)
			{
				OnClick();
			}
			else
			{
				Debug.Log ("no listener to event", gameObject);
			}
		}
	}
}