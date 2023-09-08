using UnityEngine;
using UnityEngine.UI;

public class HpbarUi : MonoBehaviour
{

    [SerializeField] Image _hpbar01;
    [SerializeField] Image _hpbar02;
    [SerializeField] Transform _hpbar02trans;
    [SerializeField] Transform _hpbar03trans;
    void Update()
    {
        float _hpbar02pos = GenericSingleton<PlayerCon>.Instance.GetComponent<PlayerCon>().HpStat * 0.01f;
        _hpbar02.fillAmount= _hpbar02pos;
        float Fillpos = (1796f*_hpbar02pos) + 65.8f; //중간바 뒤에 붙여주기
        Vector3 _hp03pos = new Vector3((Fillpos), _hpbar02trans.position.y, 0);
        _hpbar03trans.position = _hp03pos;
    }
}
