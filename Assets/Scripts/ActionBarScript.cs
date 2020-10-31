using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActionBarScript : MonoBehaviour, IDropHandler
{

    public Image Frame1;
    public Image Frame2;
    public Image Frame3;

    public Button Ab1;
    public Button Ab2;
    public Button Ab3;

    public Image[] FrameArray;
    public int Selected;

    public GameObject TrapItemPrefab;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Button ActionButton1 = Frame1.GetComponent<Button>();
        ActionButton1.Select();
        Selected = 0;
        FrameArray = new Image[]{ Frame1, Frame2, Frame3};
    }

    // Update is called once per frame
    void Update()
    {

        //Select ActionButton with scroll input
        if(Input.mouseScrollDelta.y < 0)
        {
            if(Selected < 2)
            {
                Selected++;
            } else
            {
                Selected = 0;
            }
            FrameArray[Selected].GetComponent<Button>().Select();
        } else if (Input.mouseScrollDelta.y > 0)
        {
            if(Selected > 0)
            {
                Selected--;
            } else
            {
                Selected = 2;
            }
            FrameArray[Selected].GetComponent<Button>().Select();
        }

        //Select ActionButton with Alpha Keys
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Selected = 0;
            FrameArray[Selected].GetComponent<Button>().Select();
        } else if (Input.GetKey(KeyCode.Alpha2))
        {
            Selected = 1;
            FrameArray[Selected].GetComponent<Button>().Select();
        } else if (Input.GetKey(KeyCode.Alpha3))
        {
            Selected = 2;
            FrameArray[Selected].GetComponent<Button>().Select();
        }

        //Listen to drop item key binding
        if (Input.GetKey(KeyCode.F))
        {
            Image abImage = FrameArray[Selected].transform.parent.gameObject.GetComponent<Image>();
            if (abImage.sprite != null)
            {
                DropSelectedItem(abImage);
            }
        }

        //Use selected ActionButton (on click simulation)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FrameArray[Selected].GetComponent<Button>().Select();
            FrameArray[Selected].transform.parent.gameObject.GetComponent<Button>().onClick.Invoke();
        }

    }

    void FixedUpdate()
    {
        
    }

    /// <summary>Select the button when clicked on and remember the new selected value</summary>
    public void SelectButtonOnClick(Image clickedFrame)
    {
        int selected = Selected;
        int index = 0;
        foreach(Image frame in FrameArray)
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

    private bool DropSelectedItem(Image abImage)
    {
        Vector2 playerPos = Player.transform.position;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int quant = mousePos.x < Player.transform.position.x ? -1 : 1;
        Vector2 droppedPos = new Vector2(playerPos.x + quant, playerPos.y);

        Vector2 prefabScale = new Vector2(TrapItemPrefab.transform.localScale.x * 10, TrapItemPrefab.transform.localScale.y * 10);
        bool isColliding = Physics2D.OverlapBox(droppedPos, prefabScale, 0);
        if(isColliding)
        {
            Debug.Log("Damn son, we are colliding");
            return false;
        }

        //Remove from ActionBar
        abImage.sprite = null;
        Color color = abImage.color;
        abImage.color = new Color(color.r, color.g, color.b, 0);

        //Create new object on ground
        GameObject createdObject = Instantiate(TrapItemPrefab, droppedPos, Quaternion.identity);
        Debug.Log("Local Scale of created Object is: " + createdObject.transform.localScale);
        return true;
    }
}
