using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Point : MonoBehaviour
{
    
    //////////////////////////////////////////////////////////////////////
    // fields
    //////////////////////////////////////////////////////////////////////
    #region fields

    //-------------------------------------------------- SerializeField

    //-------------------------------------------------- public fields
    public Vector2 pointPosition;

    //-------------------------------------------------- private fields
    TMP_Text pointID_Cp;

    #endregion

    //////////////////////////////////////////////////////////////////////
    // properties
    //////////////////////////////////////////////////////////////////////
    #region properties

    //-------------------------------------------------- public properties
    public int pointID
    {
        get
        {
            if(!pointID_Cp)
            {
                pointID_Cp = gameObject.GetComponentInChildren<TMP_Text>();
            }

            return int.Parse(pointID_Cp.text);
        }
        set
        {
            if(!pointID_Cp)
            {
                pointID_Cp = gameObject.GetComponentInChildren<TMP_Text>();
            }

            pointID_Cp.text = value.ToString();
        }
    }

    //-------------------------------------------------- private properties

    #endregion

    //////////////////////////////////////////////////////////////////////
    // methods
    //////////////////////////////////////////////////////////////////////

    //-------------------------------------------------- Start is called before the first frame update
    void Start()
    {
        
    }

    //-------------------------------------------------- OnClickTargetPoint
    public void OnClickTargetPoint()
    {
        GameObject.FindWithTag("GameController").GetComponent<Controller>().player_Cp
            .OnClickTargetPoint(this);
    }

}
