using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetecterOnUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,
    IPointerExitHandler
{
    
    //////////////////////////////////////////////////////////////////////
    // fields
    //////////////////////////////////////////////////////////////////////
    #region fields

    //-------------------------------------------------- SerializeField
    [SerializeField]
    string detecterID;

    //-------------------------------------------------- public fields
    
    //-------------------------------------------------- private fields
    Controller controller_Cp;

    UIManager uiManager_Cp;

    #endregion

    //////////////////////////////////////////////////////////////////////
    // properties
    //////////////////////////////////////////////////////////////////////
    #region properties

    //-------------------------------------------------- public properties

    //-------------------------------------------------- private properties

    #endregion

    //////////////////////////////////////////////////////////////////////
    // methods
    //////////////////////////////////////////////////////////////////////

    //-------------------------------------------------- Start is called before the first frame update
    void Start()
    {
        controller_Cp = GameObject.FindWithTag("GameController").GetComponent<Controller>();

        uiManager_Cp = controller_Cp.uiManager_Cp;
    }

    //-------------------------------------------------- 
    public void OnPointerClick(PointerEventData eventData)
    {
        // 
        if(string.Compare(gameObject.name, "FloorImage Panel",
            System.StringComparison.OrdinalIgnoreCase) == 0)
        {
            controller_Cp.player_Cp.OnFloorMapClicked(eventData.position);
        }
        else if(string.Compare(gameObject.name, "Point Image(Clone)",
            System.StringComparison.OrdinalIgnoreCase) == 0)
        {
            gameObject.GetComponent<Point>().OnClickTargetPoint();
        }

        // 
        if(string.Compare(detecterID, "TaskFrom") == 0)
        {
            uiManager_Cp.OnPointEnterTaskFromDropDown();
        }
        else if(string.Compare(detecterID, "TaskTo") == 0)
        {
            uiManager_Cp.OnPointEnterTaskToDropDown();
        }
        else if(string.Compare(detecterID, "Luggage") == 0)
        {
            uiManager_Cp.OnPointEnterLuaggeDropDown();
        }
    }

    //--------------------------------------------------
    public void OnPointerEnter(PointerEventData eventData)
    {
        // if(string.Compare(detecterID, "TaskFrom") == 0)
        // {
        //     uiManager_Cp.OnPointEnterTaskFromDropDown();
        // }
        // else if(string.Compare(detecterID, "TaskTo") == 0)
        // {
        //     uiManager_Cp.OnPointEnterTaskToDropDown();
        // }
        // else if(string.Compare(detecterID, "Luggage") == 0)
        // {
        //     uiManager_Cp.OnPointEnterLuaggeDropDown();
        // }
    }

    //--------------------------------------------------
    public void OnPointerExit(PointerEventData eventData)
    {
        // if(string.Compare(detecterID, "TaskFrom") == 0)
        // {
        //     uiManager_Cp.OnPointExitTaskFromDropDown();
        // }
        // else if(string.Compare(detecterID, "TaskTo") == 0)
        // {
        //     uiManager_Cp.OnPointExitTaskToDropDown();
        // }
        // else if(string.Compare(detecterID, "Luggage") == 0)
        // {
        //     uiManager_Cp.OnPointExitLuaggeDropDown();
        // }
    }

}
