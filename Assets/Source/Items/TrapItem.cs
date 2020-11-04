using UnityEngine;

public class TrapItem : AbstractItem, IItem
{
    private string _name;
    private Sprite _sprite;
    private string _tag;

    string IItem.Name { get => _name; set => _name = value; }
    string IItem.Tag { get => _tag; set => _tag = value; }
    Sprite IItem.Sprite { get => _sprite; set => _sprite = value; }    

    public TrapItem(string name, Sprite sprite, string tag) : base(name, sprite, tag)
    {
        _name = name;
        _sprite = sprite;
        _tag = tag;

        CollectableItemPrefab = (GameObject) Resources.Load("Prefabs/Items/CollectableItem", typeof(GameObject));
    }

    public void UseItem(GameObject player, ActionButton abs)
    {
        Vector2 playerPos = player.transform.position;
        int quantX = 0;
        float quantY = 0;
        float playerColliderYOffset = player.GetComponent<BoxCollider2D>().offset.y; //offset of the PlayerCollider on y axis

        //check what the last direction of the player was
        Animator animator = player.GetComponent<Animator>();
        int pd = animator.GetInteger("LastInput"); 
        switch (pd)
        {
            case (int) PlayerMovement.PlayerDirection.DOWN:
                quantY = -1 + playerColliderYOffset;
                break;
            case (int) PlayerMovement.PlayerDirection.UP:
                quantY = 1 + playerColliderYOffset;
                break;
            case (int) PlayerMovement.PlayerDirection.SIDE:
                quantX = 1;
                break;
        }

        //flippidi flip flip
        if (quantX > 0 && player.GetComponent<SpriteRenderer>().flipX)
        {
            quantX = -1;
        }

        //check collision and return if we are colliding
        Vector2 droppedPos = new Vector2(playerPos.x + quantX, playerPos.y + quantY);
        Vector2 prefabScale = new Vector2(CollectableItemPrefab.transform.localScale.x * 10, CollectableItemPrefab.transform.localScale.y * 10);
        bool isColliding = Physics2D.OverlapBox(droppedPos, prefabScale, 90);
        if (isColliding)
        {
            Debug.LogError("Damn son, we are colliding");
            throw new System.Exception("Can't use item [" + this._name + "] because it would cause a collision");
        }

        //Create new object on ground
        GameObject createdObject = GameObject.Instantiate(CollectableItemPrefab, droppedPos, Quaternion.identity);
        CollectableItem collectable = createdObject.GetComponent<CollectableItem>();
        collectable.Collectable = abs.Item;
        collectable.randomGen = false;
        collectable.Collectable.Tag = ItemTag.Used; //set item used tag
        createdObject.GetComponent<SpriteRenderer>().sprite = abs.Item.Sprite;

        //Remove the item from ActionBar
        RemoveItemFromActionBar(abs); 
    }

    void RemoveItemFromActionBar(ActionButton abs)
    {
        abs.Item = null;
    }

}

