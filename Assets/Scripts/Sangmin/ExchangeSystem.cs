using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExchangeSystem : GenericSingleton<ExchangeSystem>
{
    List<Recipe> _recipes = new List<Recipe>();
    public List<Recipe> Recipes { get { return _recipes; } }
    Dictionary<int, ItemData> _invenData = new Dictionary<int, ItemData>();
  

    public void Init()
    {
        //최대 4가지재료 (첫번째 재료,갯수) (두번째 재료,갯수) (세번째 재료,갯수) (네번째 재료,갯수) (결과,갯수)
        //재료가 적게필요하면 -1 넣기
        _invenData = GenericSingleton<ItemSaver>.Instance.Datas._items;
        LoadRecipeData();
        CalExchange();

    }
    public void RecipeUpdate()
    {
        _invenData = GenericSingleton<ItemSaver>.Instance.Datas._items;
        CalExchange();
    }
    void LoadRecipeData()
    {

        TextAsset RecipeDataCSV = Resources.Load<TextAsset>("RecipeData");

        StringReader reader = new StringReader(RecipeDataCSV.text);
        reader.ReadLine();
        List<Recipe> temp = new List<Recipe>();
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            int firstItemIdx = int.Parse(values[1]);    
            int firstItemCount = int.Parse(values[2]);
            int SecondItemIdx = int.Parse(values[4]);
            int SecondItemCount = int.Parse(values[5]);
            int ThirdItemIdx = int.Parse(values[7]);
            int ThirdItemCount = int.Parse(values[8]);
            int FourthItemIdx = int.Parse(values[10]);
            int FourthItemCount = int.Parse(values[11]);
            int ResultItemIdx = int.Parse(values[13]);
            int ResultItemCount = int.Parse(values[14]);
            bool isWeapon = (values[15] == "1") ? true : false;

            temp.Add(new Recipe(new ItemData(firstItemIdx, firstItemCount), new ItemData(SecondItemIdx, SecondItemCount), new ItemData(ThirdItemIdx, ThirdItemCount), new ItemData(FourthItemIdx, FourthItemCount), new ItemData(ResultItemIdx, ResultItemCount),isWeapon));
  
        }
        _recipes = temp;
        reader.Close();
    }
    public void CalExchange()
    {
        foreach(Recipe recipe in _recipes)
        {
            ExchangeEnable(recipe);
        }
    }
    void ExchangeEnable(Recipe recipe)
    {
        bool[] mats = new bool[] { false, false, false, false };
        foreach (ItemData item in _invenData.Values)
        {
            if (recipe.First.ItemIdx == -1) mats[0] = true;
            else if (recipe.First.ItemIdx == item.ItemIdx)
            {
                if(item.Count >= recipe.First.Count) mats[0] = true;   
            }
            if (recipe.Second.ItemIdx == -1) mats[1] = true;
            else if(recipe.Second.ItemIdx == item.ItemIdx )
            {
                if (item.Count >= recipe.Second.Count) mats[1] = true;
            }
            if (recipe.Third.ItemIdx == -1) mats[2] = true;
            else if(recipe.Third.ItemIdx == item.ItemIdx)
            {
                if (item.Count >= recipe.Third.Count) mats[2] = true;
            }
            if (recipe.Fourth.ItemIdx == -1) mats[3] = true;
            else if(recipe.Fourth.ItemIdx == item.ItemIdx)
            {
                if (item.Count >= recipe.Fourth.Count) mats[3] = true;
            }



        }
        if (mats[0] && mats[1] && mats[2] && mats[3])                       //거래가능할경우
        {
            recipe.SetCanExchange(true);
        }
        else
        {
            recipe.SetCanExchange(false);
        }
        recipe.SetBools(mats);
    }
    public void Exchange(Recipe recipe)
    {
        if (recipe.IsWeapon)
        {
            GenericSingleton<ItemSaver>.Instance.SubItem(recipe.First,recipe.First.Count);
            GenericSingleton<ItemSaver>.Instance.SubItem(recipe.Second,recipe.Second.Count);
            GenericSingleton<ItemSaver>.Instance.SubItem(recipe.Third, recipe.Third.Count);
            GenericSingleton<ItemSaver>.Instance.SubItem(recipe.Fourth, recipe.Fourth.Count);

            GenericSingleton<WeaponManager>.Instance.UnlockWeapon(recipe.Result);
            for(int i = 0; i < _recipes.Count; i++)
            {
                if (_recipes[i].Result.ItemIdx == recipe.Result.ItemIdx)
                {
                    _recipes.RemoveAt(i);// 무기는 교환 한번만
                    break;
                }
            }

        }
        else
        {
            GenericSingleton<ItemSaver>.Instance.SubItem(recipe.First, recipe.First.Count);
            GenericSingleton<ItemSaver>.Instance.SubItem(recipe.Second, recipe.Second.Count);
            GenericSingleton<ItemSaver>.Instance.SubItem(recipe.Third, recipe.Third.Count);
            GenericSingleton<ItemSaver>.Instance.SubItem(recipe.Fourth, recipe.Fourth.Count);

            GenericSingleton<ItemSaver>.Instance.AddItem(recipe.Result);
        }
        

        CalExchange();
        GenericSingleton<UIBase>.Instance.InventoryInit();
        GenericSingleton<UIBase>.Instance.ExchangeUIInit();
        GenericSingleton<UIBase>.Instance.HealItemInit();

    }
    public void LoadRecipesData(List<RecipeData> recipes)
    {
        _recipes.Clear();
        foreach(RecipeData recipe in recipes)
        {
       
           _recipes.Add(new Recipe(new ItemData(recipe._first._idx, recipe._first._count), new ItemData(recipe._second._idx, recipe._second._count), new ItemData(recipe._third._idx, recipe._third._count), new ItemData(recipe._fourth._idx, recipe._fourth._count), new ItemData(recipe._result._idx, recipe._result._count), recipe._isWeapon));

        }

    }

}

[Serializable]
public class Recipe
{

    ItemData _first;
    public ItemData First { get { return _first; } }
    ItemData _second;
    public ItemData Second { get { return _second; } }
    ItemData _third;
    public ItemData Third { get { return _third; } }
    ItemData _fourth;
    public ItemData Fourth { get { return _fourth; } }
    ItemData _result;
    public ItemData Result { get { return _result; } }

    bool _isWeapon;
    public bool IsWeapon { get { return _isWeapon; } }

    bool[] _bools = new bool[0];
    public bool[] Bools { get { return _bools; } }
    bool _canExchange;
    public bool CanExchange { get { return _canExchange; } }

    public void SetCanExchange(bool canExchange) => _canExchange = canExchange;
    public void SetBools(bool[] bools) => _bools = bools;

    public Recipe(ItemData first, ItemData second, ItemData third, ItemData fourth, ItemData result, bool isWeapon)
    {
        _first = first;
        _second = second;
        _third = third;
        _fourth = fourth;
        _result = result;
        _isWeapon = isWeapon;
    }

}
