using UnityEngine;
using UnityEngine.UI;
public class RhinoHPbar : MonoBehaviour
{
    [SerializeField] GameObject HpBar;
    [SerializeField] GameObject detection;
    [SerializeField] GameObject Rhino;
    GameObject camera;

    Target targetScript;
    Image target;

    private void Start()
    {
        target=GetComponent<Image>();
        camera = Camera.main.gameObject;
    }
    void Update()
    {
        transform.LookAt(camera.transform);
        // ������Ʈ�� ���� HP Bar ��ġ �̵�
        //m_goHpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));
        HpBar.transform.position =new Vector3( Rhino.transform.position.x, Rhino.transform.position.y + 2.15f, Rhino.transform.position.z);
        detection.transform.position = new Vector3(Rhino.transform.position.x, Rhino.transform.position.y + 2.7f, Rhino.transform.position.z);
        float hp= Rhino.GetComponent<Target>().GetHP() * 0.01f;
        target.fillAmount = hp;
    }

}
