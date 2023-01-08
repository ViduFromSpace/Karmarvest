using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class YinYang : MonoBehaviour
{
    public Sprite YinSolo;
    public Sprite YinYangClassic;
    public Sprite YinYangActivated;

    public int jokerLevel;

    private Image YinYangImage;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        jokerLevel = 0;
        YinYangImage = GetComponent<Image>();
    }

    void Update()
    {
        if (jokerLevel == 2)
        {
            Button button = GetComponent<Button>();
            if (button != null)
            {
                button.interactable = true;
            }
            EventTrigger trigger = GetComponent<EventTrigger>();
            if (trigger != null)
            {
                trigger.enabled = true;
            }
        }
        else
        {
            Button button = GetComponent<Button>();
            if (button != null)
            {
                button.interactable = false;
            }
            EventTrigger trigger = GetComponent<EventTrigger>();
            if (trigger != null)
            {
                trigger.enabled = false;
            }
        }
    }

    public void AddOneJokerLevel()
    {
        jokerLevel++;
        switch(jokerLevel)
        {
            case 0:
                YinYangImage.sprite = YinSolo;
                break;
            case 1:
                this.gameObject.SetActive(true);
                YinYangImage.sprite = YinSolo;
                break;
            case 2:
                this.gameObject.SetActive(true);
                YinYangImage.sprite = YinYangClassic;
                break;
            case 3:
                jokerLevel = 2;
                break;
        }
    }

    public void MakeItShine()
    {
        YinYangImage.sprite = YinYangActivated;
    }

    public void MakeItStopShining()
    {
        YinYangImage.sprite = YinYangClassic;
    }

    public void UseIt()
    {
        jokerLevel = 0;
        YinYangImage.sprite = YinSolo;
        this.gameObject.SetActive(false);
    }
}
