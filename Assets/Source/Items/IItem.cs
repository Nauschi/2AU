using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{

    string Name { get; set; }
    string Tag { get; set; }
    Sprite Sprite { get; set; }

    void useItem(GameObject player, ActionButton abs);

}
