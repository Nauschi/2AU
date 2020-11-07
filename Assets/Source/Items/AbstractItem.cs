using UnityEngine;

public abstract class AbstractItem
{
    public static class ItemTag
    {
        public static string Collectable = "collectableItem";
        public static string Used = "usedItem";
    }

    private string Name;
    private Sprite Sprite;
    private string Tag;

    public GameObject CollectableItemPrefab;
    public GameObject UsedItemPrefab;

    public AbstractItem(string name, Sprite sprite, string tag)
    {
        Name = name;
        Sprite = sprite;
        Tag = tag;
    }
}

