using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitComparer : IComparer
{
    public int Compare(object x, object y)
    {
        return (((Unit)x).getInitiative() == ((Unit)y).getInitiative() ? 0 
                : ((Unit)x).getInitiative() < ((Unit)y).getInitiative() ? -1 
                : 1);
    }
}
