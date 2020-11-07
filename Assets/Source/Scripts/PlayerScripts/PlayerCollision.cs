using UnityEngine;
using UnityEngine.UI;
using static AbstractItem;
using UnityEditor.Build.Player;

public class PlayerCollision : MonoBehaviour
{
    //ActionBar stuff
    public Actionbar ActionBar;
    public EquippedItem EqItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            CollectableItem collectable = other.gameObject.GetComponent<CollectableItem>();
            UsedItem used = other.gameObject.GetComponent<UsedItem>();

            if (collectable != null && collectable.Item.Tag.Equals(ItemTag.Collectable))
            {
                CollisionWithCollectableItem(other);
            } else if (used != null && used.Item.Tag.Equals(ItemTag.Used))
            {
                CollisionWithUsedItem(other);
            }
        }
    }

    private void CollisionWithUsedItem(Collider2D other)
    {
        UsedItem item = other.GetComponent<UsedItem>();
        item.Item.TriggerUsedItem(this.gameObject, other);
    }

    private void CollisionWithCollectableItem(Collider2D other)
    {
        Image[] frameArray = ActionBar.FrameArray;

        // I likey likey hardcoded. Can be adjusted to for loop as well in the future
        ActionButton abs1 = frameArray[0].transform.parent.gameObject.GetComponent<ActionButton>();
        ActionButton abs2 = frameArray[1].transform.parent.gameObject.GetComponent<ActionButton>();
        ActionButton abs3 = frameArray[2].transform.parent.gameObject.GetComponent<ActionButton>();

        CollectableItem item = other.GetComponent<CollectableItem>();

        if (abs1.Item == null)
        {
            abs1.Item = item.Item;
            Destroy(other.gameObject);
        }
        else if (abs2.Item == null)
        {
            abs2.Item = item.Item;
            Destroy(other.gameObject);
        }
        else if (abs3.Item == null)
        {
            abs3.Item = item.Item;
            Destroy(other.gameObject);
        }
        EqItem.CheckEquippedItem();
    }
}
