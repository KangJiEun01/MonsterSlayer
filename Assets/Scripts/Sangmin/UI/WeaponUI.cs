
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] Sprite[] _weaponSprites;
    [SerializeField] Image _weaponImage;
    [SerializeField] TextMeshProUGUI _currentBullet;
    [SerializeField] TextMeshProUGUI _maxBullet;

    public void SetWeaponImage(int idx)
    {
        _weaponImage.sprite = _weaponSprites[idx];
    }
    public void SetCurrentBullet(int currentBullet)
    {
        _currentBullet.text = currentBullet.ToString();
    }
    public void SetMaxBullet(int maxBullet)
    {
        _maxBullet.text = " /" + maxBullet.ToString();
    }
    public void SetMelee()
    {
        _weaponImage.sprite = _weaponSprites[0];
        _currentBullet.text = "¡Ä";
        _maxBullet.text = " /¡Ä";
    }
    public void UIUpdate(WeaponBase weapon)
    {
        GenericSingleton<UIBase>.Instance.WeaponUI.GetComponent<WeaponUI>().SetWeaponImage(weapon.WeaponIdx);
        GenericSingleton<UIBase>.Instance.WeaponUI.GetComponent<WeaponUI>().SetMaxBullet(weapon.MaxBullet);
        GenericSingleton<UIBase>.Instance.WeaponUI.GetComponent<WeaponUI>().SetCurrentBullet(weapon.CurrentIdx);
    }
}
