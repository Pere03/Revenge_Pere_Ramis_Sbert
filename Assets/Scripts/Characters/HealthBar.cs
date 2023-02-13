using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image HealthBar_Img;

    public void UpdateHealthBar(float MaxHealt, float CurrentHealth)
    {
        HealthBar_Img.fillAmount = CurrentHealth / MaxHealt;
    }
}
