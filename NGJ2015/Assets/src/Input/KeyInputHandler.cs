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

    private static KeyInputHandler _instance;

    private static object _lock = new object();

    public static KeyInputHandler Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(KeyInputHandler) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (KeyInputHandler)FindObjectOfType(typeof(KeyInputHandler));

                    if (FindObjectsOfType(typeof(KeyInputHandler)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                            " Reopening the scene might fix it.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<KeyInputHandler>();
                        singleton.name = "(singleton) " + typeof(KeyInputHandler).ToString();

                        DontDestroyOnLoad(singleton);

                        Debug.Log("[Singleton] An instance of " + typeof(KeyInputHandler) +
                            " is needed in the scene, so '" + singleton +
                            "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " +
                            _instance.gameObject.name);
                    }
                }

                return _instance;
            }
        }
    }

    private static bool applicationIsQuitting = false;
    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
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
