using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
 
    ItemData _first;
    public ItemData First { get { return _first; } }
    ItemData _second;
    public ItemData Second { get { return _second; } }
    ItemData _third;
    public ItemData Third { get { return _second; } }
    ItemData _fourth;
    public ItemData Fourth { get {  return _fourth; } }
    ItemData _result;
    public ItemData Result { get { return _result; } }
    bool _canExchange;
    public bool CanExchange { get { return _canExchange; } }

    public void SetCanExchange(bool canExchange) => _canExchange = canExchange;

    public Recipe(ItemData first, ItemData second,ItemData third, ItemData fourth, ItemData result)
    {
        _first = first;
        _second = second;
        _third = third;
        _fourth = fourth;
        _result = result;
    }
 
}