using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Dialogue;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public Dialogue currentDialogue;
    private ArcNarratif currentArcNarratif;

    public int currentDialogueIndex;
    public int currentLine;

    public TMP_Text dialogueText;
    public TMP_Text dateText;
    public TMP_Text rollDiceButtonText;
    public TMP_Text negativeOptionButtonText;
    public TMP_Text resultDiceText;
    public TMP_Text conclusionButtonText;

    public DiceManager diceManager;

    public GameObject resultGroup;
    public GameObject textFieldName;
    public TMP_Text inputFieldText;
    public TMP_Text placeHolderTextFieldName;

    public GameObject conclusionButton;

    public Button rollDiceButton;
    public Button negativeOptionButton;
    int diceResult;

    public Image polaroid;

    public bool AllowClick;
    public bool KarmaGood;

    public string playerName;

    public GameObject yinYangGO;
    private YinYang yinYang;

    public ArcNarratif[] arcNarratifs;
    public int currentArcNarratifIndex;

    public int malusKarma;

    public bool passionPasteque;
    public bool peurEcureuils;
    public bool animalTotemEcureuil;
    public bool suicuneAttrape;
    public bool enCoupleAvecLila;
    public bool seMarieAvecLila;
    public bool seMarieAvecEmilie;

    public GameObject audioScriptGO;

    //Ajout de la variable pour choisir la langue
    public enum Language
    {
        French,
        English
    }
    public Language language;

    public GameObject languageManager;

    // ____________________________________________________________________________________________________________________________



    private void Start()
    {
        yinYang = yinYangGO.GetComponent<YinYang>();
        languageManager = GameObject.FindWithTag("LanguageManager");
        audioScriptGO = GameObject.FindWithTag("Audio");
        AllowClick = true;

        // Vérifier si l'objet LanguageManager est présent dans la hiérarchie de la scène
        if (languageManager != null)
        {
            // Appliquer la méthode AssignLanguage si l'objet LanguageManager est présent
            AssignLanguage();
        }
        else
        {
            // Mettre la langue en français par défaut si l'objet LanguageManager n'est pas présent
            language = Language.French;
        }

        passionPasteque = false;
        peurEcureuils = false;
        animalTotemEcureuil = false;
        suicuneAttrape = false;

        malusKarma = 0;
        currentArcNarratifIndex = 0;
        currentArcNarratif = arcNarratifs[currentArcNarratifIndex];
        currentDialogue = currentArcNarratif.dialogues[currentDialogueIndex];
        ShowDialogue();


        // Demande au joueur de saisir son nom
        playerName = PlayerPrefs.GetString("PlayerName", "");
        if (playerName == "")
        {
            playerName = UnityEngine.Input.inputString;
            PlayerPrefs.SetString("PlayerName", playerName);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && AllowClick)
        {

            currentLine++;
            ShowDialogue();

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Se marie avec lila bool : " + currentDialogue.seMarieAvecLilaSuccess);
            Debug.Log("Se marie avec émilie bool : " + currentDialogue.seMarieAvecEmilieSuccess);
        }


    }

    public void ShowDialogue()
    {
        // Si on arrive au bout et que c'est une version où on doit écrire son nom
        if (currentLine >= currentDialogue.linesFR.Length && currentDialogue.ecrireSonNom)
        {
            if (language == Language.French)
            {
                dialogueText.text = currentDialogue.endChapterQuestionFR;
            }
            else if (language == Language.English)
            {
                dialogueText.text = currentDialogue.endChapterQuestionEN;
            }
            ShowTextFieldName();
            return;
        }
        // Si on arrive au bout et que c'est une version avec un choix à faire
        else if (currentLine >= currentDialogue.linesFR.Length && currentDialogue.histoireAChoix)
        {
            Debug.Log("C'est une histoire à choix");
            if (language == Language.French)
            {
                dialogueText.text = currentDialogue.endChapterQuestionFR;
            }
            else if (language == Language.English)
            {
                dialogueText.text = currentDialogue.endChapterQuestionEN;
            }
            ClickAvaible(false);
            SetUpChoiceButtons();
            MakeYinYangShiny();
            ShowChoiceButtons();
            return;
        }
        // Si on arrive au bout et que c'est une version sans choix à faire
        else if (currentLine >= currentDialogue.linesFR.Length && !currentDialogue.histoireAChoix)
        {
            NextDialogue();
        }

        if (language == Language.French)
        {
            dialogueText.text = ReplacePlayerName(currentDialogue.linesFR[currentLine]);
            dateText.text = currentDialogue.dateFR;
        }
        else if (language == Language.English)
        {
            dialogueText.text = ReplacePlayerName(currentDialogue.linesEN[currentLine]);
            dateText.text = currentDialogue.dateEN;
        }
        
    }

    public void MakeYinYangShiny()
    {  
        if (yinYang.jokerLevel == 2)
        {
            yinYang.MakeItShine();
        }
    }

    public void CalmDownYinYang()
    {
        if (yinYang.jokerLevel == 2)
        {
            yinYang.MakeItStopShining();
        }
        
    }

    public void NextDialogue()
    {
        
        // On remet la ligne de dialogue à 0
        currentLine = 0;

        // On passe au Scriptable "Dialogue" suivant
        currentDialogueIndex++;

        // Si on a fini un arc narratif, on passe au suivant
        if (currentDialogueIndex >= currentArcNarratif.dialogues.Length)
        {
            currentArcNarratif = GetNextArcNarratif();
        }

        // On Set Up le Dialogue actuel en lui disant de lire le prochain "Dialogue" de l'arc Narratif
        currentDialogue = currentArcNarratif.dialogues[currentDialogueIndex];

        CheckConditions();

        // On affiche le dialogue
        ShowDialogue();
    }

    public void RollDice()
    {
        diceResult = Random.Range(1, 7);
        ShowResultDice();
    }

    public void CheckConditions()
    {
        // Si la case "NeedCheckCondition" est cochée, alors on vérifie les conditions et si elles ne sont pas remplies, on passe au dialogue suivant
        if (currentDialogue.needCheckCondition)
        {
            Debug.Log("Un check de condition s'applique");
            if (currentDialogue.passionPasteque)
            {
                if (!passionPasteque)
                {
                    PasserAuProchainDialogue();
                }
            }
            if (currentDialogue.noPassionPasteque)
            {
                if (passionPasteque)
                {
                    PasserAuProchainDialogue();
                }
            }
            if (currentDialogue.seMarieAvecLila)
            {
                Debug.Log("Se marie avec Lila checked");
                if(!seMarieAvecLila)
                {
                    PasserAuProchainDialogue();
                }
            }
            if (currentDialogue.seMarieAvecEmilie)
            {
                if (!seMarieAvecEmilie)
                {
                    PasserAuProchainDialogue();
                }
            }
            if (currentDialogue.enCoupleAvecLila)
            {
                if (!enCoupleAvecLila)
                {
                    PasserAuProchainDialogue();
                }
            }
            if (currentDialogue.celibataire)
            {
                if (enCoupleAvecLila)
                {
                    PasserAuProchainDialogue();
                }
            }
            if (!currentDialogue.enCoupleAvecLila)
            {
                if (enCoupleAvecLila)
                {
                    PasserAuProchainDialogue();
                }
            }
            if (currentDialogue.peurEcureuils)
            {
                if (!peurEcureuils)
                {
                    PasserAuProchainDialogue();
                }
            }
            if (currentDialogue.animalTotemEcureuil)
            {
                if (!animalTotemEcureuil)
                {
                    PasserAuProchainDialogue();
                }
            }
            if (currentDialogue.suicuneAttrape)
            {
                if (!suicuneAttrape)
                {
                    PasserAuProchainDialogue();
                }
            }
        }
    }

    public void AppliquerLImpact(bool resultat)
    {
        Debug.Log("Un impact s'applique");
        if (currentDialogue.passionPastequeSuccess && resultat)
        {
            passionPasteque = true;
        }
        if(currentDialogue.passionPastequeFail && !resultat)
        {
            passionPasteque = false;
        }
        if (currentDialogue.enCoupleAvecLilaSuccess && resultat)
        {
            enCoupleAvecLila = true;
        }
        if (currentDialogue.enCoupleAvecLilaFail && !resultat)
        {
            enCoupleAvecLila = false;
        }
        if (currentDialogue.animalTotemEcureuilSuccess && resultat)
        {
            animalTotemEcureuil = true;
        }
        if (currentDialogue.suicuneAttrapeSuccess && resultat)
        {
            suicuneAttrape = true;
        }

        if (currentDialogue.seMarieAvecLilaSuccess && resultat)
        {
            Debug.Log("Se marie avec Lila");
            seMarieAvecLila = true;
            seMarieAvecEmilie = false;
        }

        if (currentDialogue.seMarieAvecLilaSuccess && !resultat)
        {
            Debug.Log("Se marie avec Emilie");
            seMarieAvecLila = false;
            seMarieAvecEmilie = true;
        }
        if (currentDialogue.peurEcureuilsFail && !resultat)
        {
            peurEcureuils = true;
        }
    }
    public void PasserAuProchainDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex >= currentArcNarratif.dialogues.Length)
        {
            currentArcNarratif = GetNextArcNarratif();
        }
        currentDialogue = currentArcNarratif.dialogues[currentDialogueIndex];
        CheckConditions();
    }
    public void ShowNegativeOption()
    {
        KarmaGood = false;
        if (language == Language.French)
        {
            dialogueText.text = currentDialogue.negativeOptionFR;
        }
        else if (language == Language.English)
        {
            dialogueText.text = currentDialogue.negativeOptionEN;
        }

        AppliquerLImpact(KarmaGood);
        ShowConclusionButton();
        HideChoiceButtons();
        HideResultDice();
    }

    public void ReussiteYinYang()
    {
        KarmaGood = true;
        malusKarma = 0;
        diceResult = 6;
        if (language == Language.French)
        {
            dialogueText.SetText(currentDialogue.positiveOptionFR);
        }
        else if (language == Language.English)
        {
            dialogueText.SetText(currentDialogue.positiveOptionEN);
        }
        if (currentDialogue.aUnImpact)
        {
            AppliquerLImpact(true);
        }
        ShowConclusionButton();
        HideChoiceButtons();
        TriggerDice(diceResult, KarmaGood);

        yinYang.UseIt();

    }
    public void TryTheDestiny(int malus)
    {
        RollDice();

        int diceResultWithMalus = diceResult - malus;

        if (diceResultWithMalus >= currentDialogue.diceValue + malusKarma)
        {
            KarmaGood = true;
            malusKarma++;
            if (language == Language.French)
            {
                dialogueText.SetText(currentDialogue.positiveOptionFR);
            }
            else if (language == Language.English)
            {
                dialogueText.SetText(currentDialogue.positiveOptionEN);
            }
            if (currentDialogue.aUnImpact)
            {
                AppliquerLImpact(true);
            }

        }
        else
        {
            KarmaGood = false;
            malusKarma = 0;
            if (language == Language.French)
            {
                dialogueText.SetText(currentDialogue.negativeOptionFR);
            }
            else if (language == Language.English)
            {
                dialogueText.SetText(currentDialogue.negativeOptionEN);
            }
            if (currentDialogue.aUnImpact)
            {
                AppliquerLImpact(false);
            }

        }
        ShowConclusionButton();
        HideChoiceButtons();
        TriggerDice(diceResult, KarmaGood);
    }

    void SetUpChoiceButtons()
    {
        if (language == Language.French)
        {
            rollDiceButtonText.text = "Roue du destin (" + (currentDialogue.diceValue + malusKarma) + "+)";
            negativeOptionButtonText.text = "Le tout pour le tout (" + (currentDialogue.diceValue + malusKarma + 1) + "+)";
        }
        else if (language == Language.English)
        {
            rollDiceButtonText.text = "Wheel of fate (" + (currentDialogue.diceValue + malusKarma) + "+)";
            negativeOptionButtonText.text = "All or nothing" + (currentDialogue.diceValue + malusKarma + 1) + "+)";
        }

    }

    void HideChoiceButtons()
    {
        rollDiceButton.gameObject.SetActive(false);
        negativeOptionButton.gameObject.SetActive(false);
    }

    void ShowChoiceButtons()
    {
        rollDiceButton.gameObject.SetActive(true);
        negativeOptionButton.gameObject.SetActive(true);
    }

    void ShowResultDice()
    {
        resultGroup.gameObject.SetActive(true);
        if (diceResult >= currentDialogue.diceValue)
        {
            resultDiceText.text = diceResult.ToString() + " c'est réussi !";
        }
        else
        {
            resultDiceText.text = diceResult.ToString() + " c'est raté !";
        }
            
    }

    public void HideResultDice()
    {
        resultGroup.gameObject.SetActive(false);
    }

    void ShowTextFieldName()
    {
        if (language == Language.French)
        {
            placeHolderTextFieldName.text = "quel était-il ?";
        }
        else if (language == Language.English)
        {
            placeHolderTextFieldName.text = "how does she call you ?";
        }
        
        textFieldName.gameObject.SetActive(true);
    }

    string ReplacePlayerName(string text)
    {
        return text.Replace("[PlayerName]", playerName);
    }

    public void BoutonValidationNom(string s)
    {
        // Affichage du nom du joueur
        playerName = s;
        textFieldName.gameObject.SetActive(false);
        NextDialogue();
    }

    void ShowConclusionButton()
    {
        CalmDownYinYang();
        conclusionButton.gameObject.SetActive(true);
        AllowClick = false;
        HideResultDice();

        if (language == Language.French && KarmaGood)
        {
            conclusionButtonText.text = currentDialogue.positiveConclusionFR;
        }
        else if (language == Language.English && KarmaGood)
        {
            conclusionButtonText.text = currentDialogue.positiveConclusionEN;
        }
        else if (language == Language.French && !KarmaGood)
        {
            conclusionButtonText.text = currentDialogue.negativeConclusionFR;
        }
        else if (language == Language.English && !KarmaGood)
        {
            conclusionButtonText.text = currentDialogue.negativeConclusionEN;
        }
    }

    public void HideConclusionButton()
    {
        conclusionButton.gameObject.SetActive(false);
    }

    public void ClickAvaible(bool trueOrFalse)
    {
        AllowClick = trueOrFalse;
    }

    public ArcNarratif GetNextArcNarratif()
    {
        int nextArcNarratifIndex = currentArcNarratifIndex + 1;
        currentArcNarratifIndex++;

        // Si on commencer le premier vrai chapitre, lancer la musique.
        if (currentArcNarratifIndex == 1)
        {
            // Vérifier si l'objet LanguageManager est présent dans la hiérarchie de la scène
            if (audioScriptGO != null)
            {
                // Appliquer la méthode AssignLanguage si l'objet LanguageManager est présent
                AudioScript audioScript = audioScriptGO.GetComponent<AudioScript>();
                audioScript.PlayMusic(1);
            }
            else
            {
                // Mettre la langue en français par défaut si l'objet LanguageManager n'est pas présent
                Debug.Log("Il n'y a pas d'audioScript ici");
            }
            
        }
        // Si on commencer le premier vrai chapitre, lancer la musique.
        if (currentArcNarratifIndex == 3)
        {
            // Vérifier si l'objet LanguageManager est présent dans la hiérarchie de la scène
            if (audioScriptGO != null)
            {
                // Appliquer la méthode AssignLanguage si l'objet LanguageManager est présent
                AudioScript audioScript = audioScriptGO.GetComponent<AudioScript>();
                audioScript.PlayMusic(2);
            }
            else
            {
                // Mettre la langue en français par défaut si l'objet LanguageManager n'est pas présent
                Debug.Log("Il n'y a pas d'audioScript ici");
            }

        }
        // Si on commencer le premier vrai chapitre, lancer la musique.
        if (currentArcNarratifIndex == 5)
        {
            // Vérifier si l'objet LanguageManager est présent dans la hiérarchie de la scène
            if (audioScriptGO != null)
            {
                // Appliquer la méthode AssignLanguage si l'objet LanguageManager est présent
                AudioScript audioScript = audioScriptGO.GetComponent<AudioScript>();
                audioScript.PlayMusic(3);
            }
            else
            {
                // Mettre la langue en français par défaut si l'objet LanguageManager n'est pas présent
                Debug.Log("Il n'y a pas d'audioScript ici");
            }

        }

        if (nextArcNarratifIndex >= arcNarratifs.Length)
        {
            // Tu peux mettre ici ce que tu veux faire une fois que tu as fini tous les ArcNarratifs.
            return null;
        }
        currentDialogueIndex = 0;
        return arcNarratifs[nextArcNarratifIndex];
    }

    public void TriggerDice(int result, bool success)
    {
        diceManager.ChangeSprite(result);
        if(success)
        {
            diceManager.SetGreenColor();
        }
        else
        {
            diceManager.SetRedColor();
        }
        diceManager.MoveUp();

    }

    public void AssignLanguage()
    {
        LanguageManager languageManagerScript = languageManager.GetComponent<LanguageManager>();
        switch (languageManagerScript.language)
        {
            case LanguageManager.Language.French:
                language = Language.French;
                break;
            case LanguageManager.Language.English:
                language = Language.English;
                break;
        }
    }

}
