using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class ExchangeSystem : GenericSingleton<ExchangeSystem>
{
    List<Recipe> _recipes = new List<Recipe>();
    public List<Recipe> Recipes { get { return _recipes; } }
    List<ItemData> _invenData = new List<ItemData>();
    //���� = 0
    //��� = 1
    //���� ��� = 2
    //��Ÿ500 = 3
    //���� = 4
    //���ö� = 5 
    //��� = 6
    //������ = 7 
    //��ġ = 8
    //ȭ�� = 9
    //������ = 10
    //���� = 11
    //ö�� = 12
    void Start()
    {
        Init();
    }
    public void Init()
    {
        //�ִ� 4������� (ù��° ���,����) (�ι�° ���,����) (����° ���,����) (�׹�° ���,����) (���,����)
        //��ᰡ �����ʿ��ϸ� -1 �ֱ�
        _invenData = GenericSingleton<ItemSaver>.Instance.Datas._itemList;
        _recipes.Add(new Recipe((0, 3), (-1, 0), (-1, 0), (-1, 0), (2, 1))); //���3�� ��Ÿ500 1��
        _recipes.Add(new Recipe((2, 3), (-1, 0), (-1, 0), (-1, 0), (3, 1))); //��Ÿ500 3�� ����1��
        _recipes.Add(new Recipe((2, 3), (0, 2), (-1, 0), (-1, 0), (3, 3))); //��Ÿ500 3�� ���2�� ���� 3�� �׽�Ʈ��

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
        if (mats[0] && mats[1] && mats[2] && mats[3])                       //�ŷ������Ұ��
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
