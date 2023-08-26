using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExchangeUI : MonoBehaviour
{
    [SerializeField] GameObject _item;
    [SerializeField] GameObject _recipe;
    [SerializeField] Transform _content;
    [SerializeField] Sprite[] _ItemIcon;
    int id = 0;
    List<ItemData> InvenData;
    List<Recipe> Recipe;
    // 좌클릭하면 아이템 데이터 랜덤생산해서 인벤토리에 추가하고 표현
    //orderCount 누르면 count의 내림차순, 오름차순 순서 정렬이 스위칭되서 표현 -> 인벤토리에 있는 아이템이 재정렬
    // orderFilter 똑같이 filter의 내림차순,오름차순 순서 정렬이 스위칭되며 인벤토리에 갱신되어 표시
    //filterType 누르면 _filterType과 같은 타입만 인벤토리에 표시 다시누르면 전체목록표시
    void Start()
    {
        Init();
    }

    void Update()
    {
       
    }

    public void Init()
    {
        Recipe = GenericSingleton<Exchange>.Instance.Recipes;
        foreach(Recipe recipe in Recipe)
        {
            GameObject temp = Instantiate(_recipe, _content);
            Item[] items = temp.GetComponentsInChildren<Item>();
            if(recipe.FirstM.idx == -1) items[0].gameObject.SetActive(false);
            else
            {
                items[0].Image.sprite = _ItemIcon[recipe.FirstM.idx];
                items[0].Text.text = "X" + recipe.FirstM.count.ToString();
            }
            if (recipe.SecondM.idx == -1) items[1].gameObject.SetActive(false);
            else
            {
                items[1].Image.sprite = _ItemIcon[recipe.SecondM.idx];
                items[1].Text.text = "X" + recipe.SecondM.count.ToString();
            }
            if (recipe.ThirdM.idx == -1) items[2].gameObject.SetActive(false);
            else
            {
                items[2].Image.sprite = _ItemIcon[recipe.ThirdM.idx];
                items[2].Text.text = "X" + recipe.ThirdM.count.ToString();
            }
            if (recipe.FourthM.idx == -1) items[3].gameObject.SetActive(false);
            else
            {
                items[3].Image.sprite = _ItemIcon[recipe.FourthM.idx];
                items[3].Text.text = "X" + recipe.FourthM.count.ToString();
            }
            items[4].Image.sprite = _ItemIcon[recipe.Result.idx];
            items[4].Text.text = "X" + recipe.Result.count.ToString();
        }
    }
    public void DrawItem(ItemData Item)
    {
        
        GameObject temp = Instantiate(_item, _content);
        temp.GetComponentInChildren<Text>().text = Item.Count.ToString();
        temp.GetComponentInChildren<Image>().sprite = _ItemIcon[Item.Idx];
    }

}
