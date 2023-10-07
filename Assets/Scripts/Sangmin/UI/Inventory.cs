
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject _item;
    ItemType _weaponType = ItemType.Weapon;
    [SerializeField] Transform _content;
    Sprite[] _ItemIcon;
    public Sprite[] ItemIcon { get { return _ItemIcon; } } 
    int id = 0;
    Dictionary<int, ItemData> InvenData = new Dictionary<int, ItemData>();



    public void Init()
    {
        InvenData = GenericSingleton<ItemSaver>.Instance.Datas._items;
        _ItemIcon = GenericSingleton<UIBase>.Instance.ItemIcon;
        ReDrwing(InvenData);
    }
   
    public void ReDrwing(Dictionary<int, ItemData> InvenData)
    {

        foreach (Transform child in _content.transform)
        {
            Destroy(child.gameObject);
        }
        var datas = from data in InvenData.Values
                                          where data.Type != _weaponType
                                          select data;
        foreach (ItemData item in datas)
        {
            DrawItem(item);
        }
    }
    void DrawItem(ItemData Item)
    {
        if(Item.Count > 0)
        {
            GameObject temp = Instantiate(_item, _content);
            temp.GetComponent<Item>().Text.text = "X" + Item.Count.ToString();
            temp.GetComponent<Item>().Image.sprite = _ItemIcon[Item.ItemIdx];
        }
        
    }
}
