using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings")]
public class PlayerSettingsSO : ScriptableObject
{
    public int Speed;
    public float RotationSpeed;
    public int Health;
    public int Damage ;
    public float BulletSpeed;
}
