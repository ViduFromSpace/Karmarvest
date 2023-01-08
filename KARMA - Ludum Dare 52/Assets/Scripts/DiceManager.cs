using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    // Déclarez six sprites publiques
    public Sprite face1;
    public Sprite face2;
    public Sprite face3;
    public Sprite face4;
    public Sprite face5;
    public Sprite face6;

    // Référence à l'image du dé
    private Image diceImage;

    private Animator m_Animator;



    void Start()
    {
        m_Animator = this.GetComponent<Animator>();
        // Récupérez une référence à l'image du dé
        diceImage = GetComponent<Image>();
    }

    // Fonction pour changer le sprite de l'image en fonction du résultat du dé
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
