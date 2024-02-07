using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/EnemySettings")]
public class EnemySettingsSO : ScriptableObject
{
    public int Damage;
    public float BulletSpeed;
    public int Health;
    public int CoinsDroped;
    public int HealthBonusPercent;
    public int DamageBoosterPercent;
    public int DamageBoosterTimer;
    public int SpeedBoosterPercent;
    public int SpeedBoosterTimer;
    public int DropItemProbability;
}
