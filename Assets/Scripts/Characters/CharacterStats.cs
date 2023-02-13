using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    public int level;
    public int exp;
    public int[] expToLevelUp;
    public int[] maxHealthLevels;
    public int[] DamageLevels;
    private HealthManager _healthManager;

    public TMP_Text LevelTxt;
    public TMP_Text LevelStat;
    public TMP_Text ExpStat;
    void Start()
    {
        _healthManager = GetComponent<HealthManager>();
        AddExperience(0);
    }

    private void Update()
    {
        LevelTxt.text = level.ToString();
        LevelStat.text = level.ToString();
        ExpStat.text = exp.ToString();
    }

    public void AddExperience(int expToAdd)
    {
        exp += expToAdd;
        if (level >= expToLevelUp.Length) { return; }
        if (exp >= expToLevelUp[level])
        {
            level++;
            exp -= expToLevelUp[level - 1];
            _healthManager.UpdateMaxHealth(maxHealthLevels[level]);

        }
    }
}
