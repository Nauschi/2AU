using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapItem : AbstractItem, IItem
{
    private string _name;
    private Sprite _sprite;
    private string _tag;

    string IItem.Name { get => _name; set => _name = value; }
    string IItem.Tag { get => _tag; set => _tag = value; }
    Sprite IItem.Sprite { get => _sprite; set => _sprite = value; }

    public TrapItem(string name, Sprite sprite, string tag) : base(name, sprite, tag)
    {
        _name = name;
        _sprite = sprite;
        _tag = tag;
    }
}

