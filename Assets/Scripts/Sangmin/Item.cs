using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    [SerializeField] ItemType _type;
    [SerializeField] int _count = 1;
    [SerializeField] int _idx = 1;
    [SerializeField] Image _image;
    [SerializeField] TextMeshProUGUI _text;



    public ItemType Type { get { return _type; } }
    public int Count { get { return _count; } }
    public int idx { get { return _idx; } }
    ItemData _itemData;
    public ItemData ItemData { get { return _itemData; } }
    public Image Image { get { return _image; } }
    public TextMeshProUGUI Text { get { return _text; } }
    public ItemData GetItem()
    {
        Destroy(gameObject);
        return _itemData;

    }
    private void Start()
    {
         _itemData = new ItemData(_idx, _count);
    }
}



