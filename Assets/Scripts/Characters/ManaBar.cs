using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Image ManaBar_Img;

    public void UpdateManaBar(float MaxMana, float CurrentMana)
    {
        ManaBar_Img.fillAmount = CurrentMana / MaxMana;
    }
}
