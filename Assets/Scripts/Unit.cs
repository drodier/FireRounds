using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private int currentHealth;
    private int maxHealth;
    private int currentMana;
    private int maxMana;
    private int maxMovement;
    private int initiative;

    public int getMovement()
    {
        return maxMovement;
    }

    public int getInitiative()
    {
        return initiative;
    }

    public int damageUnit(int damage)
    {
        return currentHealth = currentHealth - damage < 0 ? 0 : currentHealth - damage;
    }

    public int healUnit(int healAmount)
    {
        return currentHealth = currentHealth + healAmount < maxHealth ? maxHealth : currentHealth + healAmount;
    }
}
