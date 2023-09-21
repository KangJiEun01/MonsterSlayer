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
        datas._items.Add(0, new ItemData(0, 3));
        datas._items.Add(2, new ItemData(2, 2));
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
        // CSV 파일 로드 및 파싱
        TextAsset itemDataCSV = Resources.Load<TextAsset>("ItemData"); // "ItemData"는 CSV 파일명
        
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
    Weapon,
}

