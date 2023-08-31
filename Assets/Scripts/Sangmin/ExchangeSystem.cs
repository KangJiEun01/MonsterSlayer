using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class ExchangeSystem : GenericSingleton<ExchangeSystem>
{
    List<Recipe> _recipes = new List<Recipe>();
    public List<Recipe> Recipes { get { return _recipes; } }
    List<ItemData> _invenData = new List<ItemData>();
    //약초 = 0
    //고기 = 1
    //익힌 고기 = 2
    //비타500 = 3
    //과자 = 4
    //도시락 = 5 
    //톱니 = 6
    //건전지 = 7 
    //렌치 = 8
    //화약 = 9
    //라이터 = 10
    //열쇠 = 11
    //철판 = 12
    void Start()
    {
        Init();
    }
    public void Init()
    {
        //최대 4가지재료 (첫번째 재료,갯수) (두번째 재료,갯수) (세번째 재료,갯수) (네번째 재료,갯수) (결과,갯수)
        //재료가 적게필요하면 -1 넣기
        _invenData = GenericSingleton<ItemSaver>.Instance.Datas._itemList;
        _recipes.Add(new Recipe(new ItemData(0, 3), new ItemData(-1, 0), new ItemData(-1, 0), new ItemData(-1, 0), new ItemData(2, 1))); //허브3개 비타500 1개
        _recipes.Add(new Recipe(new ItemData(2, 3), new ItemData(-1, 0), new ItemData(-1, 0), new ItemData(-1, 0), new ItemData(3, 1))); //비타500 3개 과자1개
        _recipes.Add(new Recipe(new ItemData(2, 3), new ItemData(0, 2), new ItemData(-1, 0), new ItemData(-1, 0), new ItemData(3, 3))); //비타500 3개 허브2개 과자 3개 테스트용

    }
    public bool[] ExchangeEnable(Recipe recipe)
    {
        bool[] mats = new bool[] { false, false, false, false };
        foreach (ItemData item in _invenData)
        {
            if (recipe.First.Idx == -1) mats[0] = true;
            else if (recipe.First.Idx == item.Idx)
            {
                if(item.Count >= recipe.First.Count) mats[0] = true;   
            }
            if (recipe.Second.Idx == -1) mats[1] = true;
            else if(recipe.Second.Idx == item.Idx)
            {
                if (item.Count >= recipe.Second.Count) mats[1] = true;
            }
            if (recipe.Third.Idx == -1) mats[2] = true;
            else if(recipe.Third.Idx == item.Idx)
            {
                if (item.Count >= recipe.Third.Count) mats[2] = true;
            }
            if (recipe.Fourth.Idx == -1) mats[3] = true;
            else if(recipe.Fourth.Idx == item.Idx)
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
        return mats;
    }
    public void Exchange(Recipe recipe)
    {
        bool _ishaving = false;
        foreach (ItemData item in _invenData)
        {
            if (recipe.First.Idx == item.Idx)
            {
                item.SetCount(item.Count- recipe.First.Count);
            }

            if (recipe.Second.Idx == item.Idx)
            {
                item.SetCount(item.Count - recipe.Second.Count);
            }

            if (recipe.Third.Idx == item.Idx)
            {
                item.SetCount(item.Count - recipe.Third.Count);
            }

            if (recipe.Fourth.Idx == item.Idx)
            {
                item.SetCount(item.Count - recipe.Fourth.Count);
            }

            if (recipe.Result.Idx == item.Idx)
            {
                _ishaving = true;
                item.SetCount(item.Count + recipe.Result.Count);
            }
        }
        if (!_ishaving)
        {
            _invenData.Add(new ItemData(recipe.Result.Idx, recipe.Result.Count));
        }
        GenericSingleton<Inventory>.Instance.ReDrwing(_invenData);
        GenericSingleton<ExchangeUI>.Instance.Init();
            
    }
    
}
