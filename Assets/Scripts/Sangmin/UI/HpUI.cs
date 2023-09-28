
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpUI : MonoBehaviour
{
    [SerializeField] Slider _hpBar;
    [SerializeField] TextMeshProUGUI _hpText;
    
    public void Init()
    {
        float _hp = GenericSingleton<PlayerCon>.Instance.HpStat;
        _hpBar.value = _hp * 0.01f;
        _hpText.text = _hp + "/100";
    }
}
