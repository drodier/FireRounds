using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : UnitClass
{
    public Mage(int _level)
    {
        level = 1;
        maxHealth = 50 + level^2;
        maxMana = 100 + level^2;
        maxMovement = 3;
        initiative = Random.Range(0, 80 - level^2);
    }
}
