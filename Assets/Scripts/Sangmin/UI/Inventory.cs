
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
        
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (var item in items)
        {
            Destroy(item.gameObject);
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
//public void OrderCount()
//{
//    if (orderCount)//��������
//    {
//        var datas = from data in OrderdData
//                    orderby data.Count ascending
//                    select data;
//        OrderdData = datas.ToList();
//    }
//    else //��������
//    {
//        var datas = from data in OrderdData
//                    orderby data.Count descending
//                    select data;
//        OrderdData = datas.ToList();
//    }
//    orderCount = !orderCount;
//    ReDrwing(OrderdData);
//}

//public void Filter()
//{
//    if (filter)
//    {
//        var datas = from data in OrderdData
//                    where data.Type == _weaponType
//                    select data;
//        ReDrwing(datas.ToList());
//    }
//    else
//    {
//        ReDrwing(OrderdData);
//    }
//    filter = !filter;

//}
// ��Ŭ���ϸ� ������ ������ ���������ؼ� �κ��丮�� �߰��ϰ� ǥ��
//orderCount ������ count�� ��������, �������� ���� ������ ����Ī�Ǽ� ǥ�� -> �κ��丮�� �ִ� �������� ������
// orderFilter �Ȱ��� filter�� ��������,�������� ���� ������ ����Ī�Ǹ� �κ��丮�� ���ŵǾ� ǥ��
//filterType ������ _filterType�� ���� Ÿ�Ը� �κ��丮�� ǥ�� �ٽô����� ��ü���ǥ��
//Dictionary<int, ItemData> OrderdData = new Dictionary<int, ItemData>();
//bool orderCount = true;
//bool orderScale = true;
//bool filter = true;