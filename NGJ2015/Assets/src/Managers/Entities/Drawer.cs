using Assets.src.Managers;
using Assets.src.Managers.Entities;
using UnityEngine;
using System.Collections;

public class Drawer : MonoBehaviour {

	public float speed = 5.0f;
    public float damage = 1.0f;
	private Vector3 _targetPosition;
    private Player _targetPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	void Update() {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
	    if (Vector3.Distance(_targetPlayer.transform.position, transform.position) <= 0.5f)
	    {
            _targetPlayer.TakeDamage(damage);
            ManagerCollection.Instance.WeaponManager.PoolBullets(gameObject);
	    }
	    if (Vector3.Distance(_targetPosition, transform.position) <= 0.1f)
	    {
            ManagerCollection.Instance.WeaponManager.PoolBullets(gameObject);
	    }
	}

    public void ShootDrawer(Vector3 from, Player target)
    {

        var msg = string.Format("Shooting drawer from {0} against {1}.", transform.position, target.transform.position);
        Debug.Log(msg, gameObject);
        _targetPlayer = target;
        //			crossHairs.position = new Vector3(mousepos.x, mousepos.y, 0f);
        var targetPosition = target.transform.position;

        _targetPosition = target.transform.position;
    }
}
