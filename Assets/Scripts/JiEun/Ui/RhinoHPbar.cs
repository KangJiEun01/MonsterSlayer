using UnityEngine;
using UnityEngine.UI;

public class RhinoHPbar : MonoBehaviour
{
    [SerializeField] GameObject m_goHpBar;
    [SerializeField] GameObject Rhino;

    void Update()
    {
        // ������Ʈ�� ���� HP Bar ��ġ �̵�
        //m_goHpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));
       m_goHpBar.transform.position =new Vector3( Rhino.transform.position.x, Rhino.transform.position.y + 2f, Rhino.transform.position.z);
    }
}
