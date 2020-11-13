using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;
using System.Linq;


public enum EquippedItemEnum : byte
{
    nothing,
    gun,
    trap
}

public class PlayerUI : NetworkBehaviour
{
    // Child Script Objects
    public HealthBar HpBar;

    // Child GameObjects
    public GameObject EqItem;

    //Item Prefabs
    public GameObject GunPrefab;
    public GameObject TrapPrefab;

    //All variables that need to be synced to all clients
    [SyncVar(hook = nameof(ChangeMaxHealth))]
    private int maxHealth = 100;
    [SyncVar(hook = nameof(ChangeHealth))]
    private int currentHealth;

    [SyncVar(hook = nameof(OnChangeEquipment))]
    public EquippedItemEnum equippedItem;

    // Start is called before the first frame update
    void Start()
    {
        if (!isClient) return;

        if (isLocalPlayer)
        {
            CmdSetHealthValues();
        }
    }

    void OnChangeEquipment(EquippedItemEnum oldEquippedItem, EquippedItemEnum newEquippedItem)
    {
        StartCoroutine(ChangeEquipment(newEquippedItem));
    }

    IEnumerator ChangeEquipment(EquippedItemEnum newEquippedItem)
    {
        while (EqItem.transform.childCount > 1)
        {
            Destroy(EqItem.transform.GetChild(1).gameObject);
            yield return null;
        }

        switch (newEquippedItem)
        {
            case EquippedItemEnum.gun:
                Instantiate(GunPrefab, EqItem.transform);
                break;
            case EquippedItemEnum.trap:
                Instantiate(TrapPrefab, EqItem.transform);
                break;
        }
    }

    [Command]
    public void CmdChangeEquippedItem(EquippedItemEnum selectedItem)
    {
        equippedItem = selectedItem;
    }

    [Command]
    private void CmdSetHealthValues()
    {
        currentHealth = maxHealth;
        HpBar.SetMaxHealth(maxHealth);
    }

    private void ChangeHealth(int oldHealth, int newHealth)
    {
        HpBar.SetHealth(newHealth);
    }

    private void ChangeMaxHealth(int oldMaxHealth, int newMaxHealth)
    {
        HpBar.SetMaxHealth(newMaxHealth);
    }
}
