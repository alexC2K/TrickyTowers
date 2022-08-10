using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : Ability
{
    [SerializeField] GameObject CurrentBlock;
    float StartTime;
    bool PetrifyBlock = false;
    
    public override void UseAbility()
    {
        if (AbilityCooldownRemaining > 0)
            return;
        
        switch (AbilityInstanceInfo.AbilityName)
        {
            case "Freeze Time":
                AbilityFreezeTime();
                break;
            case "Petrify Block":
                AbilityPetrifyBlock();
                break;
            case "Remove Last Block":
                AbilityRemoveBlock();
                break;
        }

        AbilityCooldownRemaining = AbilityInstanceInfo.AbilityCooldown;
    }

    void Start()
    {
        // Initialize the cooldowns
        AbilityCooldownRemaining = AbilityInstanceInfo.AbilityCooldown;
        StartTime = Time.time;

        // Get the current block after it is instantiated
        BlockSpawner.OnBlockSpawned += BlockSpawner_OnBlockSpawned;
        BlockController.OnCollisionEnterDetection += BlockController_OnCollisionEnterDetection;
    }

    void OnDestroy()
    {
        BlockSpawner.OnBlockSpawned -= BlockSpawner_OnBlockSpawned;
        BlockController.OnCollisionEnterDetection -= BlockController_OnCollisionEnterDetection;
    }

    void Update()
    {
        // If I'm on cooldown, reduce the cooldown
        if(Time.time - StartTime > 1 && AbilityCooldownRemaining >= 1)
        {
            AbilityCooldownRemaining -= 1;
            StartTime = Time.time;
        }
    }

    // Freeze the current block for 10 seconds then unfreeze it
    public void AbilityFreezeTime()
    {
        CurrentBlock.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        StartCoroutine(UnfreezeTime());
    }
    
    // Petrify the current block = set it's body type to Static once it collides with another block
    public void AbilityPetrifyBlock()
    {
        PetrifyBlock = true;
    }

    // Remove last placed block
    public void AbilityRemoveBlock()
    {
        GameObject LastBlock = FindObjectOfType<BlockSpawner>().GetLastBlock();
        if (LastBlock != null)
        {
            Destroy(LastBlock);
        }
    }

    IEnumerator UnfreezeTime()
    {
        yield return new WaitForSeconds(10f);

        CurrentBlock.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }

    public void BlockSpawner_OnBlockSpawned()
    {
        CurrentBlock = FindObjectOfType<BlockController>().GetCurrentBlock();
    }

    // Petrify the block only if I used the ability
    public void BlockController_OnCollisionEnterDetection()
    {
        if (PetrifyBlock)
        {
            CurrentBlock.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            PetrifyBlock = false;
        }
    }
}
