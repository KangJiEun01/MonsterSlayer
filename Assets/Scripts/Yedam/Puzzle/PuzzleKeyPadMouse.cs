using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleKeyPadMouse : MonoBehaviour
{
    bool _canPress = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(_canPress == true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("puzzle_Keypad")))
                {
                    hit.collider.GetComponent<Animator>().SetTrigger("PressKey");
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Cursor.visible = true;
        _canPress= true;
    }
    private void OnTriggerExit(Collider other)
    {
        Cursor.visible=false;
        _canPress= false;
    }
}
