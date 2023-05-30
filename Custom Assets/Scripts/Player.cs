using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TaskData_Cs
{
    public TaskData_Cs(int taskID_pr, int taskFromID_pr, int taskToID_pr)
    {
        taskID = taskID_pr;
        taskFromID = taskFromID_pr;
        taskToID = taskToID_pr;
    }

    public TaskData_Cs() {}

    public int taskID;
    public int taskFromID;
    public int taskToID;
}

public class Player : MonoBehaviour
{
    
    //////////////////////////////////////////////////////////////////////
    // types
    //////////////////////////////////////////////////////////////////////
    #region types
    
    public enum GameState_En
    {
        Nothing, Inited, Moving, Idle
    }

    #endregion

    //////////////////////////////////////////////////////////////////////
    // fields
    //////////////////////////////////////////////////////////////////////
    #region fields

    //-------------------------------------------------- SerializeField
    [SerializeField]
    public GameObject robot_Pf;

    [SerializeField]
    public GameObject targetPoint_Pf;

    [SerializeField]
    public Transform targetPointHolder_Tf;

    [SerializeField]
    public Canvas canvas;

    [SerializeField]
    Transform robot_Tf;

    [SerializeField]
    List<Transform> compartment_Tfs = new List<Transform>();

    //-------------------------------------------------- public fields
    public GameState_En gameState;

    public List<TaskData_Cs> tasksData = new List<TaskData_Cs>();

    //-------------------------------------------------- private fields
    List<Point> targetPoint_Cps = new List<Point>();

    public int robotTaskID = -1;

    Vector3 nextTargetPosition = new Vector3();

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
        
    }

    //-------------------------------------------------- Update is called once per frame
    void Update()
    {
        if(gameState == GameState_En.Moving)
        {
            if(Mathf.Approximately(Vector3.Distance(robot_Tf.position, nextTargetPosition), 0f))
            {
                robotTaskID = GetRobotNextTaskID(robotTaskID);
                if(!IsValidTaskID(robotTaskID))
                {
                    return;
                }

                SetNextTargetPosition();
            }
        }
    }

    //--------------------------------------------------
    int GetRobotNextTaskID(int curTaskID_pr)
    {
        int nextTaskID_tp = -1;

        // 
        for(int i = 0; i < tasksData.Count; i++)
        {
            if(tasksData[i].taskID > curTaskID_pr)
            {
                nextTaskID_tp = tasksData[i].taskID;
                break;
            }
        }

        // 
        if(nextTaskID_tp == -1)
        {
            if(tasksData.Count > 0)
            {
                nextTaskID_tp = tasksData[0].taskID;
            }
        }

        return nextTaskID_tp;
    }

    //--------------------------------------------------
    bool IsValidTaskID(int curTaskID_pr)
    {
        bool result = false;

        for(int i = 0; i < tasksData.Count; i++)
        {
            if(tasksData[i].taskID == curTaskID_pr)
            {
                result = true;
                break;
            }
        }

        return result;
    }

    //--------------------------------------------------
    void SetNextTargetPosition()
    {
        if(IsValidTaskID(robotTaskID))
        {
            if(Mathf.Approximately(Vector3.Distance(robot_Tf.position,
                compartment_Tfs[tasksData[robotTaskID].taskFromID].position), 0f))
            {
                nextTargetPosition = compartment_Tfs[tasksData[robotTaskID].taskToID].position;
            }
            else
            {
                nextTargetPosition = compartment_Tfs[tasksData[robotTaskID].taskFromID].position;
            }
            
        }
        else
        {
            nextTargetPosition = robot_Tf.position;
        }
    }
    
    //////////////////////////////////////////////////////////////////////
    // Init
    //////////////////////////////////////////////////////////////////////
    #region Init

    //-------------------------------------------------- Init
    public void Init()
    {

        gameState = GameState_En.Inited;
    }

    #endregion

    //-------------------------------------------------- 
    public void OnFloorMapClicked(Vector3 collidePosition)
    {
        // 
        if(targetPoint_Cps.Count > 0)
        {
            if(targetPoint_Cps[targetPoint_Cps.Count - 1].pointID == 99)
                return;
        }

        // Convert the mouse click position to a local position within the canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
            collidePosition, canvas.worldCamera, out Vector2 localPoint);

        // Create a new image object as a child of the canvas object at the local position
        GameObject targetPoint_GO_tp = Instantiate(targetPoint_Pf, canvas.transform);
        targetPoint_GO_tp.transform.localPosition = localPoint;
        targetPoint_GO_tp.transform.SetParent(targetPointHolder_Tf);

        // 
        Point targetPoint_Cp_tp = targetPoint_GO_tp.GetComponent<Point>();

        if(targetPoint_Cps.Count == 0)
        {
            targetPoint_Cp_tp.pointID = 1;
        }
        else
        {
            targetPoint_Cp_tp.pointID = targetPoint_Cps[targetPoint_Cps.Count - 1].pointID + 1;
        }
        targetPoint_Cp_tp.pointPosition = localPoint;
        
        targetPoint_Cps.Add(targetPoint_Cp_tp);
    }

    //--------------------------------------------------
    public void OnClickTargetPoint(Point point_Cp_pr)
    {
        targetPoint_Cps.Remove(point_Cp_pr);
        Destroy(point_Cp_pr.gameObject);
    }

    //--------------------------------------------------
    public void Move()
    {
        if(!IsValidTaskID(robotTaskID))
        {
            robotTaskID = GetRobotNextTaskID(robotTaskID);

            if(!IsValidTaskID(robotTaskID))
            {
                return;
            }
        }

        if(gameState == GameState_En.Moving)
        {
            gameState = GameState_En.Idle;
            return;
        }
        else
        {
            gameState = GameState_En.Moving;
        }

        SetNextTargetPosition();
        
        robot_Tf.DOMove(nextTargetPosition, 3f);
    }

}
