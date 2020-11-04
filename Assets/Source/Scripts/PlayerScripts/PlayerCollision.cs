using UnityEngine;
using UnityEngine.UI;
using static AbstractItem;

public class PlayerCollision : MonoBehaviour
{
    //ActionBar stuff
    public Actionbar ActionBar;
    public EquippedItem EqItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item") && other.gameObject.GetComponent<CollectableItem>().Collectable.Tag.Equals(ItemTag.Collectable))
        {
            Image[] frameArray = ActionBar.FrameArray;

            // I likey likey hardcoded. Can be adjusted to for loop as well in the future
            ActionButton abs1 = frameArray[0].transform.parent.gameObject.GetComponent<ActionButton>();
            ActionButton abs2 = frameArray[1].transform.parent.gameObject.GetComponent<ActionButton>();
            ActionButton abs3 = frameArray[2].transform.parent.gameObject.GetComponent<ActionButton>();

            CollectableItem item = other.GetComponent<CollectableItem>();

            if (abs1.Item == null)
            {
                abs1.Item = item.Collectable;
                Destroy(other.gameObject);
            }
            else if (abs2.Item == null)
            {
                abs2.Item = item.Collectable;
                Destroy(other.gameObject);
            }
            else if (abs3.Item == null)
            {
                abs3.Item = item.Collectable;
                Destroy(other.gameObject);
            }
            EqItem.CheckEquippedItem();
        }
    }
}
