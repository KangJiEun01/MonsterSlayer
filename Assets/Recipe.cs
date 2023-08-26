using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    (int idx, int count) _firstM;
    public (int idx, int count) FirstM { get { return _firstM; } }
    (int idx, int count) _secondM;
    public (int idx, int count) SecondM { get { return _secondM; } }
    (int idx, int count) _thirdM;
    public (int idx, int count) ThirdM { get { return _thirdM; } }
    (int idx, int count) _fourthM;
    public (int idx, int count) FourthM { get { return _fourthM; } }
    (int idx, int count) _result;
    public (int idx, int count) Result { get { return _result; } }
    public Recipe((int idx, int count) first, (int idx, int count) second, (int idx, int count) third, (int idx, int count) fourth, (int idx, int count) result)
    {
        _firstM = first;
        _secondM = second;
        _thirdM = third;
        _fourthM = fourth;
        _result = result;
    }
    private void Start()
    {
        // 버튼 클릭 이벤트 핸들러 연결
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnPointerClick);
    }

    // 아이템 클릭 시 호출되는 함수
    public void OnPointerClick()
    {
        GameObject[] recipes = GameObject.FindGameObjectsWithTag("Recipe");
        foreach (GameObject recipe in recipes)
        {
            recipe.GetComponent<Image>().color = new Color(119, 152, 184);
            
        }
        Image image = gameObject.GetComponent<Image>();
        image.color = new Color(192, 165, 136);
        Debug.Log(gameObject.name);
    }
}