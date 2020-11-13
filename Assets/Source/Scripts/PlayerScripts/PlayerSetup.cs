using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Assets.Source.GameSettings;

public class PlayerSetup : NetworkBehaviour
{
    public Behaviour[] ComponentsToDisable;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            foreach(Behaviour bhv in ComponentsToDisable)
            {
                bhv.enabled = false;
            }
        } else if(isLocalPlayer)
        {
            Actionbar ab = GameObject.Find(GameConstants.ACTIONBAR_GAMEOBJECT_NAME).GetComponent<Actionbar>();
            ab.Player = this.gameObject;
            ab.EqItem = this.gameObject.GetComponentInChildren<EquippedItem>(); //EquippedItem component is not part of Player but instead part of GameCanvas
        }
    }
}
