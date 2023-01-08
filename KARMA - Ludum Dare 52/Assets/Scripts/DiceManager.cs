using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    // D�clarez six sprites publiques
    public Sprite face1;
    public Sprite face2;
    public Sprite face3;
    public Sprite face4;
    public Sprite face5;
    public Sprite face6;

    // R�f�rence � l'image du d�
    private Image diceImage;

    private Animator m_Animator;



    void Start()
    {
        m_Animator = this.GetComponent<Animator>();
        // R�cup�rez une r�f�rence � l'image du d�
        diceImage = GetComponent<Image>();
    }

    // Fonction pour changer le sprite de l'image en fonction du r�sultat du d�
    public void ChangeSprite(int result)
    {
        switch (result)
        {
            case 1:
                diceImage.sprite = face1;
                break;
            case 2:
                diceImage.sprite = face2;
                break;
            case 3:
                diceImage.sprite = face3;
                break;
            case 4:
                diceImage.sprite = face4;
                break;
            case 5:
                diceImage.sprite = face5;
                break;
            case 6:
                diceImage.sprite = face6;
                break;
            default:
                Debug.LogError("Invalid dice result");
                break;
        }
    }

    // Fonction pour mettre la couleur de l'image en rouge
    public void SetRedColor()
    {
        diceImage.color = new Color32(176, 44, 40, 255);
    }

    // Fonction pour mettre la couleur de l'image en vert
    public void SetGreenColor()
    {
        diceImage.color = new Color32(118, 176, 40, 255);
    }
    public void MoveUp()
    {
        m_Animator.SetTrigger("moveTrigger");
    }



}
