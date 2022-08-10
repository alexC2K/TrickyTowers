using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public AbilityInfo AbilityInstanceInfo;
    public int AbilityCooldownRemaining;
    
    public abstract void UseAbility();
}
