using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum NotesButtonType
{
    category,
    entry
}
public class UIButtonNotes : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Text  text;
    public Image bulletIcon;
    public Button btnDelete;

    public NotesButtonType type;

    public int categoryIndex ;
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
        if(bulletIcon != null) bulletIcon.gameObject.SetActive(false);
        if (btnDelete != null)
        {
            btnDelete.gameObject.SetActive(true);
            text.color = btnDelete.image.color;
        }

        switch (type)
        {
            case NotesButtonType.entry:
                break;
            case NotesButtonType.category:
                UIManager.Instance.UpdateNotesEntryContent(categoryIndex);
                break;
        }
    }
    private void Deselect()
    {
        if(bulletIcon != null) bulletIcon.gameObject.SetActive(true);
        if (btnDelete != null)
        {
            btnDelete.gameObject.SetActive(false);
            text.color = Color.black;
        }
    }

    public void OnSelectorClicked()
    {
        
    }

    private void OnDeleteClicked()
    {
        Debug.Log("delete item at category index: " + categoryIndex + ", entry index: "+ entryIndex);
    }
}
