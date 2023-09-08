using TMPro;
using UnityEngine;

public class PlayerHpTextUi : MonoBehaviour
{
    TextMeshProUGUI Hp;
    private void Start()
    {
        Hp=GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        float _hp = GenericSingleton<PlayerCon>.Instance.GetComponent<PlayerCon>().HpStat;
        Hp.text = _hp + "/100";
    }
}
