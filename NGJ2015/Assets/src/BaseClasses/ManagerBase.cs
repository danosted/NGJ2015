using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.DataAccess.BaseClasses
{
    public class ManagerBase : MonoBehaviour
    {
        #region Properties
        [SerializeField]
        private List<GameObject> _prefabPool;
        public List<GameObject> PrefabPool
        {
            get
            {
                return _prefabPool;
            }
            set
            {
                _prefabPool = value;
            }
        }

        [SerializeField]
        private List<GameObject> _activeObjects = new List<GameObject>();
        public List<GameObject> ActiveObjects
        {
            get
            {
                return _activeObjects;
            }
            set
            {
                _activeObjects = value;
            }
        }

        [SerializeField]
        private List<GameObject> _inactiveObjects = new List<GameObject>();
        public List<GameObject> InactiveObjects
        {
            get
            {
                return _inactiveObjects;
            }
            set
            {
                _inactiveObjects = value;
            }
        }
        #endregion

    }
}
