using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    public Unit unit;
    public Transform container;

    void Start()
    {
            container.position = new Vector3(unit.position.x + (1 - (unit.getMana() / unit.getMaxMana())), unit.position.y + 0.6f, 0);
    }

    void FixedUpdate()
    {
        float delta = (float)unit.getMana() / (float)unit.getMaxMana();
        transform.localScale = new Vector2(delta, 1);
        transform.position = new Vector3(container.position.x - (1-transform.localScale.x)/1.33f, container.position.y, -1);
    }
}
