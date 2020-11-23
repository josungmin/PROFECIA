using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class Equipment : MonoBehaviour
{ 
    /* 아이템 대신 장비 교체용
    public int itemID; // 장비의 고유 ID값 중복불가능
    public string itemName; // 장비 아이템 이름
    public Sprite itemIcon; // 장비 아이템 아이콘

    public ItemType itemType;

    public enum ItemType
    {
        Sword,
        Gauntlet
    }

    public Item(int _itemID, ItemType _itemType)
    {
        itemID = _itemID;
        itemType = _itemType;
        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
    }

    public Item(int _itemID, string _itemName, ItemType _itemType)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemType = _itemType;
        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
    }
    
    private PlayerStat thePlayerStat;
    ///private inventory theinven;

    private const int SWORD = 1, GAUNTLET = 2;

    public GameObject go;
    ///public Image[] img_slots; // 장비슬롯 아이콘
    public GameObject go_selected_Slot_UI; // 선택된 장비 슬롯 UI

    ///public Item[] equipItemList; //  현재장착 장비리스트

    private int slelctedSlot; // 선택된 장비 슬롯

    public bool activated;
    private bool inputKey;
 
    // Start is called before the first frame update
    void Start()
    {
        ///theInven = FindObjectOfType<Inventory>();
        ///theOrder = FindObjectOfType<OrderManager>();
        ///theAudio = FindObjectOfType<AudioManager>();
        thePlayerStat = FindObjectOfType<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(InputKey)
        {
            if(Input.GetKeyDown(KeyCode.))
            {
                activated = !activated;

                if (activated)
                {
                    
                }
            }
        }
    }
    */
}
