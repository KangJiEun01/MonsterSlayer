using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSaver : GenericSingleton<ItemSaver>
{
    ItemDataList datas = new ItemDataList();
    public ItemDataList Datas { get { return datas; } }
    protected override void OnAwake()
    {
        datas._itemList = new List<ItemData>();
        datas._itemList.Add(new ItemData(ItemType.Food, 0, 3));
        datas._itemList.Add(new ItemData(ItemType.Food, 2, 2));
    }

}

[Serializable]
public class ItemDataList
{
    public List<ItemData> _itemList = new List<ItemData>();
    
}
[Serializable]
public class ItemData
{
    ItemType _type;
    public ItemType Type { get { return _type; } }
    int _itemIdx;
    public int Idx { get { return _itemIdx; } }
    int _count;
    public int Count { get { return _count; } }
    public ItemData(ItemType type, int idx,int count)
    {
        _type = type;
        _itemIdx = idx;
        _count = count;
    }
    public void SetCount(int count)
    {
        _count = count;
    }
}
//약초 = 0
//고기 = 1
//익힌 고기 = 2
//비타500 = 3
//과자 = 4
//도시락 = 5 
//톱니 = 6
//건전지 = 7 
//렌치 = 8
//화약 = 9
//라이터 = 10
//열쇠 = 11
//철판 = 12

public enum ItemType
{
    Food,
    Material,
}

