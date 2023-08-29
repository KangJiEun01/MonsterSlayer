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
    [SerializeField] GameObject _resultItem;
    [SerializeField] Text _resultText;
    [SerializeField] Text _resultName;
    [SerializeField] GameObject ExchangeBtn;
    Sprite[] _ItemIcon;
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
        _ItemIcon = GenericSingleton<Inventory>.Instance.ItemIcon;
        _resultItem.SetActive(false);
        _resultText.text = "";
        _resultName.text = "";
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
            if (recipe.First.Idx == -1) items[0].gameObject.SetActive(false);   //��ᰡ 4�������� ���� �ʿ��Ѱ����
            else
            {

                if (!bools[0]) SetAlpha(items[0].Image, 0.2f);     // ����ᰡ �����ϴٸ� �����ǰ� ����������     
                else SetAlpha(items[0].Image, 1);
                items[0].Image.sprite = _ItemIcon[recipe.First.Idx];
                items[0].Text.text = "X" + recipe.First.Count.ToString();
            }
            if (recipe.Second.Idx == -1) items[1].gameObject.SetActive(false);
            else
            {

                if (!bools[1]) SetAlpha(items[1].Image, 0.2f);
                else SetAlpha(items[1].Image, 1);
                items[1].Image.sprite = _ItemIcon[recipe.Second.Idx];
                items[1].Text.text = "X" + recipe.Second.Count.ToString();
            }
            if (recipe.Third.Idx == -1) items[2].gameObject.SetActive(false);
            else
            {
                if (!bools[2]) SetAlpha(items[2].Image, 0.2f);
                else SetAlpha(items[2].Image, 1);
                items[2].Image.sprite = _ItemIcon[recipe.Third.Idx];
                items[2].Text.text = "X" + recipe.Third.Count.ToString();
            }
            if (recipe.Fourth.Idx == -1) items[3].gameObject.SetActive(false);
            else
            {
                if (!bools[3]) SetAlpha(items[3].Image, 0.2f);
                else SetAlpha(items[3].Image, 1);
                items[3].Image.sprite = _ItemIcon[recipe.Fourth.Idx];
                items[3].Text.text = "X" + recipe.Fourth.Count.ToString();
            }
            items[4].Image.sprite = _ItemIcon[recipe.Result.Idx];
            items[4].Text.text = "X" + recipe.Result.Count.ToString();
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
        _resultItem.SetActive(true);
        
        _resultItem.GetComponent<Item>().Image.sprite = _ItemIcon[_currentRecipe.Result.Idx];
        _resultItem.GetComponent<Item>().Text.text = "X" + _currentRecipe.Result.Count;
        _resultText.text = _currentRecipe.Result.Text;
        _resultName.text = _currentRecipe.Result.Name;
        
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
