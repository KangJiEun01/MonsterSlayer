using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerpos : MonoBehaviour
{
    [SerializeField] GameObject player; 
    void Start()
    {
        player.transform.position = new Vector3(14.8f, 2f, 50.59f);
    }
}
