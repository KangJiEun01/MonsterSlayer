using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemSaver : GenericSingleton<ItemSaver>
{
    ItemDataWrapper datas = new ItemDataWrapper();
    public ItemDataWrapper Datas { get { return datas; } }

    public void Init()
    {
        datas._items = new Dictionary<int, ItemData>();
        
        foreach(var item in datas._itemDatas)
        {
            datas._items.Add(item._idx, new ItemData(item._idx,item._count));
        }

    }
    public void DemoSceneItem()
    {
        datas._items.Add(0, new ItemData(0, 10));
        datas._items.Add(1, new ItemData(1, 10));
        datas._items.Add(2, new ItemData(2, 10));
        datas._items.Add(3, new ItemData(3, 10));
        datas._items.Add(4, new ItemData(4, 10));
        datas._items.Add(5, new ItemData(5, 10));
        datas._items.Add(6, new ItemData(6, 10));
        datas._items.Add(7, new ItemData(7, 10));
    }
    public void SubItem(ItemData item,int count)
    {
        if (datas._items.TryGetValue(item.ItemIdx, out ItemData data))
        {
            data.SetCount(data.Count - count);
            if (data.Count == 0) datas._items.Remove(item.ItemIdx);
            GenericSingleton<ExchangeSystem>.Instance.CalExchange();
            GenericSingleton<UIBase>.Instance.InventoryInit();
            GenericSingleton<UIBase>.Instance.ExchangeUIInit();
        }

        else return;

    }
    public void AddItem(ItemData item)
    {

        if (datas._items.TryGetValue(item.ItemIdx, out ItemData data))
        {
            datas._items.Remove(data.ItemIdx);
            ItemData newItem = new ItemData(item.ItemIdx,data.Count + item.Count);
            datas._items.Add(newItem.ItemIdx, newItem);
        }
        else datas._items.Add(item.ItemIdx, item);

        GenericSingleton<ExchangeSystem>.Instance.CalExchange();

        GenericSingleton<UIBase>.Instance.InventoryInit();

        GenericSingleton<UIBase>.Instance.ExchangeUIInit();
    }
    public void LoadItemData(List<ItemSource> items)
    {
        datas._items.Clear();
        foreach (var item in items)
        {
            if (!datas._items.ContainsKey(item._idx))
            {
                datas._items.Add(item._idx, new ItemData(item._idx, item._count));
            }
        }   
    }

}


[Serializable]
public class ItemDataWrapper
{
    public Dictionary<int, ItemData> _items = new Dictionary<int, ItemData>();
    public List<ItemSource> _itemDatas = new List<ItemSource>();
}
[Serializable]
public class ItemData
{
    ItemType _type;
    public ItemType Type { get { return _type; } }

    int _itemIdx;
    public int ItemIdx { get { return _itemIdx; } }

    int _count;

    public int Count { get { return _count; } }

    string _name;
    public string Name { get { return _name; } }

    string _text;
    public string Text { get { return _text; } }
    
    public ItemData(int idx,int count)
    {
        _itemIdx = idx;
        _count = count;
        LoadItemData();        
    }
    public void SetCount(int count)
    {
        _count = count;
    }
    void LoadItemData()
    {
        TextAsset itemDataCSV = Resources.Load<TextAsset>("ItemData");
        
        StringReader reader = new StringReader(itemDataCSV.text);
        reader.ReadLine();
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');

            int itemIndex = int.Parse(values[0]);

            if (itemIndex == _itemIdx)
            {
                _name = values[1];
                _text = values[2];
                _type = (ItemType)int.Parse(values[3]);
                break;
            }
        }
        reader.Close();
    }
}


public enum ItemType
{
    Food,
    Material,
    Weapon,
}

