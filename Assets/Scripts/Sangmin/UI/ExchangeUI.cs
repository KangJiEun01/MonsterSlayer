using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class ExchangeUI : MonoBehaviour
{
    [SerializeField] GameObject _item;
    [SerializeField] GameObject _recipe;
    [SerializeField] Transform _content;
    [SerializeField] GameObject _resultItem;
    [SerializeField] TextMeshProUGUI _resultText;
    [SerializeField] TextMeshProUGUI _resultName;
    [SerializeField] GameObject ExchangeBtn;
    [SerializeField] GameObject _resultEffect;
    Sprite[] _itemIcon;
    int id = 0;
    List<Recipe> Recipe;

    Recipe _currentRecipe;
    Action exchangeBtn;
    public Recipe CurrentRecipe { get { return _currentRecipe; } }
    // ��Ŭ���ϸ� ������ ������ ���������ؼ� �κ��丮�� �߰��ϰ� ǥ��
    //orderCount ������ count�� ��������, �������� ���� ������ ����Ī�Ǽ� ǥ�� -> �κ��丮�� �ִ� �������� ������
    // orderFilter �Ȱ��� filter�� ��������,�������� ���� ������ ����Ī�Ǹ� �κ��丮�� ���ŵǾ� ǥ��
    //filterType ������ _filterType�� ���� Ÿ�Ը� �κ��丮�� ǥ�� �ٽô����� ��ü���ǥ��

    public void Init()
    {
        Recipe = GenericSingleton<ExchangeSystem>.Instance.Recipes;
        _itemIcon = GenericSingleton<UIBase>.Instance.ItemIcon;
        _resultText.text = "";
        _resultName.text = "";
        _resultItem.SetActive(false);
        SetAlpha(_resultItem.GetComponent<Item>().Image, 0.2f);
        SetAlpha(ExchangeBtn.GetComponent<Image>(), 0.2f);
        ExchangeBtn.GetComponent<Button>().interactable = false;
        _resultEffect.SetActive(false);
        foreach (Transform recipe in _content)
        {   
            Destroy(recipe.gameObject);
        }
        var datas = from data in Recipe
                    orderby data.CanExchange descending
                    select data;
        Recipe = datas.ToList();
        
        foreach (Recipe recipe in Recipe)
        {
            GameObject temp = Instantiate(_recipe, _content);
            temp.name = recipe.Result.Name;
            bool[] bools = recipe.Bools;
            Item[] items = temp.GetComponentsInChildren<Item>();
            if (!bools[0]) SetAlpha(items[0].Image, 0.6f);     // ����ᰡ �����ϴٸ� �����ǰ� ����������     
            else SetAlpha(items[0].Image, 1);
            items[0].Image.sprite = _itemIcon[recipe.First.ItemIdx];
            items[0].Text.text = "X" + recipe.First.Count.ToString();

            temp.GetComponent<RecipeUI>().Plus[0].SetActive(false);
            temp.GetComponent<RecipeUI>().Plus[1].SetActive(false);
            temp.GetComponent<RecipeUI>().Plus[2].SetActive(false);

            if (recipe.Second.ItemIdx == -1)
            {
                items[1].gameObject.SetActive(false);
                

            }
            else
            {
                temp.GetComponent<RecipeUI>().Plus[0].SetActive(true);
                if (!bools[1]) SetAlpha(items[1].Image, 0.6f);
                else SetAlpha(items[1].Image, 1);
                items[1].Image.sprite = _itemIcon[recipe.Second.ItemIdx];
                items[1].Text.text = "X" + recipe.Second.Count.ToString();
            }   
            if (recipe.Third.ItemIdx == -1)
            {
                items[2].gameObject.SetActive(false);         
                
            }
            else
            {
               
                temp.GetComponent<RecipeUI>().Plus[1].SetActive(true);
                if (!bools[2]) SetAlpha(items[2].Image, 0.6f);
                else SetAlpha(items[2].Image, 1);
                items[2].Image.sprite = _itemIcon[recipe.Third.ItemIdx];
                items[2].Text.text = "X" + recipe.Third.Count.ToString();
            }
            if (recipe.Fourth.ItemIdx == -1)
            {
                items[3].gameObject.SetActive(false);

            }
            else
            {
                temp.GetComponent<RecipeUI>().Plus[2].SetActive(true);
                if (!bools[3]) SetAlpha(items[3].Image, 0.6f);
                else SetAlpha(items[3].Image, 1);
                items[3].Image.sprite = _itemIcon[recipe.Fourth.ItemIdx];
                items[3].Text.text = "X" + recipe.Fourth.Count.ToString();
            }
            Button recipeButton = temp.GetComponent<Button>();
            recipeButton.onClick.AddListener(() => OnRecipeButtonClick(recipe));
            
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
        _resultItem.SetActive(true);
        
        _resultItem.GetComponent<Item>().Image.sprite = _itemIcon[_currentRecipe.Result.ItemIdx];
      
        _resultName.text = _currentRecipe.Result.Name + "X" + _currentRecipe.Result.Count;
        
        if (recipe.CanExchange)                      //�ŷ������Ұ��
        {
            _resultEffect.SetActive(true);
            SetAlpha(_resultItem.GetComponent<Item>().Image, 1f);
            SetAlpha(ExchangeBtn.GetComponent<Image>(), 1);
            ExchangeBtn.GetComponent<Button>().interactable = true;
        }
        else
        {
            _resultEffect.SetActive(false);
            SetAlpha(_resultItem.GetComponent<Item>().Image, 0.6f);
            SetAlpha(ExchangeBtn.GetComponent<Image>(), 0.6f);
            ExchangeBtn.GetComponent<Button>().interactable = false;
        }

    }
    public void ExChangeBtn()
    {
        Debug.Log(_currentRecipe.Result.Count);
        GenericSingleton<ExchangeSystem>.Instance.Exchange(_currentRecipe);
    }


}
