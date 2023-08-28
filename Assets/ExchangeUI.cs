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
    // ��Ŭ���ϸ� ������ ������ ���������ؼ� �κ��丮�� �߰��ϰ� ǥ��
    //orderCount ������ count�� ��������, �������� ���� ������ ����Ī�Ǽ� ǥ�� -> �κ��丮�� �ִ� �������� ������
    // orderFilter �Ȱ��� filter�� ��������,�������� ���� ������ ����Ī�Ǹ� �κ��丮�� ���ŵǾ� ǥ��
    //filterType ������ _filterType�� ���� Ÿ�Ը� �κ��丮�� ǥ�� �ٽô����� ��ü���ǥ��
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
            bool[] bools = GenericSingleton<ExchangeSystem>.Instance.ExchangeEnable(recipe);       //������ ��Ḷ�� �ŷ��� �������� �Ǵܿ���
            GameObject temp = Instantiate(_recipe, _content);
            foreach (var a in bools) Debug.Log(a);
            Item[] items = temp.GetComponentsInChildren<Item>();
            if (recipe.FirstM.idx == -1) items[0].gameObject.SetActive(false);   //��ᰡ 4�������� ���� �ʿ��Ѱ����
            else
            {

                if (!bools[0]) SetAlpha(items[0].Image, 0.2f);     // ����ᰡ �����ϴٸ� �����ǰ� ����������     
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
            if (recipe.CanExchange)                       //�ŷ������Ұ��
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
        if (recipe.CanExchange)                      //�ŷ������Ұ��
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
