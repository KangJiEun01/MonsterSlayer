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
    Sprite[] _ItemIcon;
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
        _ItemIcon = GenericSingleton<UIBase>.Instance.InventoryUI.GetComponent<Inventory>().ItemIcon;
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
            bool[] bools = recipe.Bools;
            Item[] items = temp.GetComponentsInChildren<Item>();
            if (!bools[0]) SetAlpha(items[0].Image, 0.2f);     // ����ᰡ �����ϴٸ� �����ǰ� ����������     
            else SetAlpha(items[0].Image, 1);
            items[0].Image.sprite = _ItemIcon[recipe.First.Idx];
            items[0].Text.text = "X" + recipe.First.Count.ToString();

            temp.GetComponent<Recipe>().Plus[0].SetActive(false);
            temp.GetComponent<Recipe>().Plus[1].SetActive(false);
            temp.GetComponent<Recipe>().Plus[2].SetActive(false);

            if (recipe.Second.Idx == -1)
            {
                items[1].gameObject.SetActive(false);
                temp.GetComponent<Recipe>().Plus[0].SetActive(true);

            }
            else
            {
                if (!bools[1]) SetAlpha(items[1].Image, 0.2f);
                else SetAlpha(items[1].Image, 1);
                items[1].Image.sprite = _ItemIcon[recipe.Second.Idx];
                items[1].Text.text = "X" + recipe.Second.Count.ToString();
            }   
            if (recipe.Third.Idx == -1)
            {
                items[2].gameObject.SetActive(false);
                temp.GetComponent<Recipe>().Plus[0].SetActive(true);
                temp.GetComponent<Recipe>().Plus[1].SetActive(true);
            }
            else
            {
                if (!bools[2]) SetAlpha(items[2].Image, 0.2f);
                else SetAlpha(items[2].Image, 1);
                items[2].Image.sprite = _ItemIcon[recipe.Third.Idx];
                items[2].Text.text = "X" + recipe.Third.Count.ToString();
            }
            if (recipe.Fourth.Idx == -1)
            {
                items[3].gameObject.SetActive(false);
                temp.GetComponent<Recipe>().Plus[0].SetActive(true);
                temp.GetComponent<Recipe>().Plus[1].SetActive(true);
                temp.GetComponent<Recipe>().Plus[2].SetActive(true);
            }
            else
            {
                if (!bools[3]) SetAlpha(items[3].Image, 0.2f);
                else SetAlpha(items[3].Image, 1);
                items[3].Image.sprite = _ItemIcon[recipe.Fourth.Idx];
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
        
        _resultItem.GetComponent<Item>().Image.sprite = _ItemIcon[_currentRecipe.Result.Idx];
        _resultItem.GetComponent<Item>().Text.text = "X" + _currentRecipe.Result.Count;
        _resultText.text = _currentRecipe.Result.Text;
        _resultName.text = _currentRecipe.Result.Name;
        
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
