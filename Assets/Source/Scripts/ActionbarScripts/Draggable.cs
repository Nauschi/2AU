using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    public bool isDraggin = false;

    private Transform ActionBarTransform;
    private int HierarchyIndex;
    private int TempHierarchyIndex;
    private int TempSelected;
    private bool HierarchyChange;
    private Actionbar ActionBar;

    void Start()
    {
        ActionBar = this.transform.parent.gameObject.GetComponent<Actionbar>();
        ActionBarTransform = this.transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("On begin drag -->" + this.name);
        HierarchyIndex = this.transform.GetSiblingIndex();
        this.transform.SetParent(this.transform.root);
        TempHierarchyIndex = HierarchyIndex;
        TempSelected = ActionBar.Selected;
        HierarchyChange = false;
        isDraggin = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("On drag -->" + this.name);
        this.transform.position = new Vector3(eventData.position.x, this.transform.position.y, this.transform.position.z);
        if (GetComponent<CanvasGroup>().blocksRaycasts)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        Image[] frameArray = ActionBar.FrameArray;
        //Image draggedFrame = FindComponentInChildWithTag<Image>(this.transform.gameObject, "ActionButtonFrame");
        Image draggedActionButton = this.gameObject.GetComponent<Image>();
        float draggedX = draggedActionButton.transform.position.x;
        switch (TempHierarchyIndex)
        {
            case 0: 
                if(draggedX > frameArray[1].transform.parent.position.x && draggedX < frameArray[2].transform.parent.position.x)
                {
                    ActionBar.GetComponent<GridLayoutGroup>().spacing = new Vector2(20, 0);
                    HierarchyIndex = 1;
                    HierarchyChange = true;
                    switch (TempSelected)
                    {
                        case 0:
                            ActionBar.Selected = 1;
                            break;
                        case 1:
                            ActionBar.Selected = 0;
                            break;
                    }
                } else if (draggedX < frameArray[1].transform.parent.position.x)
                {
                    ActionBar.GetComponent<GridLayoutGroup>().spacing = new Vector2(-40, 0);
                    HierarchyIndex = 0;
                } else if (draggedX > frameArray[2].transform.parent.position.x)
                {
                    ActionBar.GetComponent<GridLayoutGroup>().spacing = new Vector2(-40, 0);
                    HierarchyIndex = 2;
                    HierarchyChange = true;
                    switch (TempSelected)
                    {
                        case 0:
                            ActionBar.Selected = 2;
                            break;
                        case 1:
                            ActionBar.Selected = 0;
                            break;
                        case 2:
                            ActionBar.Selected = 1;
                            break;
                    }
                }
                break;
            case 1:
                if (draggedX > frameArray[0].transform.parent.position.x && draggedX < frameArray[2].transform.parent.position.x)
                {
                    ActionBar.GetComponent<GridLayoutGroup>().spacing = new Vector2(20, 0);
                    HierarchyIndex = 1;
                } else if (draggedX < frameArray[0].transform.parent.position.x)
                {
                    ActionBar.GetComponent<GridLayoutGroup>().spacing = new Vector2(-40, 0);
                    HierarchyIndex = 0;
                    HierarchyChange = true;
                    switch (TempSelected)
                    {
                        case 0:
                            ActionBar.Selected = 1;
                            break;
                        case 1:
                            ActionBar.Selected = 0;
                            break;
                    }
                }
                else if (draggedX > frameArray[2].transform.parent.position.x)
                {
                    ActionBar.GetComponent<GridLayoutGroup>().spacing = new Vector2(-40, 0);
                    HierarchyIndex = 2;
                    HierarchyChange = true;
                    switch (TempSelected)
                    {
                        case 1:
                            ActionBar.Selected = 2;
                            break;
                        case 2:
                            ActionBar.Selected = 1;
                            break;
                    }
                }
                break;
            case 2:
                if (draggedX > frameArray[0].transform.parent.position.x && draggedX < frameArray[1].transform.parent.position.x)
                {
                    ActionBar.GetComponent<GridLayoutGroup>().spacing = new Vector2(20, 0);
                    HierarchyIndex = 1;
                    HierarchyChange = true;
                    switch (TempSelected)
                    {
                        case 2:
                            ActionBar.Selected = 1;
                            break;
                        case 1:
                            ActionBar.Selected = 2;
                            break;
                    }
                } else if (draggedX < frameArray[0].transform.parent.position.x)
                {
                    ActionBar.GetComponent<GridLayoutGroup>().spacing = new Vector2(-40, 0);
                    HierarchyIndex = 0;
                    HierarchyChange = true;
                    switch (TempSelected)
                    {
                        case 0:
                            ActionBar.Selected = 1;
                            break;
                        case 1:
                            ActionBar.Selected = 2;
                            break;
                        case 2:
                            ActionBar.Selected = 0;
                            break;
                    }
                }
                else if (draggedX > frameArray[1].transform.parent.position.x)
                {
                    ActionBar.GetComponent<GridLayoutGroup>().spacing = new Vector2(-40, 0);
                    HierarchyIndex = 2;
                }
                break;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + this.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("On end drag -->" + this.name);
        this.transform.SetParent(ActionBarTransform);
        this.transform.SetSiblingIndex(HierarchyIndex);
        ActionBar.GetComponent<GridLayoutGroup>().spacing = new Vector2(-40, 0);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        //reload frameArray in case of hierarchy changes
        if (HierarchyChange)
        {
            foreach (Transform child in ActionBar.transform)
            {
                ActionBar.FrameArray[child.GetSiblingIndex()] = child.GetChild(0).gameObject.GetComponent<Image>();
            }
        }

        ActionBar.FrameArray[ActionBar.Selected].GetComponent<Button>().Select();
        isDraggin = false;
    }
}
