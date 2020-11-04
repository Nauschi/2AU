using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    IItem _item; // The current item on this ActionButton object
    Button Button; // The Button component of the ActionButton object
    Image Image; // The Image component of the Button componment of the ActionButton object

    private void Start()
    {
        Button = this.gameObject.GetComponent<Button>();
        Image = Button.GetComponent<Image>();
    }

    public IItem Item
    {
        get { return _item; }
        set
        {
            _item = value;
            ChangeComponents();
        }
    }

    void ChangeComponents()
    {
        if (_item != null)
        {
            //Item was added to ActionBar
            Image.sprite = _item.Sprite;
            Color color = Image.color;
            Image.color = new Color(color.r, color.g, color.b, 255);
        } else
        {
            //Item was removed from ActionBar
            Image.sprite = null;
            Color color = Image.color;
            Image.color = new Color(color.r, color.g, color.b, 0);
        }
    }

}
