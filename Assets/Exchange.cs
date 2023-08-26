using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exchange : GenericSingleton<Exchange>
{
    List<Recipe> _recipes = new List<Recipe>();
    public List<Recipe> Recipes { get { return _recipes; } }
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
        _recipes.Add(new Recipe((0, 3), (-1, 0), (-1, 0), (-1, 0), (2, 1))); //허브3개 비타500 1개
        _recipes.Add(new Recipe((2, 3), (-1, 0), (-1, 0), (-1, 0), (3, 1))); //비타500 3개 과자1개
        _recipes.Add(new Recipe((2, 3), (0, 2), (-1, 0), (-1, 0), (3, 3))); //비타500 3개 허브2개 과자 3개 테스트용

    }
    
}
