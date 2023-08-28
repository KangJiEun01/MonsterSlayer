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
        _recipes.Add(new Recipe((0, 3), (-1, 0), (-1, 0), (-1, 0), (2, 1))); //허브3개 비타500 1개
        _recipes.Add(new Recipe((2, 3), (-1, 0), (-1, 0), (-1, 0), (3, 1))); //비타500 3개 과자1개
        _recipes.Add(new Recipe((2, 3), (0, 2), (-1, 0), (-1, 0), (3, 3))); //비타500 3개 허브2개 과자 3개 테스트용

    }
    public bool[] ExchangeEnable(Recipe recipe)
    {
        bool[] mats = new bool[] { false, false, false, false };
        foreach (ItemData item in _invenData)
        {
            if (recipe.FirstM.idx == -1) mats[0] = true;
            else if (recipe.FirstM.idx == item.Idx)
            {
                if(item.Count >= recipe.FirstM.count) mats[0] = true;   
            }
            if (recipe.SecondM.idx == -1) mats[1] = true;
            else if(recipe.SecondM.idx == item.Idx)
            {
                if (item.Count >= recipe.SecondM.count) mats[1] = true;
            }
            if (recipe.ThirdM.idx == -1) mats[2] = true;
            else if(recipe.ThirdM.idx == item.Idx)
            {
                if (item.Count >= recipe.ThirdM.count) mats[2] = true;
            }
            if (recipe.FourthM.idx == -1) mats[3] = true;
            else if(recipe.FourthM.idx == item.Idx)
            {
                if (item.Count >= recipe.FourthM.count) mats[3] = true;
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
            if (recipe.FirstM.idx == item.Idx)
            {
                item.SetCount(item.Count- recipe.FirstM.count);
            }

            if (recipe.SecondM.idx == item.Idx)
            {
                item.SetCount(item.Count - recipe.SecondM.count);
            }

            if (recipe.ThirdM.idx == item.Idx)
            {
                item.SetCount(item.Count - recipe.ThirdM.count);
            }

            if (recipe.FourthM.idx == item.Idx)
            {
                item.SetCount(item.Count - recipe.FourthM.count);
            }

            if (recipe.Result.idx == item.Idx)
            {
                _ishaving = true;
                item.SetCount(item.Count + recipe.Result.count);
            }
        }
        if (!_ishaving)
        {
            _invenData.Add(new ItemData(recipe.Result.idx, recipe.Result.count));
        }
        GenericSingleton<Inventory>.Instance.ReDrwing(_invenData);
        GenericSingleton<ExchangeUI>.Instance.Init();
            
    }
    
}
