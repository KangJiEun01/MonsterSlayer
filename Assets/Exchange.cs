using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exchange : GenericSingleton<Exchange>
{
    List<Recipe> _recipes = new List<Recipe>();
    public List<Recipe> Recipes { get { return _recipes; } }
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
        _recipes.Add(new Recipe((0, 3), (-1, 0), (-1, 0), (-1, 0), (2, 1))); //���3�� ��Ÿ500 1��
        _recipes.Add(new Recipe((2, 3), (-1, 0), (-1, 0), (-1, 0), (3, 1))); //��Ÿ500 3�� ����1��
        _recipes.Add(new Recipe((2, 3), (0, 2), (-1, 0), (-1, 0), (3, 3))); //��Ÿ500 3�� ���2�� ���� 3�� �׽�Ʈ��

    }
    
}
