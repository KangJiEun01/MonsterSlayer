using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpUI : MonoBehaviour
{
    [SerializeField] Image _hpBar;
    [SerializeField] TextMeshProUGUI _hpText;
    
    public void Init()
    {
        float _hp = GenericSingleton<PlayerCon>.Instance.HpStat;
        _hpBar.fillAmount = _hp * 0.01f;
        _hpText.text = _hp + "/100";
    }
}
