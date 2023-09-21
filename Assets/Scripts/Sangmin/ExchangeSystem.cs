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
    void LoadRecipeData()
    {
        // CSV 파일 로드 및 파싱
        TextAsset RecipeDataCSV = Resources.Load<TextAsset>("RecipeData"); // "ItemData"는 CSV 파일명

        StringReader reader = new StringReader(RecipeDataCSV.text);
        reader.ReadLine();
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            Debug.Log(values[1]);
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
            _recipes.Add(new Recipe(new ItemData(firstItemIdx, firstItemCount), new ItemData(SecondItemIdx, SecondItemCount), new ItemData(ThirdItemIdx, ThirdItemCount), new ItemData(FourthItemIdx, FourthItemCount), new ItemData(ResultItemIdx, ResultItemCount)));

        }
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
        recipe.SetBools(mats);
    }
    public void Exchange(Recipe recipe)
    {
        GenericSingleton<ItemSaver>.Instance.SubItem(recipe.First);
        GenericSingleton<ItemSaver>.Instance.SubItem(recipe.Second);
        GenericSingleton<ItemSaver>.Instance.SubItem(recipe.Third);
        GenericSingleton<ItemSaver>.Instance.SubItem(recipe.Fourth);

        GenericSingleton<ItemSaver>.Instance.AddItem(recipe.Result);

        CalExchange();
        GenericSingleton<UIBase>.Instance.InventoryUI.GetComponent<Inventory>().ReDrwing(_invenData);
        GenericSingleton<UIBase>.Instance.ExchangeUI.GetComponent<ExchangeUI>().Init();
    
    }
    
}
