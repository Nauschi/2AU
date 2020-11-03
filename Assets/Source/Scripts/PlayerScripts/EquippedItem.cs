using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedItem : MonoBehaviour
{
    public Actionbar Actionbar;

    public void CheckEquippedItem()
    {
        bool anyActionButtonIsDragged = false;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("ActionButton"))
        {
            if (go.GetComponent<Draggable>().isDraggin)
            {
                anyActionButtonIsDragged = true;
                break;
            }
        }

        if (!anyActionButtonIsDragged)
        {
            Image equippedImage = this.GetComponent<Image>();
            Image selectedFrame = Actionbar.GetSelectedFrame();
            Sprite selectedSprite = selectedFrame.transform.parent.gameObject.GetComponent<Image>().sprite;
            if (selectedSprite != null)
            {
                equippedImage.sprite = selectedSprite;
                Color color = equippedImage.color;
                equippedImage.color = new Color(color.r, color.g, color.b, 255);
            }
            else if (equippedImage.sprite != null)
            {
                equippedImage.sprite = null;
                Color color = equippedImage.color;
                equippedImage.color = new Color(color.r, color.g, color.b, 0);
            }
        }
    }
}
