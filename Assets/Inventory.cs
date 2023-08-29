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
    // 좌클릭하면 아이템 데이터 랜덤생산해서 인벤토리에 추가하고 표현
    //orderCount 누르면 count의 내림차순, 오름차순 순서 정렬이 스위칭되서 표현 -> 인벤토리에 있는 아이템이 재정렬
    // orderFilter 똑같이 filter의 내림차순,오름차순 순서 정렬이 스위칭되며 인벤토리에 갱신되어 표시
    //filterType 누르면 _filterType과 같은 타입만 인벤토리에 표시 다시누르면 전체목록표시
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
        if (orderCount)//오름차순
        {
            var datas = from data in OrderdData
                        orderby data.Count ascending
                        select data;
            OrderdData = datas.ToList();
        }
        else //내림차순
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
