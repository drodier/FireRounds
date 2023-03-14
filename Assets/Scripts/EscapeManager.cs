using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EscapeManager : MonoBehaviour
{
    public TMP_Text InfoMessage;
    public bool isSaved = true;
    public string mapName;
    
    private bool isOpen = false;

    public void ToggleMenu()
    {
        GetComponent<Canvas>().enabled = isOpen = !isOpen;

        Cursor.lockState = GetComponent<Canvas>().enabled ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void SaveMap()
    {
        WriteJsonFile();

        InfoMessage.text = mapName + " was saved !";
        isSaved = true;
    }

    public void ExitBuilder()
    {
        if(!isSaved)
        {
            InfoMessage.text = "You Sure ? \n Map was not saved since last edit.";
        }
    }

    private void WriteJsonFile()
    {

    }
}
