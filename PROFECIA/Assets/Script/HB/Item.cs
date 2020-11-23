using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{ 
    public int itemID;
    public string itemName;
    public Sprite itemIcon;

    public ItemType itemType;

    public enum ItemType
    {
        Heal,
        Equip
    }

    public Item(int _itemID, ItemType _itemType)
    {
        itemID = _itemID;
        itemType = _itemType;
        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
