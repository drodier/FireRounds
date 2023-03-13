using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SideMenu : MonoBehaviour
{
    public Vector2 openPosition;
    public Vector2 closedPosition;
    public bool isOpen = false;
    public RectTransform drawer;
    public float animationSpeed = 100;
    public int direction = 0;
    public GameObject drawerObject;

    void FixedUpdate()
    {
        isOpen = CheckMouseOver();
        animateMenu();
    }

    private void animateMenu()
    {
        if(isOpen)
        {
            if(direction == -1 && drawer.anchoredPosition.x > openPosition.x ||
                direction == 1 && drawer.anchoredPosition.x < openPosition.x)
            {
                drawer.anchoredPosition += (new Vector2(animationSpeed * Time.deltaTime, 0)) * direction;
            }
            else
            {
                drawer.anchoredPosition = openPosition;
            }
        }
        else
        {
            if(direction == -1 && drawer.anchoredPosition.x < closedPosition.x ||
                direction == 1 && drawer.anchoredPosition.x > closedPosition.x)
            {
                drawer.anchoredPosition += (new Vector2(animationSpeed * Time.deltaTime, 0)) * -direction;
            }
            else
            {
                drawer.anchoredPosition = closedPosition;
            }
        }
    }
    
    public void toggleMenu()
    {
        isOpen = !isOpen;
    }

    public bool CheckMouseOver()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);

        for (int index = 0; index < raysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = raysastResults[index];
            if (curRaysastResult.gameObject == drawerObject)
                return true;
        }
        return false;
    }
}
