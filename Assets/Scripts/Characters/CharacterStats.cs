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
    public int[] TornadoDamageLevels;
    private HealthManager _healthManager;
    [SerializeField] private WeaponDamage _Damage;
    [SerializeField] private TornadoDamage TDamage;

    public TMP_Text LevelTxt;
    public TMP_Text LevelStat;
    public TMP_Text ExpStat;
    public TMP_Text Health;
    public TMP_Text Damage;

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
        Health.text = _healthManager.maxHealth.ToString();
        Damage.text = _Damage.maxDamage.ToString();
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
            _Damage.UpdateDamage(DamageLevels[level]);
            //TDamage.UpdateTornadoDamage(TornadoDamageLevels[level]);
        }
    }
}
