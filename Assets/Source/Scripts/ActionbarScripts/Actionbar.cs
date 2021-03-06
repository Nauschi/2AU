﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Actionbar : MonoBehaviour, IDropHandler
{

    private Image Frame1;
    private Image Frame2;
    private Image Frame3;

    public Image[] FrameArray;
    public int Selected;

    public GameObject Player { get; set; }
    public EquippedItem EqItem { get; set; }

    public GameObject EscapeMenu;
    public GameObject SoundsMenu;
    public GameObject OptionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        //me likey likey hardcoded omegaLUL
        Frame1 = this.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
        Frame2 = this.gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Image>();
        Frame3 = this.gameObject.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Image>();

        Button ActionButton1 = Frame1.GetComponent<Button>();
        ActionButton1.Select();
        Selected = 0;
        FrameArray = new Image[] { Frame1, Frame2, Frame3 };
    }

    // Update is called once per frame
    void Update()
    {
        SelectActionButtonListener();
        UseActionButtonListener();
        OpenMenuListener(); // has to be implemented in this script, because Actionbar is always active. EscapeMenu is inactive in the beginning
    }

    private void SelectActionButtonListener()
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Selected = 0;
            FrameArray[Selected].GetComponent<Button>().Select();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Selected = 1;
            FrameArray[Selected].GetComponent<Button>().Select();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Selected = 2;
            FrameArray[Selected].GetComponent<Button>().Select();
        }
    }

    private void UseActionButtonListener()
    {
        //Listen to "UseItem" Keybinding (hardcoded Space at the moment)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject ab = FrameArray[Selected].transform.parent.gameObject;
            ActionButton abs = ab.GetComponent<ActionButton>();
            FrameArray[Selected].GetComponent<Button>().Select(); // select the button component of the frame image
            if (abs.Item != null)
            {
                UseSelectedItem(abs);
                EqItem.CheckEquippedItem();
            }
        }
    }

    private void OpenMenuListener()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EscapeMenu.activeSelf)
            {
                EscapeMenu.SetActive(false);
                EscapeMenu.GetComponent<EscapeMenu>().IsMenuOpen = false;
            } else if (OptionsMenu.activeSelf)
            {
                OptionsMenu.SetActive(false);
                EscapeMenu.SetActive(true);
            } else if (SoundsMenu.activeSelf)
            {
                SoundsMenu.SetActive(false);
                OptionsMenu.SetActive(true);
            }
            else
            {
                EscapeMenu.SetActive(true);
                EscapeMenu.GetComponent<EscapeMenu>().IsMenuOpen = true;
            }
        }
    }

    /// <summary>Select the button when clicked on and remember the new selected value</summary>
    private void SelectButtonOnClick(Image clickedFrame)
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

    private bool UseSelectedItem(ActionButton abs)
    {
       // Image abImage = ab.GetComponent<Image>();

        Debug.Log("Using ActionButton Item: " + abs.Item.Name);
        IItem item = abs.Item;

        try
        {
            item.UseItem(Player, abs);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Exception occurred while trying to use selected item > " + ex.Message);
            return false;
        }
        return true;
    }
}

