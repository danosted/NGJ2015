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

        private GenericManager _genericManager;
        public GenericManager GenericManager
        {
            get
            {
                if (_genericManager == null)
                {
                    var prefab = Resources.Load(Constants.GenericManagerName);
                    var GO = (GameObject.Instantiate(prefab)) as GameObject;
                    _genericManager = GO.GetComponent<GenericManager>();
                }
                return _genericManager;
            }
        }
    }
}
