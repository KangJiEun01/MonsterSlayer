using UnityEngine;
using UnityEngine.UI;

public class RhinoHPbar : MonoBehaviour
{
    [SerializeField] GameObject m_goHpBar;
    [SerializeField] GameObject Rhino;

    void Update()
    {
        // 오브젝트에 따른 HP Bar 위치 이동
        //m_goHpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));
       m_goHpBar.transform.position =new Vector3( Rhino.transform.position.x, Rhino.transform.position.y + 2f, Rhino.transform.position.z);
    }
}
