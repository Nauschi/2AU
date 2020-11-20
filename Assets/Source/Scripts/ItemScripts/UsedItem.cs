using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Assets.Source.Scripts.ItemScripts;
using static AbstractItem;

public class UsedItem : NetworkBehaviour
{
    private static class ItemType
    {
        public const string Trap = "trap";
        public const string Gun = "gun";
    }

    [SyncVar(hook = nameof(OnChangeItem))]
    public EquippedItemEnum CurrentItem;

    public GameObject TrapPrefab;
    public GameObject GunPrefab;

    public IItem Item { get; set; }

    void OnChangeItem(EquippedItemEnum oldItem, EquippedItemEnum newItem)
    {
        StartCoroutine(ChangeItem(newItem));
    }

    IEnumerator ChangeItem(EquippedItemEnum newItem)
    {
        while (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            yield return null;
        }

        // Use the new value, not the SyncVar property value
        SetItem(newItem);
    }

    public void SetItem(EquippedItemEnum newItem)
    {
        switch (newItem)
        {
            case EquippedItemEnum.trap:
                Item = new TrapItem("trap", Resources.Load<Sprite>("Items/trapIcon"), ItemTag.Used);
                GameObject usedTrap = Instantiate(TrapPrefab, transform);
                TrapHider th = gameObject.GetComponent<TrapHider>();
                th.HideTrap(usedTrap.GetComponent<UsedItem>());
                break;
            case EquippedItemEnum.gun:
                Item = new GunItem("gun", Resources.Load<Sprite>("Items/gunIcon"), ItemTag.Used);
                Instantiate(GunPrefab, transform);
                break;
        }
    }
}
