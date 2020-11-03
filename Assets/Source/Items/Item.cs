using System.Collections;
using System.Collections.Generic;

public class Item
{
    static class ItemTag
    {
        public static string Collectable = "CollectableItem";
        public static string Used = "UsedItem";
    }

    private string Name;
    private string Tag;

    public Item(string name)
    {
        Name = name;
        Tag = ItemTag.Collectable;
    }

}
