using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Canvas menu = new Canvas();
    public TileLogic currentTile;
    public Vector2 position;
    public UnitClass unitClass;


    public int currentHealth;
    public int currentMana;    
    protected bool isActive = false;


    public void FixedUpdate()
    {
        transform.position = new Vector3(position.x, currentTile.GetComponent<TileLogic>().stats.height, position.y);
    }
    
    public int getMovement()
    {
        return unitClass.maxMovement;
    }

    public int getInitiative()
    {
        return unitClass.initiative;
    }

    public int damageUnit(int damage)
    {
        return currentHealth = currentHealth - damage < 0 ? 0 : currentHealth - damage;
    }

    public int getMana()
    {
        return currentMana;
    }

    public int getMaxMana()
    {
        return unitClass.maxMana;
    }

    public int getHealth()
    {
        return currentHealth;
    }

    public int getMaxHealth()
    {
        return unitClass.maxHealth;
    }

    public bool getActive()
    {
        return isActive;
    }

    public void toggleActive()
    {
        isActive = !isActive;
        toggleMenu();
    }

    public int healUnit(int healAmount)
    {
        return currentHealth = currentHealth + healAmount < unitClass.maxHealth ? unitClass.maxHealth : currentHealth + healAmount;
    }

    public void toggleMenu()
    {
        menu.enabled = menu.enabled ? false : isActive;
    }

    public void showMovement()
    {
        FindObjectOfType<MapLogic>().showMovementRange(this);
        toggleMenu();
    }

    public void move(TileLogic tile)
    {
        currentTile = tile;
        position = currentTile.transform.position;
        toggleMenu();
    }

    public void endTurn()
    {
        FindObjectOfType<UnitsManager>().NextTurn();
        toggleMenu();
    }

    public void DebugUnit()
    {
        currentHealth -= 1;
        currentMana -= 1;
    }

    public int Compare(object other)
    {
        return getInitiative() == ((Unit)other).getInitiative() ? 0 
                : (getInitiative() < ((Unit)other).getInitiative() ? -1 
                : 1);
    }
}
