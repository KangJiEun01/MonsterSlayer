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
//���� = 0
//��� = 1
//���� ��� = 2
//��Ÿ500 = 3
//���� = 4
//���ö� = 5 
//��� = 6
//������ = 7 
//��ġ = 8
//ȭ�� = 9
//������ = 10
//���� = 11
//ö�� = 12

public enum ItemType
{
    Food,
    Material,
}

