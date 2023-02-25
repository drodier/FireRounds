using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Canvas menu;
    public TileLogic currentTile;
    public Vector2 position;


    protected int level;
    protected int currentHealth;
    protected int maxHealth;
    protected int currentMana;
    protected int maxMana;
    protected int maxMovement;
    protected int initiative;


    public void FixedUpdate()
    {
        transform.position = new Vector3(position.x, position.y, -1);
    }
    
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

    public int getMana()
    {
        return currentMana;
    }

    public int getMaxMana()
    {
        return maxMana;
    }

    public int getHealth()
    {
        return currentHealth;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public int healUnit(int healAmount)
    {
        return currentHealth = currentHealth + healAmount < maxHealth ? maxHealth : currentHealth + healAmount;
    }

    public void toggleMenu()
    {
        menu.enabled = !menu.enabled;
    }

    public void showMovement()
    {
        FindObjectOfType<MapLogic>().showMovementRange(this);
    }

    public void move(TileLogic tile)
    {
        currentTile = tile;
        position = currentTile.transform.position;
    }

    public void DebugUnit()
    {
        currentHealth -= 1;
        currentMana -= 1;
    }
}
