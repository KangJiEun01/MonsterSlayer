using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BoltScripts : MonoBehaviour
{
    [SerializeField] Transform[] _pos;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<VisualEffect>().SetVector4("Color",new Vector4(8,0,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        _pos[0].position = new Vector3(Random.Range(-250,250f), 100, Random.Range(-250,250));
        _pos[3].position = new Vector3(Random.Range(-250,250f), 0, Random.Range(-250,250));
        
    }
}
