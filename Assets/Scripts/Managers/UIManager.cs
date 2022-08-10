using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    private Dictionary<string, CanvasGroup> UIDictionary;
    public Canvas UI;
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
        UIDictionary = new Dictionary<string, CanvasGroup>();

        CanvasGroup[] allChildren = UI.GetComponentsInChildren<CanvasGroup>();

        foreach (CanvasGroup child in allChildren)
        {
            UIDictionary.Add(child.gameObject.name, child);
        }
    }

    public void Show(string Name)
    {
        if(!ContainsKey(Name))
            return;

        UIDictionary[Name].alpha = 1;
        UIDictionary[Name].interactable = true;
        UIDictionary[Name].blocksRaycasts = true;
    }

    public void HideAll()
    {
        foreach (var item in UIDictionary)
        {
            item.Value.alpha = 0;
            item.Value.interactable = false;
            item.Value.blocksRaycasts = false;
        }
    }

    public void Hide(string Name)
    {
        if(!ContainsKey(Name))
            return;

        UIDictionary[Name].alpha = 0;
        UIDictionary[Name].interactable = false;
        UIDictionary[Name].blocksRaycasts = false;
    }

    public void SetColor(string GroupName, string ElementName, Color32 color)
    {
        if (!ContainsKey(GroupName))
            return;

        Image[] images = UIDictionary[GroupName].GetComponentsInChildren<Image>();

        foreach (var image in images)
        {
            if (image.gameObject.name == ElementName)
            {
                image.GetComponent<Image>().color = color;
                return;
            }
        }
    }

    public void SetText(string GroupName, string ElementName, string Text)
    {
        if (!ContainsKey(GroupName))
            return;

        Text[] texts = UIDictionary[GroupName].GetComponentsInChildren<Text>();

        foreach (var text in texts)
        {
            if (text.gameObject.name == ElementName)
            {
                text.text = Text;
                return;
            }
        }
    }

    private bool ContainsKey(string GroupName)
    {
        if (!UIDictionary.ContainsKey(GroupName))
        {
            Debug.Log("UI with tag " + GroupName + " doesn't excist.");
            return false;
        }
        return true;
    }
}