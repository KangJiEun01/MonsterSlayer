using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSaver : GenericSingleton<ItemSaver>
{
    ItemDataList datas;
    public ItemDataList Datas { get { return datas; } }
    void Start()
    {
        datas = new ItemDataList();
        datas.ItemList = new List<ItemData>();
    }

    void Update()
    {
        
    }
}

[Serializable]
public class ItemDataList
{
    List<ItemData> _itemList;
    public List<ItemData> ItemList { get { return _itemList; } set { _itemList = value; } }
}
[Serializable]
public class ItemData
{
    ItemType _type;
    public ItemType Type { get { return _type; } }
    int _count;
    public int Count { get { return _count; } set { _count = value; } }
    public ItemData(ItemType type, int count)
    {
        _type = type;
        _count = count;
    }
}

public enum ItemType
{
    Cube,
    Sphere,
}

