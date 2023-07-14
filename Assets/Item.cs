using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemType _type;
    [SerializeField] int _count = 1;
    public ItemType Type { get { return _type; } }
    ItemData _itemData;
    public ItemData ItemData { get { return _itemData; } }
    public ItemData getItem()
    {
        Destroy(gameObject);
        ItemSaver.Instance.GetComponent<ItemSaver>().Datas.ItemList.Add(_itemData);
        return _itemData;
    }
    private void Start()
    {
        ItemData _itemData = new ItemData(_type, _count);
    }
}



