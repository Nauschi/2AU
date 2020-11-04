using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractItem
{
    public static class ItemTag
    {
        public static string Collectable = "collectableItem";
        public static string Used = "usedItem";
    }

    private string Name;
    private Sprite Sprite;
    private string Tag;

    public AbstractItem(string name, Sprite sprite, string tag)
    {
        Name = name;
        Sprite = sprite;
        Tag = tag;
    }
}

