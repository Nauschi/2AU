using UnityEngine;
using static AbstractItem;

public class CollectableItem : MonoBehaviour
{

    public bool randomGen = true;

    private static class ItemType
    {
        public const string Trap = "trap";
        public const string Gun = "gun";
    }

    private Sprite[] Sprites;
    public IItem Collectable { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (randomGen)
        {
            Sprites = Resources.LoadAll<Sprite>("Items");

            if (Sprites.Length == 0)
            {
                Debug.Log("I am empty. Please feed me master");
            }
            CreateRandomCollectableItem();
        }
    }

    void CreateRandomCollectableItem()
    {
        float randomNumber = Random.Range(0f, 100f);
        int randomItem = randomNumber < 50 ? 0 : 1;

        string name = Sprites[randomItem].name;
        name = name.Substring(0, name.IndexOf("Icon"));
        switch (name)
        {
            case ItemType.Trap:
                Collectable = new TrapItem(name, Sprites[randomItem], ItemTag.Collectable);
                break;
            case ItemType.Gun:
                Collectable = new GunItem(name, Sprites[randomItem], ItemTag.Collectable);
                break;
        }

        this.gameObject.GetComponent<SpriteRenderer>().sprite = Collectable.Sprite;
    }
}
