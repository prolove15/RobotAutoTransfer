using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    //////////////////////////////////////////////////////////////////////
    // types
    //////////////////////////////////////////////////////////////////////
    #region types
    
    public enum GameState_En
    {
        Nothing, Inited
    }

    #endregion

    //////////////////////////////////////////////////////////////////////
    // fields
    //////////////////////////////////////////////////////////////////////
    #region fields

    //-------------------------------------------------- SerializeField
    [SerializeField]
    RectTransform taskSVContent_RT;

    [SerializeField]
    RectTransform floorImagePanel_RT;

    [SerializeField]
    List<RectTransform> compartmentContent_Cps = new List<RectTransform>();

    [SerializeField]
    Text taskTitle_Cp;

    [SerializeField]
    Dropdown taskFromDropD_Cp;

    [SerializeField]
    Dropdown taskToDropD_Cp;

    [SerializeField]
    Dropdown luggageDropD_Cp;

    [SerializeField]
    Animator descriptionAnim_Cp;

    [SerializeField]
    Text descriptionText_Cp;

    [SerializeField]
    GameObject taskBtn_Pf;

    [SerializeField]
    GameObject pointFrom_Pf;

    [SerializeField]
    GameObject pointTo_Pf;

    [SerializeField]
    GameObject robot_Pf;

    //-------------------------------------------------- public fields
    public GameState_En gameState;

    //-------------------------------------------------- private fields
    Controller controller_Cp;

    ScrollViewManager taskSVManager = new ScrollViewManager();

    List<ScrollViewManager> compartmentSVManagers = new List<ScrollViewManager>();

    int m_focusedTaskID;

    #endregion

    //////////////////////////////////////////////////////////////////////
    // properties
    //////////////////////////////////////////////////////////////////////
    #region properties

    //-------------------------------------------------- public properties

    //-------------------------------------------------- private properties
    int taskFromID
    {
        get
        {
            return taskFromDropD_Cp.value;
        }
    }

    int taskToID
    {
        get
        {
            return taskToDropD_Cp.value;
        }
    }

    int luggageID
    {
        get
        {
            return luggageDropD_Cp.value;
        }
    }

    int focusedTaskID
    {
        get
        {
            return m_focusedTaskID;
        }
        set
        {
            m_focusedTaskID = value;

            taskTitle_Cp.text = value >= 0 ? "Task " + (value + 1).ToString() : string.Empty;
        }
    }

    List<TaskData_Cs> tasksData
    {
        get { return controller_Cp.player_Cp.tasksData; }
    }

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
        
    }
    
    //////////////////////////////////////////////////////////////////////
    // Init
    //////////////////////////////////////////////////////////////////////
    #region Init

    //-------------------------------------------------- Init
    public void Init()
    {
        // 
        controller_Cp = GameObject.FindWithTag("GameController").GetComponent<Controller>();

        // 
        taskSVManager.Init(true, 10f, 10f);
        taskSVManager.targetSVContent_Cp = taskSVContent_RT;
        taskSVManager.item_Pf = taskBtn_Pf;

        // 
        for(int i = 0; i < compartmentContent_Cps.Count; i++)
        {
            compartmentSVManagers.Insert(i, new ScrollViewManager());
            compartmentSVManagers[i].Init(false, 0f, 5f);
            compartmentSVManagers[i].targetSVContent_Cp = compartmentContent_Cps[i];
            compartmentSVManagers[i].item_Pf = pointFrom_Pf;
            compartmentSVManagers[i].item2_Pf = pointTo_Pf;
        }
        
        // 
        focusedTaskID = -1;

        // 
        gameState = GameState_En.Inited;
    }

    #endregion

    //////////////////////////////////////////////////////////////////////
    // Event from UI
    //////////////////////////////////////////////////////////////////////
    #region Event from UI

    //--------------------------------------------------
    public void OnClickAddTaskBtn()
    {
        int taskID_tp = taskSVManager.verticalTaskID;

        // 
        Button_Custom taskBtn_Cp_tp = taskSVManager.AddItem();
        taskBtn_Cp_tp.taskID = taskID_tp;

        // 
        Button_Custom taskFromBtn_Cp_tp = compartmentSVManagers[taskFromID].AddItem(true);
        taskFromBtn_Cp_tp.taskID = taskID_tp;
        Button_Custom taskToBtn_Cp_tp = compartmentSVManagers[taskToID].AddItem(false);
        taskToBtn_Cp_tp.taskID = taskID_tp;

        // 
        focusedTaskID = taskID_tp;

        // 
        tasksData.Add(new TaskData_Cs(taskID_tp, taskFromID, taskToID));
    }

    //--------------------------------------------------
    public void OnClickTaskBtn(int id)
    {
        focusedTaskID = id;
    }

    //--------------------------------------------------
    public void OnClickRemoveTaskBtn(int id)
    {
        // 
        taskSVManager.RemoveItem(id);

        for(int i = 0; i < compartmentSVManagers.Count; i++)
        {
            compartmentSVManagers[i].RemoveItem(id);
        }

        // 
        focusedTaskID = taskSVManager.verticalTaskID - 1;

        // 
        for(int i = 0; i < tasksData.Count; i++)
        {
            if(tasksData[i].taskID == id)
            {
                tasksData.RemoveAt(i);
                break;
            }
        }
    }

    //--------------------------------------------------
    public void OnValueChangeTaskFromDropDown(int index)
    {
        // 
        if(focusedTaskID == -1)
        {
            return;
        }

        // 
        for(int i = 0; i < compartmentSVManagers.Count; i++)
        {
            compartmentSVManagers[i].RemoveHorizontalFromOrToItem(focusedTaskID, true);
        }

        Button_Custom taskFromBtn_Cp_tp = compartmentSVManagers[taskFromID].AddItem(true);
        taskFromBtn_Cp_tp.taskID = focusedTaskID;
        
        // 
        for(int i = 0; i < tasksData.Count; i++)
        {
            if(tasksData[i].taskID == focusedTaskID)
            {
                tasksData[i].taskFromID = taskFromID;
                break;
            }
        }
    }

    //--------------------------------------------------
    public void OnValueChangeTaskToDropDown(int index)
    {
        // 
        if(focusedTaskID == -1)
        {
            return;
        }

        // 
        for(int i = 0; i < compartmentSVManagers.Count; i++)
        {
            compartmentSVManagers[i].RemoveHorizontalFromOrToItem(focusedTaskID, false);
        }

        Button_Custom taskToBtn_Cp_tp = compartmentSVManagers[taskToID].AddItem(false);
        taskToBtn_Cp_tp.taskID = focusedTaskID;
        
        // 
        for(int i = 0; i < tasksData.Count; i++)
        {
            if(tasksData[i].taskID == focusedTaskID)
            {
                tasksData[i].taskToID = taskToID;
                break;
            }
        }
    }

    //--------------------------------------------------
    public void OnValueChangeLuggageDropDown(int index)
    {
        
    }

    //--------------------------------------------------
    public void OnPointEnterTaskFromDropDown()
    {
        descriptionText_Cp.text = "荷物を移動する開始エリアを選択してください。";
        // descriptionAnim_Cp.SetBool("show", true);
        // StartCoroutine(Corou_OnPointEnterTaskFromDropDown());
    }

    IEnumerator Corou_OnPointEnterTaskFromDropDown()
    {
        yield return null;
    }

    //--------------------------------------------------
    public void OnPointEnterTaskToDropDown()
    {
        descriptionText_Cp.text = "荷物を移動する最後のゾーンを選択してください。";
        
    }

    //--------------------------------------------------
    public void OnPointEnterLuaggeDropDown()
    {
        descriptionText_Cp.text = "運ぶ荷物を入力してください！";
        
    }

    //--------------------------------------------------
    public void OnPointExitTaskFromDropDown()
    {
        
    }

    //--------------------------------------------------
    public void OnPointExitTaskToDropDown()
    {
        
    }

    //--------------------------------------------------
    public void OnPointExitLuaggeDropDown()
    {
        
    }

    //--------------------------------------------------
    public void OnClickTransferBtn()
    {
        controller_Cp.player_Cp.Move();
    }

    #endregion

}
