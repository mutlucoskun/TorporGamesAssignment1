using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Button btnObj;
    public Text  text;
    public Image icon;
    public Image iconSelected;

    public int categoryIndex ;
    public int topicIndex ;
    public int entryIndex ;
    
    public void OnSelect(BaseEventData eventData)
    {
        Select();
    }
    public void OnDeselect(BaseEventData eventData)
    {
        Deselect();
    }
    private void Select()
    {
        if(icon != null) icon.enabled = false;
        if(iconSelected != null) iconSelected.enabled = true;
        btnObj.Select();

        if (entryIndex >= 0)
        {
            UIManager.Instance.UpdateEntryContent(categoryIndex, topicIndex, entryIndex);
        }
        else if (topicIndex >= 0)
        {
            UIManager.Instance.UpdateEntryContent(categoryIndex, topicIndex, 0);
            UIManager.Instance.UpdateEntryButtons(categoryIndex, topicIndex);
        }
        else if (categoryIndex >= 0)
        {
            UIManager.Instance.UpdateEntryContent(categoryIndex, 0, 0);
            UIManager.Instance.UpdateEntryButtons(categoryIndex, 0);
            UIManager.Instance.UpdateTopicButtons(categoryIndex);
        }
    }
    private void Deselect()
    {
        if(icon != null) icon.enabled = true;
        if(iconSelected != null) iconSelected.enabled = false;
    }
}
