//using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    [SerializeField] ItemType _type;
    [SerializeField] int _count = 1;
    [SerializeField] int _idx = 1;
    public ItemType Type { get { return _type; } }
    ItemData _itemData;
    public ItemData ItemData { get { return _itemData; } }
    public void getItem()
    {
        GenericSingleton<ItemSaver>.Instance.Datas._itemList.Add(_itemData);
        Debug.Log(GenericSingleton<ItemSaver>.Instance.Datas._itemList[0].Count);
        GenericSingleton<Inventory>.Instance.DrawItem(_itemData);
        Destroy(gameObject);

    }
    private void Start()
    {
         _itemData = new ItemData(_type,_idx, _count);
    }
}



