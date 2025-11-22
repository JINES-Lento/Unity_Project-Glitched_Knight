using UnityEngine;

public class CooldownUI : MonoBehaviour
{
    public UnityEngine.UI.Image fill;
    private float maxCooldown = 3f;
    private float currentCooldown = 3f;

    public void SetMaxCooldown(in float value)
    {
        maxCooldown = value;
        UpdateFiilAmount();
    }

    public void SetCurrentCooldown(in float value)
    {
        currentCooldown = value;
        UpdateFiilAmount();
    }

    private void UpdateFiilAmount()
    {
        fill.fillAmount = currentCooldown / maxCooldown;
    }

}