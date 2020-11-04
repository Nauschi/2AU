using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Actionbar : MonoBehaviour, IDropHandler
{

    public Image Frame1;
    public Image Frame2;
    public Image Frame3;

    public Button Ab1;
    public Button Ab2;
    public Button Ab3;

    public Image[] FrameArray;
    public int Selected;

    public GameObject CollectableItemPrefab;
    public GameObject Player;
    public Animator Animator;
    public EquippedItem EqItem;

    enum PlayerDirection
    {
        DOWN = 1,
        UP = 2,
        SIDE = 3,
    }

    // Start is called before the first frame update
    void Start()
    {
        Button ActionButton1 = Frame1.GetComponent<Button>();
        ActionButton1.Select();
        Selected = 0;
        FrameArray = new Image[] { Frame1, Frame2, Frame3 };
    }

    // Update is called once per frame
    void Update()
    {

        //Select ActionButton with scroll input
        if (Input.mouseScrollDelta.y < 0)
        {
            if (Selected < 2)
            {
                Selected++;
            }
            else
            {
                Selected = 0;
            }
            EqItem.CheckEquippedItem();
            FrameArray[Selected].GetComponent<Button>().Select();
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            if (Selected > 0)
            {
                Selected--;
            }
            else
            {
                Selected = 2;
            }
            EqItem.CheckEquippedItem();
            FrameArray[Selected].GetComponent<Button>().Select();
        }

        //Select ActionButton with Alpha Keys
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Selected = 0;
            FrameArray[Selected].GetComponent<Button>().Select();
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            Selected = 1;
            FrameArray[Selected].GetComponent<Button>().Select();
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            Selected = 2;
            FrameArray[Selected].GetComponent<Button>().Select();
        }

        //Listen to "UseItem" Keybinding (hardcoded Space at the moment)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject ab = FrameArray[Selected].transform.parent.gameObject;
            FrameArray[Selected].GetComponent<Button>().Select();
            if (ab.GetComponent<Image>().sprite != null)
            {
                UseSelectedItem(ab);
                EqItem.CheckEquippedItem();
            }
        }
    }

    /// <summary>Select the button when clicked on and remember the new selected value</summary>
    public void SelectButtonOnClick(Image clickedFrame)
    {
        int selected = Selected;
        int index = 0;
        foreach (Image frame in FrameArray)
        {
            if (frame.name.Equals(clickedFrame.name))
            {
                selected = index;
                break;
            }
            index++;
        }
        Selected = selected;
        FrameArray[Selected].GetComponent<Button>().Select();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
    }

    public Image GetSelectedFrame()
    {
        return FrameArray[Selected];
    }

    private bool UseSelectedItem(GameObject ab)
    {
        ActionButton abScript = ab.GetComponent<ActionButton>();
        Image abImage = ab.GetComponent<Image>();

        Debug.Log("Using ActionButton Item: " + abScript.Item.Name);

        Vector2 playerPos = Player.transform.position;
        int quantX = 0;
        float quantY = 0;
        float playerColliderYOffset = Player.GetComponent<BoxCollider2D>().offset.y; //offset of the PlayerCollider on y axis

        PlayerDirection pd = (PlayerDirection)Animator.GetInteger("LastInput"); //check what the last direction of the player was
        switch (pd)
        {
            case PlayerDirection.DOWN:
                quantY = -1 + playerColliderYOffset;
                break;
            case PlayerDirection.UP:
                quantY = 1 + playerColliderYOffset;
                break;
            case PlayerDirection.SIDE:
                quantX = 1;
                break;
        }

        //flippidi flip flip
        if (quantX > 0 && Player.GetComponent<SpriteRenderer>().flipX)
        {
            quantX = -1;
        }

        //check collision and return if we are colliding
        Vector2 droppedPos = new Vector2(playerPos.x + quantX, playerPos.y + quantY);
        Vector2 prefabScale = new Vector2(CollectableItemPrefab.transform.localScale.x * 10, CollectableItemPrefab.transform.localScale.y * 10);

        bool isColliding = Physics2D.OverlapBox(droppedPos, prefabScale, 90);
        if (isColliding)
        {
            Debug.Log("Damn son, we are colliding ");
            return false;
        }

        //Remove from ActionBar
        abImage.sprite = null;
        Color color = abImage.color;
        abImage.color = new Color(color.r, color.g, color.b, 0);

        //Create new object on ground
        GameObject createdObject = Instantiate(CollectableItemPrefab, droppedPos, Quaternion.identity);
        createdObject.GetComponent<CollectableItem>().Collectable = abScript.Item;
        createdObject.GetComponent<CollectableItem>().randomGen = false;
        createdObject.GetComponent<SpriteRenderer>().sprite = abScript.Item.Sprite;
        Debug.Log("CreatedObject sprite: " + createdObject.GetComponent<SpriteRenderer>().sprite);
        CollectableItem ca = createdObject.GetComponent<CollectableItem>();
        Debug.Log("CreatedObject Item: " + ca.Collectable.Name);
        return true;
    }
}

