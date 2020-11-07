using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedItem : MonoBehaviour
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
