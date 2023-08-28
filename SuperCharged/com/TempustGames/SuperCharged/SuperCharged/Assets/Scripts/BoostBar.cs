using UnityEngine;
using UnityEngine.UI;

public class BoostBar : MonoBehaviour
{
    public Image bar;
    public Charger charger;

    void Update()
    {
        bar.fillAmount = charger.smallBoostCharge / charger.maxSmallBoost;
    }
}
