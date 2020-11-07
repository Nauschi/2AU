using UnityEngine;

public interface IItem
{

    string Name { get; set; }
    string Tag { get; set; }
    Sprite Sprite { get; set; }

    void UseItem(GameObject player, ActionButton abs);
    void TriggerUsedItem(GameObject player, Collider2D itemCollider);
}
