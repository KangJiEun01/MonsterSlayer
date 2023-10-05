using UnityEngine;
using UnityEngine.UI;

public class DashCoolUI : MonoBehaviour
{
    [SerializeField] Image _backImage;
    public void DashCoolUIUpdate()
    {
        float cooldownRemaining = Mathf.Max(0, GenericSingleton<PlayerCon>.Instance.DashCool - (Time.time - GenericSingleton<PlayerCon>.Instance.LastDashTime));
        _backImage.fillAmount = 1 -(cooldownRemaining / GenericSingleton<PlayerCon>.Instance.DashCool);

    }
}