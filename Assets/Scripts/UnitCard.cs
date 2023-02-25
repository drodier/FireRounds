using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitCard : MonoBehaviour
{
    public TextMeshProUGUI unitName;
    public TextMeshProUGUI unitInitiative;
    
    public void SetUnit(Unit unit)
    {
        unitName.text = unit.name;
        unitInitiative.text = unit.getInitiative().ToString();
    }
}
