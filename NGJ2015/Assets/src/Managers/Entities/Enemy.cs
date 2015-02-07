using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.Managers.Entities
{
    public class Enemy : CharacterBase
	{
		private CharacterBase _target;
        private List<GameObject> _targets; 

		public void SetTarget(List<GameObject> targetObjects)
		{
			_targets = targetObjects;
		}

		public void Update()
		{
		    _target = GetNearestTarget(_targets, transform.position).GetComponent<CharacterBase>();
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

        private GameObject GetNearestTarget(List<GameObject> targets, Vector3 position)
        {
            var nearestDist = float.MaxValue;
            GameObject result = targets[0];
            foreach (var target in targets)
            {
                var tempDist = Vector3.Distance(target.transform.position, position);
                if (tempDist < nearestDist)
                {
                    nearestDist = tempDist;
                    result = target;
                }
            }
            return result;
        }
    }
}
