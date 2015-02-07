using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.Managers.Entities
{
    public class Enemy : CharacterBase
	{
		private Player _target;

		public void SetTarget(GameObject targetObject)
		{
			_target = targetObject.GetComponent<Player>();
		}

		public void Update()
		{
			if (_target)
			{
                //var msg = string.Format("Enemy {0} is moving towards {1}.", gameObject, _target);
                //Debug.Log(msg, gameObject);
				transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _speed);
				if (Vector3.Magnitude(transform.position - _target.transform.position) < _range)
				{
					_target.TakeDamage(_damage);
				}
			}
		}
    }
}
