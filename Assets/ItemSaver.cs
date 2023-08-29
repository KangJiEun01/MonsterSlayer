using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemSaver : GenericSingleton<ItemSaver>
{
    ItemDataList datas = new ItemDataList();
    public ItemDataList Datas { get { return datas; } }
    protected override void OnAwake()
    {
        datas._itemList = new List<ItemData>();
        datas._itemList.Add(new ItemData(0, 3));
        datas._itemList.Add(new ItemData(2, 2));
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
        Debug.Log(itemDataCSV);
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

