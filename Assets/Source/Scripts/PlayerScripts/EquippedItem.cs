using Assets.Source.GameSettings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;

public class EquippedItem : MonoBehaviour
{
    private Actionbar Actionbar;
    public PlayerUI PUI;

    private void Start()
    {
        Actionbar = GameObject.Find(GameConstants.ACTIONBAR_GAMEOBJECT_NAME).GetComponent<Actionbar>();
    }

    public void CheckEquippedItem()
    {
        bool anyActionButtonIsDragged = false;
        for (int i = 0; i < 3; i++)
        {
            if (Actionbar.gameObject.transform.GetChild(i).gameObject.GetComponent<Draggable>().isDraggin)
            {
                anyActionButtonIsDragged = true;
                break;
            }
        }

        if (!anyActionButtonIsDragged)
        {
            Image selectedFrame = Actionbar.GetSelectedFrame();
            Sprite selectedSprite = selectedFrame.transform.parent.gameObject.GetComponent<Image>().sprite;
            if (selectedSprite != null)
            {
                string selectedSpriteName = selectedSprite.name;
                selectedSpriteName = selectedSpriteName.Substring(0, selectedSpriteName.IndexOf("Icon"));
                EquippedItemEnum resultEnum;
                if (Enum.TryParse(selectedSpriteName, out resultEnum)) // parses the substring name of selected item into an Enum
                {
                    if (resultEnum != PUI.equippedItem) // only do something if we have not already equipped the new selected item 
                    {
                        PUI.CmdChangeEquippedItem(resultEnum);
                    }
                }
            }  else if (PUI.equippedItem != EquippedItemEnum.nothing)
            {
                PUI.CmdChangeEquippedItem(EquippedItemEnum.nothing);
            }
        }
        
    }
}

