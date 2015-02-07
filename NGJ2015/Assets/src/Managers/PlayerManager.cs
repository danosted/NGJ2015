using Assets.src.Common;
using Assets.src.Managers;
using UnityEngine;
using System.Collections;

public class PlayerManager : ManagerBase
{
    public GameObject GetNewPlayerFromType(Enumerations.PlayerType playerType)
    {
        Debug.Log(string.Format("Fetching object with type '{0}'.", playerType));
        var GO = PrefabPool.Find(x => x.GetComponent(playerType.ToString()) != null);
        if (GO == null)
        {
            var msg = string.Format("No object found with type '{0}'.", playerType);
            Debug.LogError(msg, gameObject);
        }
        var resultGO = GameObject.Instantiate(GO) as GameObject;
        ActiveObjects.Add(resultGO.gameObject);
        resultGO.transform.parent = transform;
        return resultGO;
    }
}
