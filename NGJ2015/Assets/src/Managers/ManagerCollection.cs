using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.src.Common;
using UnityEngine;

namespace Assets.src.Managers
{
    public class ManagerCollection
    {

        private static ManagerCollection _managerCollection;
        public static ManagerCollection Instance
        {
            get
            {
                if (_managerCollection == null)
                {
                    _managerCollection = new ManagerCollection();
                }
                return _managerCollection;
            }
        }

        private EnemyManager _enemyManager;
        public EnemyManager EnemyManager
        {
            get
            {
                if (_enemyManager == null)
                {
                    var prefab = Resources.Load(Constants.EnemyManagerName);
                    var GO = (GameObject.Instantiate(prefab)) as GameObject;
                    _enemyManager = GO.GetComponent<EnemyManager>();
                }
                return _enemyManager;
            }
        }

        private PlayerManager _playerManager;
        public PlayerManager PlayerManager
        {
            get
            {
                if (_playerManager == null)
                {
                    var prefab = Resources.Load(Constants.PlayerManagerName);
                    var GO = (GameObject.Instantiate(prefab)) as GameObject;
                    _playerManager = GO.GetComponent<PlayerManager>();
                }
                return _playerManager;
            }
        }

		private MouseInputHandler _mouseInputManager;
		public MouseInputHandler MouseInputManager
		{
			get
			{
				if (_mouseInputManager == null)
				{
					var prefab = Resources.Load(Constants.MouseInputHandlerName);
					var GO = (GameObject.Instantiate(prefab)) as GameObject;
					_mouseInputManager = GO.GetComponent<MouseInputHandler>();
				}
				return _mouseInputManager;
			}
		}

        private EventHandler _eventManager;
        public EventHandler EventManager
        {
            get
            {
                if (_eventManager == null)
                {
                    _eventManager = new EventHandler();
                }
                return _eventManager;
            }
		}
    }
}
