﻿using System.Collections;
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
    [Header("Note Elements")] 
    public GameObject notesCategoryButtons;

    private CodexMenuData _codexMenuData;
    
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
                UI_Button btn = codexTopicButton.GetComponent<UI_Button>();
                btn.OnSelect(null);
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
                UI_Button btn = codexEntryButton.GetComponent<UI_Button>();
                btn.OnSelect(null);
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
    

    public void InitUI()
    {
        //CODEX
        _codexMenuData = DataManager.Instance.CodexMenu;
        InitCodexCategories();
        InitCodexTopics();
        InitCodexEntries();
        
        //TODO: NOTES
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

    private void InitCodexCategories()
    {
        ScrollRect catScrollRect = codexCategoryButtons.GetComponent<ScrollRect>();
        ClearChildren(catScrollRect.content.transform);
        if (catScrollRect != null)
        {
            for (int i = 0; i < _codexMenuData.CodexMenuItems.Count; i++)
            {
                GameObject codexMenuBtn = Instantiate(codexCategoryBtn, catScrollRect.content);
                UI_Button btn = codexMenuBtn.GetComponent<UI_Button>();
                btn.OnSelect(null);
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
    
    

    void Start()
    {
        InitUI();
    }
    
    //Utility
    public void ClearChildren(Transform t) {
        for (int i = t.childCount-1; i >= 0; i--) {
            Transform child = t.GetChild(i);
            DestroyImmediate(child.gameObject);
        }
    } 
    
}
