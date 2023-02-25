using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Unit
{
    public Warrior(int _level)
    {
        menu.enabled = false;
        level = 1;
        currentHealth = maxHealth = 100 + level^2;
        currentMana = maxMana = 50 + level^2;
        maxMovement = 4;
        initiative = Random.Range(0, 100 - level^2);
    }
}
