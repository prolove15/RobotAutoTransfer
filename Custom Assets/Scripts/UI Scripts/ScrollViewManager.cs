using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewManager
{
    
    //////////////////////////////////////////////////////////////////////
    // types
    //////////////////////////////////////////////////////////////////////
    #region types
    
    delegate Button_Custom Del_AddItem();

    delegate Button_Custom Del_AddItemHorizontal(bool isFrom);

    delegate void Del_RemoveItem(int itemID_pr);

    delegate void Del_RefreshContent();

    #endregion

    //////////////////////////////////////////////////////////////////////
    // fields
    //////////////////////////////////////////////////////////////////////
    #region fields

    //-------------------------------------------------- public fields
    public RectTransform targetSVContent_Cp;

    public GameObject item_Pf, item2_Pf;

    public bool isVertical;

    public float baseOffset;

    public float itemInterval;

    //-------------------------------------------------- private fields
    List<RectTransform> item_RTs = new List<RectTransform>();

    float itemLength;

    Del_AddItem addItem;

    Del_AddItemHorizontal addItemHorizon;

    Del_RemoveItem removeItem;

    Del_RefreshContent refreshContent;

    #endregion

    //////////////////////////////////////////////////////////////////////
    // properties
    //////////////////////////////////////////////////////////////////////
    #region properties

    //-------------------------------------------------- public properties
    public int itemCount
    {
        get { return item_RTs.Count; }
    }

    public int verticalTaskID
    {
        get
        {
            if(itemCount == 0)
            {
                return 0;
            }

            return item_RTs[itemCount - 1].GetComponent<Button_Custom>().taskID + 1;
        }
    }

    //-------------------------------------------------- private properties

    #endregion

    //////////////////////////////////////////////////////////////////////
    // methods
    //////////////////////////////////////////////////////////////////////

    //-------------------------------------------------- Init
    public void Init(bool isVertical_pr, float baseOffset_pr, float itemInterval_pr)
    {
        isVertical = isVertical_pr;
        baseOffset = baseOffset_pr;
        itemInterval = itemInterval_pr;

        if(isVertical)
        {
            addItem += AddVerticalItem;
            removeItem += RemoveVerticalItem;
            refreshContent += RefreshVerticalContent;
        }
        else
        {
            addItemHorizon += AddHorizontalItem;
            removeItem += RemoveHorizontalItem;
            refreshContent += RefreshHorizontalContent;
        }
    }

    //-------------------------------------------------- Add item
    public Button_Custom AddItem(bool isFrom = true)
    {
        Button_Custom itemBtn_Cp_tp = null;

        if(isVertical)
        {
            itemBtn_Cp_tp = addItem.Invoke();
        }
        else
        {
            itemBtn_Cp_tp = addItemHorizon.Invoke(isFrom);
        }

        RefreshContent();

        return itemBtn_Cp_tp;
    }

    //--------------------------------------------------
    Button_Custom AddHorizontalItem(bool isFrom)
    {
        // Create the item game object. 
        GameObject item_Pf_tp = isFrom ? item_Pf : item2_Pf;

        GameObject newItemObj = GameObject.Instantiate(item_Pf_tp, targetSVContent_Cp); 

        newItemObj.GetComponent<Button_Custom>().isFrom = isFrom;
    
        // Set the local position of the item to the right side based on the index and item length. 
        RectTransform newItemRT = newItemObj.GetComponent<RectTransform>(); 
        newItemRT.localPosition = new Vector3(itemLength + baseOffset + itemInterval, 0f, 0f); 
    
        // Add the item to the list of items. 
        item_RTs.Add(newItemRT); 
    
        // Increment the item length. 
        itemLength += newItemRT.rect.width + itemInterval; 
    
        return newItemObj.GetComponent<Button_Custom>();
    }

    //--------------------------------------------------
    Button_Custom AddVerticalItem()
    {
        // Create the item game object. 
        GameObject newItemObj = GameObject.Instantiate(item_Pf, targetSVContent_Cp); 
    
        // Set the local position of the item to the top side based on the index and item length. 
        RectTransform newItemRT = newItemObj.GetComponent<RectTransform>(); 
        newItemRT.localPosition = new Vector3(0f, -(itemLength + baseOffset + itemInterval), 0f); 
    
        // Add the item to the list of items. 
        item_RTs.Add(newItemRT); 
    
        // Increment the item length. 
        itemLength += newItemRT.rect.height + itemInterval; 

        return newItemObj.GetComponent<Button_Custom>(); 
    }

    //-------------------------------------------------- Remove item
    public void RemoveItem(int itemID_pr)
    {
        removeItem.Invoke(itemID_pr);

        RefreshContent();
    }

    //--------------------------------------------------
    void RemoveHorizontalItem(int itemID_pr)
    {
        // Find the item to be removed in the list, and remove it. 
        for (int i = 0; i < item_RTs.Count; i++) 
        { 
            if (item_RTs[i].GetComponent<Button_Custom>().taskID == itemID_pr) 
            { 
                RectTransform removedItemRT = item_RTs[i]; 
                item_RTs.RemoveAt(i); 
                i--;
                
                GameObject.Destroy(removedItemRT.gameObject);
            } 
        } 
    
        // Recalculate the item length. 
        itemLength = 0f; 
        foreach (RectTransform itemRT in item_RTs) 
        { 
            itemRT.localPosition = new Vector3(itemLength + baseOffset + itemInterval, 0f, 0f); 
            itemLength += itemRT.rect.width + itemInterval; 
        } 
    }

    //--------------------------------------------------
    public void RemoveHorizontalFromOrToItem(int itemID_pr, bool isFrom)
    {
        // Find the item to be removed in the list, and remove it. 
        for (int i = 0; i < item_RTs.Count; i++) 
        { 
            if (item_RTs[i].GetComponent<Button_Custom>().taskID == itemID_pr &&
                item_RTs[i].GetComponent<Button_Custom>().isFrom == isFrom) 
            { 
                RectTransform removedItemRT = item_RTs[i]; 
                item_RTs.RemoveAt(i); 
                GameObject.Destroy(removedItemRT.gameObject);
                break;
            } 
        } 
    
        // Recalculate the item length. 
        itemLength = 0f; 
        foreach (RectTransform itemRT in item_RTs) 
        { 
            itemRT.localPosition = new Vector3(itemLength + baseOffset + itemInterval, 0f, 0f); 
            itemLength += itemRT.rect.width + itemInterval; 
        } 
    }
    
    //--------------------------------------------------
    void RemoveVerticalItem(int itemID_pr)
    {
        // Find the item to be removed in the list, and remove it. 
        for (int i = 0; i < item_RTs.Count; i++) 
        { 
            if (item_RTs[i].GetComponent<Button_Custom>().taskID == itemID_pr) 
            { 
                RectTransform removedItemRT = item_RTs[i]; 
                item_RTs.RemoveAt(i); 
                GameObject.Destroy(removedItemRT.gameObject); 
                break; 
            } 
        } 
    
        // Recalculate the item length. 
        itemLength = 0f; 
        foreach (RectTransform itemRT in item_RTs) 
        { 
            itemRT.localPosition = new Vector3(0f, -(itemLength + baseOffset + itemInterval), 0f); 
            itemLength += itemRT.rect.height + itemInterval; 
        } 
    }

    //-------------------------------------------------- Refresh content
    void RefreshContent()
    {
        refreshContent.Invoke();
    }

    //-------------------------------------------------- 
    void RefreshHorizontalContent()
    {
        // Reset the item length. 
        itemLength = 0f; 
    
        // Reposition the items. 
        // int i = 0;
        foreach (RectTransform itemRT in item_RTs) 
        { 
            itemRT.localPosition = new Vector3(itemLength + baseOffset + itemInterval, 0f, 0f); 
            itemLength += itemRT.rect.width + itemInterval; 

            // itemRT.GetComponent<Button_Custom>().taskID = i;
            // i++;
        } 

        targetSVContent_Cp.sizeDelta = new Vector2(itemLength, targetSVContent_Cp.sizeDelta.y);
    }

    //-------------------------------------------------- 
    void RefreshVerticalContent()
    {
        // Reset the item length. 
        itemLength = 0f; 
    
        // Reposition the items. 
        foreach (RectTransform itemRT in item_RTs) 
        { 
            itemRT.localPosition = new Vector3(0f, -(itemLength + baseOffset + itemInterval), 0f); 
            itemLength += itemRT.rect.height + itemInterval; 
        } 
        
        targetSVContent_Cp.sizeDelta = new Vector2(targetSVContent_Cp.sizeDelta.x, itemLength);
    }

}
