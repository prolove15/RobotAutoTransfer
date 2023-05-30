using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Custom : Button
{
    
    //--------------------------------------------------
    UIManager uiManager_Cp;

    int m_taskID;

    //--------------------------------------------------
    public bool isFrom;

    //--------------------------------------------------
    public int taskID
    {
        get { return m_taskID; }
        set
        {
            m_taskID = value;

            string text = string.Empty;
            if(gameObject.name.Contains("Task"))
            {
                text = "Task " + (value + 1).ToString();
            }
            else if(gameObject.name.Contains("Point"))
            {
                text = (value + 1).ToString();
            }

            gameObject.GetComponentInChildren<Text>().text = text;
        }
    }



    //--------------------------------------------------
    new void Start()
    {
        uiManager_Cp = GameObject.FindWithTag("GameController").GetComponent<Controller>()
            .uiManager_Cp;
    }

    //--------------------------------------------------
    public void OnClickTaskBtn()
    {
        uiManager_Cp.OnClickTaskBtn(taskID);
    } 

    //--------------------------------------------------
    public void OnClickRemoveTaskBtn()
    {
        uiManager_Cp.OnClickRemoveTaskBtn(taskID);
    }

}
