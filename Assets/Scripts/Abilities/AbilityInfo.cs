using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Ability")]
public class AbilityInfo : ScriptableObject
{
    public string AbilityName;
    public string AbilityDescription;
    public int AbilityCooldown;
}
