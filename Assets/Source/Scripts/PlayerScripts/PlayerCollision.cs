using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{

    //Player RigidBody
    public Rigidbody2D rb;

    //ActionBar stuff
    public Actionbar ActionBar;
    public EquippedItem EqItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CollectableItem"))
        {
            Image[] frameArray = ActionBar.FrameArray;
            Button ab1 = frameArray[0].transform.parent.gameObject.GetComponent<Button>();
            Button ab2 = frameArray[1].transform.parent.gameObject.GetComponent<Button>();
            Button ab3 = frameArray[2].transform.parent.gameObject.GetComponent<Button>();

            CollectableItem item = other.GetComponent<CollectableItem>();

            if (ab1.GetComponent<Image>().sprite == null)
            {
                ab1.GetComponent<Image>().sprite = other.GetComponent<SpriteRenderer>().sprite;
                Color color = ab1.GetComponent<Image>().color;
                ab1.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 255);
                Destroy(other.gameObject);
                ab1.gameObject.GetComponent<ActionButton>().Item = item.Collectable;
            }
            else if (ab2.GetComponent<Image>().sprite == null)
            {
                ab2.GetComponent<Image>().sprite = other.GetComponent<SpriteRenderer>().sprite;
                Color color = ab2.GetComponent<Image>().color;
                ab2.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 255);
                Destroy(other.gameObject);
                ab2.gameObject.GetComponent<ActionButton>().Item = item.Collectable;
            }
            else if (ab3.GetComponent<Image>().sprite == null)
            {
                ab3.GetComponent<Image>().sprite = other.GetComponent<SpriteRenderer>().sprite;
                Color color = ab3.GetComponent<Image>().color;
                ab3.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 255);
                Destroy(other.gameObject);
                ab3.gameObject.GetComponent<ActionButton>().Item = item.Collectable;
            }
            EqItem.CheckEquippedItem();
        }
    }
}
