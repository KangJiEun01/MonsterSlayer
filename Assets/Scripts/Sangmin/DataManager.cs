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
        WeaponManager wm = GenericSingleton<WeaponManager>.Instance;
        data.SaveWeaponData(wm.ActiveWeapons, wm.CurrentWeapons, wm.CurrentWeapon);
        data.SaveRecipeData(GenericSingleton<ExchangeSystem>.Instance.Recipes);
        data.SaveItemData(GenericSingleton<ItemSaver>.Instance.Datas._items);
        Debug.Log(_gameDatas);
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
        GenericSingleton<WeaponManager>.Instance.LoadWeaponData(data._activeWeapons, data._currentWeapons, data._currentWeapon);
        GenericSingleton<ExchangeSystem>.Instance.LoadRecipesData(data._recipeDatas);
        GenericSingleton<ItemSaver>.Instance.LoadItemData(data._itemDatas);
        GenericSingleton<WeaponManager>.Instance.WeaponLoad();
        GenericSingleton<PlayerCon>.Instance.Init();
        GenericSingleton<UIBase>.Instance.Init();



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
    public List<WeaponBase> _activeWeapons;
    public WeaponBase _currentWeapon;

    public WeaponBase[] _currentWeapons;


    public List<ItemSource> _itemDatas = new List<ItemSource>();
    public List<RecipeData> _recipeDatas = new List<RecipeData>();



    public void SaveWeaponData(List<WeaponBase> activeWeapons, WeaponBase[] currentWeapons, WeaponBase currentWeapon)
    {
        _activeWeapons = activeWeapons;
        _currentWeapon = currentWeapon;
        _currentWeapons = currentWeapons;
    }


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

}

