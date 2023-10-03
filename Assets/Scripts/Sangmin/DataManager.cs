using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : GenericSingleton<DataManager>
{
    GameDataWrapper _gameDatas = new GameDataWrapper();
    public void Init()
    {
        _gameDatas._datas.Add(new GameData());
    }
    public void SaveData(int idx)
    {
        Debug.Log("데이터 저장");
        GameData data = _gameDatas._datas[idx];
        data.SaveWeaponData();
        data.SaveRecipeData(GenericSingleton<ExchangeSystem>.Instance.Recipes);
        data.SaveItemData(GenericSingleton<ItemSaver>.Instance.Datas._items);
        string json = JsonUtility.ToJson(_gameDatas);
        Debug.Log(json);
        string filePath = Path.Combine(Application.persistentDataPath, "GameData.json");
        File.WriteAllText(filePath, json);


    }
    public void LoadData(int idx)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "GameData.json");
        string json = File.ReadAllText(filePath);
        _gameDatas = JsonUtility.FromJson<GameDataWrapper>(json);

        Debug.Log("데이터 로드");

        GameData data = _gameDatas._datas[idx];
        GenericSingleton<ExchangeSystem>.Instance.LoadRecipesData(data._recipeDatas);
        GenericSingleton<ItemSaver>.Instance.LoadItemData(data._itemDatas);
        GenericSingleton<ExchangeSystem>.Instance.RecipeUpdate();
        GenericSingleton<WeaponManager>.Instance.LoadWeaponData(data._weaponData);
        GenericSingleton<PlayerCon>.Instance.Init();
        GenericSingleton<UIBase>.Instance.Init();
        GenericSingleton<WeaponManager>.Instance.UIUpdate();



    }
}

[Serializable]
public class GameDataWrapper
{
    public List<GameData> _datas = new List<GameData>();
   
}
[Serializable]
public class GameData
{

    public List<ItemSource> _itemDatas = new List<ItemSource>();
    public List<RecipeData> _recipeDatas = new List<RecipeData>();
    public WeaponData _weaponData;

    public void SaveRecipeData(List<Recipe> recipes)
    {

        _recipeDatas.Clear();
        foreach (Recipe recipe in recipes)
        {
            _recipeDatas.Add(new RecipeData(recipe.First, recipe.Second, recipe.Third, recipe.Fourth, recipe.Result, recipe.IsWeapon));
        }

    }

    public void SaveItemData(Dictionary<int, ItemData> items)
    {
        _itemDatas.Clear();
        foreach (var item in items)
        {
            _itemDatas.Add(new ItemSource(item.Value.ItemIdx,item.Value.Count));
        }
    }
    public void SaveWeaponData()
    {
        _weaponData = new WeaponData();
        _weaponData._activeWeaponIdx = GenericSingleton<WeaponManager>.Instance.ActiveWeaponIdx;
        if (GenericSingleton<WeaponManager>.Instance.CurrentWeapons[0] != null)
        {
            _weaponData._currentMainIdx = GenericSingleton<WeaponManager>.Instance.CurrentWeapons[0].WeaponIdx;
        }
        else _weaponData._currentMainIdx = 0;
    }

}
[Serializable]
public class WeaponData
{
    public List<int> _activeWeaponIdx;
    public int _currentMainIdx;
}
[Serializable]
public class ItemSource
{
    public int _idx;
    public int _count;
    public ItemSource(int idx, int count)
    {
        _idx = idx;
        _count = count;
    }
}
[Serializable]
public class RecipeData
{
    public ItemSource _first;

    public ItemSource _second;

    public ItemSource _third;

    public ItemSource _fourth;

    public ItemSource _result;

    public bool _isWeapon;
    public RecipeData(ItemData first, ItemData second, ItemData third, ItemData fourth, ItemData result, bool isWeapon)
    {
        _first = new ItemSource(first.ItemIdx, first.Count);
        _second = new ItemSource(second.ItemIdx, second.Count);
        _third = new ItemSource(third.ItemIdx, third.Count);
        _fourth = new ItemSource(fourth.ItemIdx, fourth.Count);
        _result = new ItemSource(result.ItemIdx, result.Count);
        _isWeapon = isWeapon;
    }
}

