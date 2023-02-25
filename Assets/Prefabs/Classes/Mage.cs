using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Unit
{
    public Mage(int _level)
    {
        menu.enabled = false;
        level = 1;
        currentHealth = maxHealth = 50 + level^2;
        currentMana = maxMana = 100 + level^2;
        maxMovement = 3;
        initiative = Random.Range(0, 80 - level^2);
    }
}
