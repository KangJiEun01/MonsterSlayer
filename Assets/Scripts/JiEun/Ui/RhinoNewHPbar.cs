using UnityEngine.UI;
using UnityEngine;

public class RhinoNewHPbar : MonoBehaviour
{
    [SerializeField] GameObject HpBar;
    [SerializeField] GameObject detection;
    [SerializeField] GameObject Rhino;
    [SerializeField] GameObject camera;

    [SerializeField] float HPBarYpos;
    [SerializeField] float detectionYpos;


    Slider _hpbar01;
    void Start()
    {
        _hpbar01 = GetComponent<Slider>();
    }
    void Update()
    {
        float _hpbar02pos = 100;
        _hpbar01.value = _hpbar02pos * 0.01f;
        transform.LookAt(camera.transform);
        HpBar.transform.position = new Vector3(Rhino.transform.position.x, Rhino.transform.position.y + 1.8f, Rhino.transform.position.z);
        detection.transform.position = new Vector3(Rhino.transform.position.x, Rhino.transform.position.y + 2.4f, Rhino.transform.position.z);
    }
    public void TargetHp()
    {

    }
}
