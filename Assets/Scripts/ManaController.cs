using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    public GameObject unit;
    public Transform container;

    void FixedUpdate()
    {
        float delta = (float)unit.GetComponent<Unit>().getMana() / (float)unit.GetComponent<Unit>().getMaxMana();
        transform.localScale = new Vector2(delta, 1);
        transform.position = new Vector3(container.position.x - (1-transform.localScale.x)/1.33f, container.position.y, -1);
    }
}
