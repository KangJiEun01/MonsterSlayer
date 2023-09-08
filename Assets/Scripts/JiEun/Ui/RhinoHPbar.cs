using UnityEngine;

public class RhinoHPbar : MonoBehaviour
{
    [SerializeField] GameObject HpBar;
    [SerializeField] GameObject detection;
    [SerializeField] GameObject Rhino;
    [SerializeField] GameObject camera;

    void Update()
    {
        transform.LookAt(camera.transform);
        // ������Ʈ�� ���� HP Bar ��ġ �̵�
        //m_goHpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));
        HpBar.transform.position =new Vector3( Rhino.transform.position.x, Rhino.transform.position.y + 2f, Rhino.transform.position.z);
       detection.transform.position = new Vector3(Rhino.transform.position.x, Rhino.transform.position.y + 2.6f, Rhino.transform.position.z);
    }

}
