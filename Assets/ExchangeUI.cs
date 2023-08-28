using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ExchangeUI : GenericSingleton<ExchangeUI>
{
    [SerializeField] GameObject _item;
    [SerializeField] GameObject _recipe;
    [SerializeField] Transform _content;
    [SerializeField] Sprite[] _ItemIcon;
    [SerializeField] GameObject _resultItem;
    [SerializeField] Text _resultText;
    [SerializeField] GameObject ExchangeBtn;
    int id = 0;
    List<ItemData> InvenData;
    List<Recipe> Recipe;
    Recipe _currentRecipe;
    Action exchangeBtn;
    public Recipe CurrentRecipe { get { return _currentRecipe; } }
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
        InvenData = GenericSingleton<ItemSaver>.Instance.Datas._itemList;
        Recipe = GenericSingleton<ExchangeSystem>.Instance.Recipes;
        SetAlpha(_resultItem.GetComponent<Item>().Image, 0.2f);
        SetAlpha(ExchangeBtn.GetComponent<Image>(), 0.2f);
        ExchangeBtn.GetComponent<Button>().interactable = false;
        foreach (Transform recipe in _content)
        {
            Destroy(recipe.gameObject);
        }

        
        foreach (Recipe recipe in Recipe)
        {
            bool[] bools = GenericSingleton<ExchangeSystem>.Instance.ExchangeEnable(recipe);       //각각의 재료마다 거래가 가능한지 판단여부
            GameObject temp = Instantiate(_recipe, _content);
            foreach (var a in bools) Debug.Log(a);
            Item[] items = temp.GetComponentsInChildren<Item>();
            if (recipe.FirstM.idx == -1) items[0].gameObject.SetActive(false);   //재료가 4가지보다 적게 필요한경우임
            else
            {

                if (!bools[0]) SetAlpha(items[0].Image, 0.2f);     // 각재료가 부족하다면 레시피가 반투명해짐     
                else SetAlpha(items[0].Image, 1);
                items[0].Image.sprite = _ItemIcon[recipe.FirstM.idx];
                items[0].Text.text = "X" + recipe.FirstM.count.ToString();
            }
            if (recipe.SecondM.idx == -1) items[1].gameObject.SetActive(false);
            else
            {

                if (!bools[1]) SetAlpha(items[1].Image, 0.2f);
                else SetAlpha(items[1].Image, 1);
                items[1].Image.sprite = _ItemIcon[recipe.SecondM.idx];
                items[1].Text.text = "X" + recipe.SecondM.count.ToString();
            }
            if (recipe.ThirdM.idx == -1) items[2].gameObject.SetActive(false);
            else
            {
                if (!bools[2]) SetAlpha(items[2].Image, 0.2f);
                else SetAlpha(items[2].Image, 1);
                items[2].Image.sprite = _ItemIcon[recipe.ThirdM.idx];
                items[2].Text.text = "X" + recipe.ThirdM.count.ToString();
            }
            if (recipe.FourthM.idx == -1) items[3].gameObject.SetActive(false);
            else
            {
                if (!bools[3]) SetAlpha(items[3].Image, 0.2f);
                else SetAlpha(items[3].Image, 1);
                items[3].Image.sprite = _ItemIcon[recipe.FourthM.idx];
                items[3].Text.text = "X" + recipe.FourthM.count.ToString();
            }
            items[4].Image.sprite = _ItemIcon[recipe.Result.idx];
            items[4].Text.text = "X" + recipe.Result.count.ToString();
            Button recipeButton = temp.GetComponent<Button>();
            recipeButton.onClick.AddListener(() => OnRecipeButtonClick(recipe));
            if (recipe.CanExchange)                       //거래가능할경우
            {
                SetAlpha(items[4].Image, 1f);
            }
            else
            {
                SetAlpha(items[4].Image, 0.2f);
            }
        }
    }
    public void SetAlpha(Image img, float alpha)
    {
        Color currentColor = img.color;
        currentColor.a = alpha;
        img.color = currentColor;
    }
    private void OnRecipeButtonClick(Recipe recipe)
    { 
        _currentRecipe = recipe;
        _resultItem.GetComponent<Item>().Image.sprite = _ItemIcon[_currentRecipe.Result.idx];
        _resultItem.GetComponent<Item>().Text.text = "X" + _currentRecipe.Result.count;
        if (recipe.CanExchange)                      //거래가능할경우
        {
            
            SetAlpha(_resultItem.GetComponent<Item>().Image, 1f);
            SetAlpha(ExchangeBtn.GetComponent<Image>(), 1);
            ExchangeBtn.GetComponent<Button>().interactable = true;
        }
        else
        {
            SetAlpha(_resultItem.GetComponent<Item>().Image, 0.2f);
            SetAlpha(ExchangeBtn.GetComponent<Image>(), 0.2f);
            ExchangeBtn.GetComponent<Button>().interactable = false;
        }

    }
    public void ExChangeBtn()
    {
        GenericSingleton<ExchangeSystem>.Instance.Exchange(_currentRecipe);
    }


}
