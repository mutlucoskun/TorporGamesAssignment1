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
    public Button selector;

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
                if(selector !=null) selector.image.raycastTarget = false;
                break;
            case NotesButtonType.category:
                UIManager.Instance.UpdateNotesEntryContent(categoryIndex);
                break;
        }
    }
    private void Deselect()
    {
        StartCoroutine(DeselectDelay());
        if(selector !=null) selector.image.raycastTarget = true;
    }

    private IEnumerator DeselectDelay()
    {
        yield return new WaitForSeconds(.1f);
        
        if(bulletIcon != null) bulletIcon.gameObject.SetActive(true);
        if (btnDelete != null)
        {
            btnDelete.gameObject.SetActive(false);
            text.color = Color.black;
        }
    }
    public void OnDeleteClicked()
    {
        StopCoroutine(DeselectDelay());
        UIManager.Instance.RemoveNote(categoryIndex, entryIndex);
    }
}
