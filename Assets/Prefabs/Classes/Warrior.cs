using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : UnitClass
{
    public Warrior(int _level)
    {
        level = _level;
        maxHealth = 100 + level^2;
        maxMana = 50 + level^2;
        maxMovement = 3;
        initiative = Random.Range(0, 80 - level^2);
    }
}
