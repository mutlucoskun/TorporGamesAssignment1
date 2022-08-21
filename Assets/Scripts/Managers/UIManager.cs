using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<UIManager>();
            return _instance;
        }
    }
    #endregion
    
    [Header("Main Views")]
    public GameObject codexView;
    public GameObject notesView;
    [Header("Codex Elements")] 
    public GameObject codexCategoryButtons;
    public GameObject codexTopicButtons;
    public GameObject codexEntryButtons;
    public GameObject codexEntryLayout;
    public Text codexEntryTitle;
    public Image codexEntryImage;
    public Text codexEntryText;
    [Header("Codex Prefabs")] 
    public GameObject codexCategoryBtn;
    public GameObject codexTopicBtn;
    public GameObject codexEntryBtn;
    [Header("Notes Elements")] 
    public GameObject notesCategoryButtons;
    public GameObject notesInput;
    public GameObject notesEntryButtons;
    public UIButtonCodex addNoteButton;
    [Header("Notes Prefabs")] 
    public GameObject notesCategoryBtn;
    public GameObject notesEntryBtn;
    
    #region Codex
    private CodexMenuData _codexMenuData;
    private void InitCodexCategories()
    {
        ScrollRect catScrollRect = codexCategoryButtons.GetComponent<ScrollRect>();
        ClearChildren(catScrollRect.content.transform);
        if (catScrollRect != null)
        {
            for (int i = 0; i < _codexMenuData.CodexMenuItems.Count; i++)
            {
                GameObject codexMenuBtn = Instantiate(codexCategoryBtn, catScrollRect.content);
                UIButtonCodex btn = codexMenuBtn.GetComponent<UIButtonCodex>();
                if(i==0) btn.OnSelect(null);
                //A category button only has category index 
                btn.categoryIndex = i;
                btn.topicIndex = -1;
                btn.entryIndex = -1;
                if (_codexMenuData.CodexMenuItems[i].icon != null && btn.icon != null)
                {
                    Sprite s = Resources.Load<Sprite>("UI/" + _codexMenuData.CodexMenuItems[i].icon);
                    if (s != null)
                        btn.icon.sprite = s;
                }
                if (_codexMenuData.CodexMenuItems[i].iconSelected != null && btn.iconSelected != null)
                {
                    Sprite s = Resources.Load<Sprite>("UI/" + _codexMenuData.CodexMenuItems[i].iconSelected);
                    if (s != null)
                        btn.iconSelected.sprite = s;
                }
                if (_codexMenuData.CodexMenuItems[i].text != null && btn.text != null)
                {
                    btn.text.text = LanguageManager.Instance.Translate(_codexMenuData.CodexMenuItems[i].text);
                }
            }
        }
    }
    public void UpdateTopicButtons(int categoryIndex)
    {
        //On Codex Category Selection Update the Title Buttons
        ScrollRect tpcScrollRect = codexTopicButtons.GetComponent<ScrollRect>();
        ClearChildren(tpcScrollRect.content.transform);
        if (tpcScrollRect != null)
        {
            CodexMenuItem menuItem = _codexMenuData.CodexMenuItems[categoryIndex];
            if (menuItem == null)
                return;
            for (int i = 0; i < menuItem.topicButtons.Count; i++)
            {
                GameObject codexTopicButton = Instantiate(codexTopicBtn, tpcScrollRect.content);
                UIButtonCodex btn = codexTopicButton.GetComponent<UIButtonCodex>();
                if(i==0) btn.OnSelect(null);
                //A topip button only has the category and topic indexes
                btn.categoryIndex = categoryIndex;
                btn.topicIndex = i;
                btn.entryIndex = -1;
                if (menuItem.topicButtons[i].text != null && btn.text != null)
                {
                    btn.text.text = LanguageManager.Instance.Translate(menuItem.topicButtons[i].text);
                }
            }
        }
    }
    public void UpdateEntryButtons(int categoryIndex, int topicIndex)
    {
        //On Codex Title Selection Update the Entry Buttons
        ScrollRect entScrollRect = codexEntryButtons.GetComponent<ScrollRect>();
        ClearChildren(entScrollRect.content.transform);
        if (entScrollRect != null)
        {
            CodexMenuItem menuItem = _codexMenuData.CodexMenuItems[categoryIndex];
            if (menuItem == null)
                return;
            CodexTopic topicItem = menuItem.topicButtons[topicIndex];
            if (topicItem == null)
                return;
            for (int i = 0; i < topicItem.entryButtons.Count; i++)
            {
                GameObject codexEntryButton = Instantiate(codexEntryBtn, entScrollRect.content);
                UIButtonCodex btn = codexEntryButton.GetComponent<UIButtonCodex>();
                if(i==0) btn.OnSelect(null);
                //An entry button has all three indexes
                btn.categoryIndex = categoryIndex;
                btn.topicIndex = topicIndex;
                btn.entryIndex = i;
                if (topicItem.entryButtons[i].text != null && btn.text != null)
                {
                    btn.text.text = LanguageManager.Instance.Translate(topicItem.entryButtons[i].text);
                }
            }
        }
    }
    public void UpdateEntryContent(int categoryIndex, int topicIndex, int entryIndex)
    {
        //On Codex Entry Selection Update the Entry Title, Image and Text
        RectTransform entryLayout = codexEntryLayout.GetComponent<RectTransform>();
        codexEntryTitle.text = "";
        codexEntryText.text = "";
        codexEntryImage.gameObject.SetActive(false);
        if (entryLayout != null)
        {
            CodexMenuItem menuItem = _codexMenuData.CodexMenuItems[categoryIndex];
            if (menuItem == null)
                return;
            CodexTopic topicItem = menuItem.topicButtons[topicIndex];
            if (topicItem == null)
                return;
            CodexEntry entryItem = topicItem.entryButtons[entryIndex];
            if (entryItem == null)
                return;

            codexEntryTitle.text = entryItem.entryTitle;
            codexEntryText.text = entryItem.entryText;

            Sprite s = Resources.Load<Sprite>("UI/" + entryItem.entryImage);
            if (s != null)
            {
                codexEntryImage.sprite = s;
                codexEntryImage.gameObject.SetActive(true);
            }
            else
            {
                codexEntryImage.gameObject.SetActive(false);
            }
        }
    }
    
    private void InitCodexEntries()
    {
        //We start with the first item selected.
        UpdateEntryButtons(0,0);
        UpdateEntryContent(0,0, 0);
    }
    private void InitCodexTopics()
    {
        UpdateTopicButtons(0);
    }
    #endregion

    #region Notes

    private NotesMenuData _notesMenuData;
    private void InitNotesCategories()
    {
        ScrollRect catScrollRect = notesCategoryButtons.GetComponent<ScrollRect>();
        ClearChildren(catScrollRect.content.transform);
        if (catScrollRect != null)
        {
            for (int i = 0; i < _notesMenuData.NotesMenuItems.Count; i++)
            {
                GameObject notesMenuBtn = Instantiate(notesCategoryBtn, catScrollRect.content);
                UIButtonNotes btn = notesMenuBtn.GetComponent<UIButtonNotes>();
                btn.OnSelect(null);
                //A category button only has the category index 
                btn.categoryIndex = i;
                btn.entryIndex = -1;
                if (_notesMenuData.NotesMenuItems[i].text != null && btn.text != null)
                {
                    btn.text.text = LanguageManager.Instance.Translate(_notesMenuData.NotesMenuItems[i].text);
                }
            }
        }
    }

    public void UpdateNotesEntryContent(int categoryIndex)
    {
        //On Notes Category Selection Update the Entry Buttons
        ScrollRect entScrollRect = notesEntryButtons.GetComponent<ScrollRect>();
        ClearChildren(entScrollRect.content.transform);
        if (entScrollRect != null)
        {
            NotesMenuItem menuItem = _notesMenuData.NotesMenuItems[categoryIndex];
            if (menuItem == null)
                return;
            for (int i = 0; i < menuItem.entryButtons.Count; i++)
            {
                GameObject codexTopicButton = Instantiate(notesEntryBtn, entScrollRect.content);
                UIButtonNotes btn = codexTopicButton.GetComponentInChildren<UIButtonNotes>();
                btn.OnDeselect(null);
                //An entry button has both the category and entry indexes
                btn.categoryIndex = categoryIndex;
                btn.entryIndex = i;
                if (menuItem.entryButtons[i].text != null && btn.text != null)
                {
                    btn.text.text = LanguageManager.Instance.Translate(menuItem.entryButtons[i].text);
                }
            }
        }
    }
    private void InitNotesEntries()
    {
        UpdateNotesEntryContent(0);
    }
    #endregion
    
    void Start()
    {
        InitUI();
    }
    
    private void InitUI()
    {
        ActivateCodexView();
    }
    private void InitCodexView()
    {
        _codexMenuData = DataManager.Instance.CodexMenu;
        InitCodexCategories();
        InitCodexTopics();
        InitCodexEntries();
    }
    private void InitNotesView()
    {
        _notesMenuData = DataManager.Instance.NotesMenu;
        InitNotesCategories();
        InitNotesEntries();
    }

    public void ActivateCodexView()
    {
        codexView.SetActive(true);
        notesView.SetActive(false);
        InitCodexView();
    }
    public void ActivateNotesView()
    {
        codexView.SetActive(false);
        notesView.SetActive(true);
        InitNotesView();
    }

    #region Utility
    public void ClearChildren(Transform t) {
        for (int i = t.childCount-1; i >= 0; i--) {
            Transform child = t.GetChild(i);
            DestroyImmediate(child.gameObject);
        }
    } 
    #endregion
    
}
