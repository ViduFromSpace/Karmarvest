using UnityEngine;

[CreateAssetMenu(fileName = "Nouveau Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [Header("Date")]
    public string dateFR;
    public string dateEN;

    [Header("Dialogues")]
    public string[] linesFR;
    public string[] linesEN;

    [Header("Question fin chapitre")]
    public string endChapterQuestionFR;
    public string endChapterQuestionEN;
    
    [Header("Réponses")]
    public string negativeOptionFR;
    public string negativeOptionEN;
    public string positiveOptionFR;
    public string positiveOptionEN;

    [Header("Conclusion")]
    public string negativeConclusionFR;
    public string negativeConclusionEN;
    public string positiveConclusionFR;
    public string positiveConclusionEN;
 

    [Header("Habillage")]
    public Emotion emotion;
    public Sprite characterImage;

    [Header("Hasard")]
    public int diceValue;

    [Header("Type d'histoire")]
    public bool histoireAChoix;
    public bool ecrireSonNom;

    [Header("Conditions nécessaires")]
    public bool needCheckCondition;
    public bool passionPasteque;
    public bool noPassionPasteque;
    public bool peurEcureuils;
    public bool animalTotemEcureuil;
    public bool suicuneAttrape;
    public bool enCoupleAvecLila;
    public bool seMarieAvecLila;
    public bool seMarieAvecEmilie;
    public bool celibataire;

    [Header("Si a un impact :")]
    public bool aUnImpact;

    [Header("Si réussi :")]
    public bool passionPastequeSuccess;
    public bool animalTotemEcureuilSuccess;
    public bool suicuneAttrapeSuccess;
    public bool enCoupleAvecLilaSuccess;
    public bool seMarieAvecLilaSuccess;

    [Header("Si échoue :")]
    public bool passionPastequeFail;
    public bool peurEcureuilsFail;
    public bool enCoupleAvecLilaFail;
    public bool seMarieAvecEmilieSuccess;

    public enum Emotion
    {
        Neutre,
        Heureux,
        Triste,
        Colere,
        Surpris,
    }

}
