using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Dialogue;
using TMPro;

public class TooltipHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text tooltipText;
    public GameObject tooltipGameObject;
    public string FrancaisToolTip;
    public string EnglishToolTip;
    public DialogueManager dialogueManager;
    public GameObject ecranAllumeSmartphone;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (dialogueManager.language == DialogueManager.Language.French)
        {
            tooltipText.text = FrancaisToolTip;
        }
        else if (dialogueManager.language == DialogueManager.Language.English)
        {
            tooltipText.text = EnglishToolTip;
        }
        tooltipGameObject.SetActive(true);
        ecranAllumeSmartphone.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipGameObject.SetActive(false);
        ecranAllumeSmartphone.SetActive(false);
    }
}
