using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : GenericSingleton<DataManager>
{
    GameDataWrapper _gameDatas = new GameDataWrapper();
    public void Init()
    {
        _gameDatas._datas.Add(0,new GameData());
    }
    public void SaveData(int idx)
    {
       
        if (_gameDatas._datas.ContainsKey(idx))
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

        else
        {
            Debug.LogError("해당 인덱스의 GameData가 없습니다.");
        }

    }
    public void LoadData(int idx)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "GameData.json");
        string json = File.ReadAllText(filePath);
        _gameDatas = JsonUtility.FromJson<GameDataWrapper>(json);

        Debug.Log("데이터 로드");
        if (_gameDatas._datas.ContainsKey(idx))
        {
            
            GameData data = _gameDatas._datas[idx];
            GenericSingleton<WeaponManager>.Instance.LoadWeaponData(data.ActiveWeapons,data.CurrentWeapons,data.CurrentWeapon);
            GenericSingleton<ExchangeSystem>.Instance.LoadRecipesData(data.RecipeDatas);
            GenericSingleton<ItemSaver>.Instance.LoadItemData(data.ItemDatas);

        }
        else
        {
            Debug.LogError("해당 인덱스의 GameData가 없습니다.");
        }

    }
}

[Serializable]
public class GameDataWrapper
{
    public Dictionary<int, GameData> _datas = new Dictionary<int, GameData>();
}
[Serializable]
public class GameData
{
    List<WeaponBase> _activeWeapons;
    public List<WeaponBase> ActiveWeapons { get { return _activeWeapons; } }
    WeaponBase _currentWeapon;
    public WeaponBase CurrentWeapon { get { return _currentWeapon; } }
    WeaponBase[] _currentWeapons;
    public WeaponBase[] CurrentWeapons { get { return _currentWeapons; } }
    Dictionary<int, ItemData> _itemDatas;
    public Dictionary<int, ItemData> ItemDatas { get { return _itemDatas; } }
    List<Recipe> _recipeDatas;
    public List<Recipe> RecipeDatas { get { return _recipeDatas; } }


    public void SaveWeaponData(List<WeaponBase> activeWeapons, WeaponBase[] currentWeapons, WeaponBase currentWeapon)
    {
        _activeWeapons = activeWeapons;
        _currentWeapon = currentWeapon;
        _currentWeapons = currentWeapons;
    }
    public void LoadWeaponData(List<WeaponBase> activeWeapons, WeaponBase[] currentWeapons, WeaponBase currentWeapon)
    {
        GenericSingleton<WeaponManager>.Instance.LoadWeaponData(activeWeapons, currentWeapons, currentWeapon);
    }

    public void SaveRecipeData(List<Recipe> recipes)
    {
        _recipeDatas = recipes;
    }
    public void LoadRecipeData(List<Recipe> recipes)
    {
        GenericSingleton<ExchangeSystem>.Instance.LoadRecipesData(recipes);
    }
    public void SaveItemData(Dictionary<int, ItemData> items)
    {
        _itemDatas = items;
    }
    public void LoadItemData(Dictionary<int, ItemData> items)
    {
        GenericSingleton<ItemSaver>.Instance.LoadItemData(items);
    }
}

