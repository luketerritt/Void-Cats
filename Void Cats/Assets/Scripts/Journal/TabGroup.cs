using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    
    public List<TabButton> tabButtons;      // list of the tab buttons
    public Sprite tabIdle;                  // when the tab is idle
    public Sprite tabHover;                 // when the tab is being hovered over
    public Sprite tabActive;                // when the tab is active or selected
    public TabButton selectedTab;           // storing what tab is selected
    public List<GameObject> objectsToSwap;  // The pages that swap when tabs are pressed
   
    // takes in the buttons
    public void Subscribe(TabButton button)
    {
        // when the first button is "subscribed" to the group
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();     // creates new list of tab buttons
        }
        tabButtons.Add(button);                     //add the button to our list 
    }

    // changes the state of the buttons when interacted with
    public void OnTabEnter(TabButton button)
    {
        
        ResetTabs();                                        // resets the tabs to idle
        if (selectedTab == null || button != selectedTab)   // only changes the spirte if the tab isnt already selected
        {
            button.background.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();    // resets the tabs to idle
    }

    public void OnTabSelected(TabButton button)
    {
        if(selectedTab != null)         // checks if a tab already exists
        {
            selectedTab.Deselect();     // then deselects it
        }
        selectedTab = button;

        selectedTab.Select();

        ResetTabs(); // resets the tabs to idle
        button.background.sprite = tabActive;
        // goes through object to swap
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if(i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }
    // resets the tabs to idle
    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            // skips over the selected tab
            if(selectedTab != null & button == selectedTab)
            {
                continue;
            }
            // resets the other tabs
            button.background.sprite = tabIdle;
        }
    }
}
