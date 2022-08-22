using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum CodexButtonType
{
    category,
    topic,
    entry
}


public class UIButtonCodex : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Button btnObj;
    public Text  text;
    public Image icon;
    public Image iconSelected;

    public CodexButtonType type;
    
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
        if(icon != null) icon.gameObject.SetActive(false);
        if(iconSelected != null) iconSelected.gameObject.SetActive(true);
        if(btnObj != null) btnObj.Select();

        switch (type)
        {
            case CodexButtonType.entry:
                UIManager.Instance.UpdateCodexEntryContent(categoryIndex, topicIndex, entryIndex);
                break;
            case CodexButtonType.topic:
                UIManager.Instance.UpdateCodexEntryContent(categoryIndex, topicIndex, 0);
                UIManager.Instance.UpdateCodexEntryButtons(categoryIndex, topicIndex);
                break;
            case CodexButtonType.category:
                UIManager.Instance.UpdateCodexEntryContent(categoryIndex, 0, 0);
                UIManager.Instance.UpdateCodexEntryButtons(categoryIndex, 0);
                UIManager.Instance.UpdateCodexTopicButtons(categoryIndex);
                break;
        }
        
    }
    private void Deselect()
    {
        if(icon != null) icon.gameObject.SetActive(true);
        if(iconSelected != null) iconSelected.gameObject.SetActive(false);
    }
}
