//using System;
//using System.Collections;
//using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    [SerializeField] ItemType _type;
    [SerializeField] int _count = 1;
    [SerializeField] int _idx = 1;
    [SerializeField] Image _image;
    //[SerializeField] Text _text;
    [SerializeField] TextMeshProUGUI _text;
    //[SerializeField] TextMesh _text;



    public ItemType Type { get { return _type; } }
    public int Count { get { return _count; } }
    public int idx { get { return _idx; } }
    ItemData _itemData;
    public ItemData ItemData { get { return _itemData; } }
    public Image Image { get { return _image; } }
    public TextMeshProUGUI Text { get { return _text; } }
    public void getItem()
    {
        GenericSingleton<ItemSaver>.Instance.Datas._itemList.Add(_itemData);
        Debug.Log(GenericSingleton<ItemSaver>.Instance.Datas._itemList[0].Count);
        GenericSingleton<Inventory>.Instance.DrawItem(_itemData);
        Destroy(gameObject);

    }
    private void Start()
    {
         _itemData = new ItemData(_idx, _count);
    }
}



