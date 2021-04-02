using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public string longAttackName, shortAttackName;
    [HideInInspector] public bool shortRange = false, longRange = false, canAttack = false;

    /// <summary>
    /// Starts calling the method that will decide which attack to perform after certain fixed amount of time.
    /// </summary>
    public void StartAttacking()
    {
        if (!canAttack)
        {
            canAttack = true;
            InvokeRepeating("ChooseAttack", 0, 4.2f);
        }
    }

    /// <summary>
    /// Stops calling the method that will decide which attack to perform after certain fixed amount of time.
    /// </summary>
    public void StopAttacking()
    {
        if (canAttack && !shortRange && !longRange)
        {
            canAttack = false;
            CancelInvoke("ChooseAttack");
        }
    }

    /// <summary>
    /// Randomly chooses an attack by checking certain conditions and performs it.
    /// </summary>
    protected void ChooseAttack()
    {
        string[] attacks = { longAttackName, shortAttackName };

        if (longRange && !shortRange)
        {
            animator.SetTrigger(longAttackName);
        }
        
        else if (longRange && shortRange)
        {
            animator.SetTrigger(attacks[Random.Range(0, attacks.Length)]);
        }
    }

}
