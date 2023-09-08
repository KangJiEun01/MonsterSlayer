using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBarUI : MonoBehaviour
{
    Slider _hpbar01;
    void Start()
    {
        _hpbar01= GetComponent<Slider>();
    }
    void Update()
    {
        float _hpbar02pos = GenericSingleton<PlayerCon>.Instance.GetComponent<PlayerCon>().HpStat * 0.01f;
        _hpbar01.value= _hpbar02pos;
    }
}
