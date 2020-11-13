using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UsedItem : NetworkBehaviour
{
    private static class ItemType
    {
        public const string Trap = "trap";
        public const string Gun = "gun";
    }

    public IItem Item { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        // Maybe used for later purposes
    }
}
