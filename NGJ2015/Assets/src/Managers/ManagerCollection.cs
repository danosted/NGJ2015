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

		private AudioManager _audioManager;
		public AudioManager AudioManager
		{
			get
			{
				if (_audioManager == null)
				{
					var prefab = Resources.Load(Constants.AudioManagerName);
					var GO = (GameObject.Instantiate(prefab)) as GameObject;
					_audioManager = GO.GetComponent<AudioManager>();
				}
				return _audioManager;
			}
		}
		
		private WeaponManager _weaponManager;
		public WeaponManager WeaponManager 
		{
			get
			{
				if (_weaponManager == null) {
					var prefab = Resources.Load(Constants.WeaponManagerName);
					var GO = (GameObject.Instantiate(prefab)) as GameObject;
					_weaponManager = GO.GetComponent<WeaponManager>();
				}
				return _weaponManager;
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
		private HillManager _hillManager;
		public HillManager HillManager
		{
			get
			{
				if (_hillManager == null)
				{
					var prefab = Resources.Load(Constants.HillManagerName);
					var GO = (GameObject.Instantiate(prefab)) as GameObject;
					_hillManager = GO.GetComponent<HillManager>();
				}
				return _hillManager;
			}
		}
    }
}
