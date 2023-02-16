using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : Unit
{
    public Canvas menu;

    // Start is called before the first frame update
    void Start()
    {
        menu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleMenu()
    {
        menu.enabled = !menu.enabled;
    }
}
