using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : GenericSingleton<Inventory>
{
    [SerializeField] GameObject _item;
    ItemType _weaponType = ItemType.Weapon;
    [SerializeField] Transform _content;
    [SerializeField] Sprite[] _ItemIcon;
    public Sprite[] ItemIcon { get { return _ItemIcon; } } 
    int id = 0;
    List<ItemData> InvenData = new List<ItemData>();
    List<ItemData> OrderdData = new List<ItemData>();
    bool orderCount = true;
    bool orderScale = true;
    bool filter = true;
    // ��Ŭ���ϸ� ������ ������ ���������ؼ� �κ��丮�� �߰��ϰ� ǥ��
    //orderCount ������ count�� ��������, �������� ���� ������ ����Ī�Ǽ� ǥ�� -> �κ��丮�� �ִ� �������� ������
    // orderFilter �Ȱ��� filter�� ��������,�������� ���� ������ ����Ī�Ǹ� �κ��丮�� ���ŵǾ� ǥ��
    //filterType ������ _filterType�� ���� Ÿ�Ը� �κ��丮�� ǥ�� �ٽô����� ��ü���ǥ��
    void Start()
    {
        InvenData = GenericSingleton<ItemSaver>.Instance.Datas._itemList;
        OrderdData = InvenData;
        ReDrwing(InvenData);
    }

    void Update()
    {
        
    }
    public void OrderCount()
    {
        if (orderCount)//��������
        {
            var datas = from data in OrderdData
                        orderby data.Count ascending
                        select data;
            OrderdData = datas.ToList();
        }
        else //��������
        {
            var datas = from data in OrderdData
                        orderby data.Count descending
                        select data;
            OrderdData = datas.ToList();
        }
        orderCount = !orderCount;
        ReDrwing(OrderdData);
    }

    public void Filter()
    {
        if (filter)
        {
            var datas = from data in OrderdData
                        where data.Type == _weaponType
                        select data;
            ReDrwing(datas.ToList());
        }
        else
        {
            ReDrwing(OrderdData);
        }
        filter = !filter;

    }
    public void ReDrwing(List<ItemData> InvenData)
    {
        
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (var item in items)
        {
            Destroy(item.gameObject);
        }
        var datas = from data in InvenData
                    where data.Type != _weaponType
                    select data;
        foreach (var item in datas.ToList())
        {
            DrawItem(item);
        }
    }
    public void DrawItem(ItemData Item)
    {
        if(Item.Count > 0)
        {
            GameObject temp = Instantiate(_item, _content);
            temp.GetComponent<Item>().Text.text = "X" + Item.Count.ToString();
            temp.GetComponent<Item>().Image.sprite = _ItemIcon[Item.Idx];
        }
        
    }
}
