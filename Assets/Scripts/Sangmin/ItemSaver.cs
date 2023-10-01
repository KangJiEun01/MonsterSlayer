using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemSaver : GenericSingleton<ItemSaver>
{
    ItemDataWrapper  datas = new ItemDataWrapper();
    public ItemDataWrapper Datas { get { return datas; } }

    public void Init()
    {
        datas._items = new Dictionary<int, ItemData>();
        datas._items.Add(0, new ItemData(0, 10));
        datas._items.Add(1, new ItemData(1, 10));
        datas._items.Add(2, new ItemData(2, 10));
        datas._items.Add(3, new ItemData(3, 10));
        datas._items.Add(4, new ItemData(4, 10));
        datas._items.Add(5, new ItemData(5, 10));
        datas._items.Add(6, new ItemData(6, 10));
        datas._items.Add(7, new ItemData(7, 10));


    }
    public void SubItem(ItemData item)
    {
        if (datas._items.TryGetValue(item.Idx, out ItemData data))
        {
            data.SetCount(data.Count - item.Count);
            if (data.Count == 0) datas._items.Remove(item.Idx);
        }
        else return;

    }
    public void AddItem(ItemData item)
    {
        if (datas._items.TryGetValue(item.Idx, out ItemData data))
        {
            data.SetCount(data.Count + item.Count);
        }
        else datas._items.Add(item.Idx, item);
    }
    public void LoadItemData(Dictionary<int, ItemData> items)
    {
        datas._items = items;
    }

}

[Serializable]
public class ItemDataWrapper
{
    public Dictionary<int, ItemData> _items = new Dictionary<int, ItemData>();
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
        // CSV ���� �ε� �� �Ľ�
        TextAsset itemDataCSV = Resources.Load<TextAsset>("ItemData"); // "ItemData"�� CSV ���ϸ�
        
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
                Debug.Log(_name);
                _text = values[2];
                _type = (ItemType)int.Parse(values[3]);
                break;
            }
        }
        reader.Close();
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
    Weapon,
}

