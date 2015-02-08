using Assets.src.Managers;
using Assets.src.Managers.Entities;
using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed = 5.0f;
    public float damage = 1.0f;
	private Vector3 _targetPosition;
    private Player _targetPlayer;
    public bool reachedTarget;

	// Use this for initialization
	void Start () {
		
	}
	


	void Update() {
        if(reachedTarget) return;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
	    if (Vector3.Distance(_targetPlayer.transform.position, transform.position) <= 0.5f)
	    {
            _targetPlayer.TakeDamage(damage);
            ManagerCollection.Instance.WeaponManager.PoolBullets(gameObject);
	    }
	    if (Vector3.Distance(_targetPosition, transform.position) <= 0.1f)
	    {
            // TODO: DO SMTH?
	        reachedTarget = true;
	    }
	}

    public void ShootProjectile(Vector3 from, Player target)
    {
        reachedTarget = false;
        //var msg = string.Format("Shooting drawer from {0} against {1}.", transform.position, target.transform.position);
        //Debug.Log(msg, gameObject);
        _targetPlayer = target;
        //			crossHairs.position = new Vector3(mousepos.x, mousepos.y, 0f);
        var targetPosition = target.transform.position;

        _targetPosition = target.transform.position;
    }
}
