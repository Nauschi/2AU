using UnityEngine;
using static AbstractItem;
using Mirror;

public class CollectableItem : NetworkBehaviour
{
    public bool randomGen = false;

    private static class ItemType
    {
        public const string Trap = "trap";
        public const string Gun = "gun";
    }

    private Sprite[] Sprites;
    public IItem Item { get; set; }

    [SyncVar(hook = nameof(ChangeItem))]
    private string spriteName;

    private void Start()
    {
        if (isServer)
        {
            CreateRandomCollectableItem();
        }
    }

    public void CreateRandomCollectableItem()
    {
        Sprites = Resources.LoadAll<Sprite>("Items");
        if (Sprites.Length == 0)
        {
            Debug.Log("I am empty. Please feed me master");
        }
        float randomNumber = Random.Range(0f, 100f);
        int randomItem = randomNumber < 50 ? 0 : 1;

        string name = Sprites[randomItem].name;
        name = name.Substring(0, name.IndexOf("Icon"));
        switch (name)
        {
            case ItemType.Trap:
                Item = new TrapItem(name, Sprites[randomItem], ItemTag.Collectable);
                break;
            case ItemType.Gun:
                Item = new GunItem(name, Sprites[randomItem], ItemTag.Collectable);
                break;
        }

        this.gameObject.GetComponent<SpriteRenderer>().sprite = Item.Sprite;
        spriteName = Item.Sprite.name;
    }    

    public void ChangeItem(string oldSpriteName, string newSpriteName)
    {
        Debug.Log("Called Sync Hook ChangeItem");
        string name = newSpriteName;
        name = name.Substring(0, name.IndexOf("Icon"));
        switch (name)
        {
            case ItemType.Trap:
                Item = new TrapItem(name, Resources.Load<Sprite>("Items/trapIcon"), ItemTag.Collectable);
                break;
            case ItemType.Gun:
                Item = new GunItem(name, Resources.Load<Sprite>("Items/gunIcon"), ItemTag.Collectable);
                break;
        }
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Item.Sprite;
    }

}
