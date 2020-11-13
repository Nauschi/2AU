using UnityEngine;
using UnityEngine.UI;
using static AbstractItem;
using Mirror;
using Assets.Source.GameSettings;

public class PlayerCollision : NetworkBehaviour
{
    //ActionBar stuff
    private Actionbar ActionBar;
    private EquippedItem EqItem;

    private void Start()
    {
        if (!isLocalPlayer) return;

        EqItem = this.gameObject.GetComponentInChildren<EquippedItem>(); //EquippedItem component is not part of Player but instead part of GameCanvas
        ActionBar = GameObject.Find(GameConstants.ACTIONBAR_GAMEOBJECT_NAME).GetComponent<Actionbar>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isLocalPlayer) return;

        Debug.Log("How often am I getting triggered?");
        if (other.gameObject.CompareTag("Item"))
        {
            GameObject item = other.gameObject;
            CollectableItem collectable = item.GetComponent<CollectableItem>();
            UsedItem used = item.GetComponent<UsedItem>();

            if (collectable != null && collectable.Item.Tag.Equals(ItemTag.Collectable))
            {
                CollisionWithCollectableItem(item);
            }
            else if (used != null && used.Item.Tag.Equals(ItemTag.Used))
            {
                CollisionWithUsedItem(item);
            }
        }

    }

    private void CollisionWithUsedItem(GameObject collisionItem)
    {
        if (!isClient) return;

        UsedItem item = collisionItem.GetComponent<UsedItem>();
        item.Item.TriggerUsedItem(this.gameObject, collisionItem);
    }

    private void CollisionWithCollectableItem(GameObject collisionItem)
    {
        if (!isClient) return;

        Image[] frameArray = ActionBar.FrameArray;

        // I likey likey hardcoded. Can be adjusted to for loop as well in the future
        ActionButton abs1 = frameArray[0].transform.parent.gameObject.GetComponent<ActionButton>();
        ActionButton abs2 = frameArray[1].transform.parent.gameObject.GetComponent<ActionButton>();
        ActionButton abs3 = frameArray[2].transform.parent.gameObject.GetComponent<ActionButton>();

        CollectableItem item = collisionItem.GetComponent<CollectableItem>();

        if (abs1.Item == null)
        {
            abs1.Item = item.Item;
            CmdNetworkDestroy(collisionItem);
        }
        else if (abs2.Item == null)
        {
            abs2.Item = item.Item;
            CmdNetworkDestroy(collisionItem);
        }
        else if (abs3.Item == null)
        {
            abs3.Item = item.Item;
            CmdNetworkDestroy(collisionItem);
        }
        EqItem.CheckEquippedItem();
    }

    [Command]
    private void CmdNetworkDestroy(GameObject Object)
    {
        //Get the NetworkIdentity assigned to the object
        NetworkIdentity id = Object.GetComponent<NetworkIdentity>();
        // Check if we successfully got the NetworkIdentity Component from our object, if not we return(essentially do nothing).
        if (id == null) return;
        // First check if the objects NetworkIdentity can be transferred, or if it is server only.
        if (id.serverOnly == false)
        {
            // Do we already own this NetworkIdentity? If so, don't do anything.
            if (id.hasAuthority == false)
            {
                // If we do not already have authority over the NetworkIdentity, assign authority.
                // Keep in mind, using connectionToClient to get this NetworkIdentity is only valid for Network Player Objects.
                if (id.AssignClientAuthority(connectionToClient) == true)
                {
                    // If takeover was successful, we can now destroy our GameObject.
                    CustomNetworkManager.Destroy(Object);
                }
            }
            else
            {
                // Do nothing because we already have ownership of this NetworkIdentity.
            }
        }
        else
        {
            //Server only, so we can't do anything.
        }
    }
}
