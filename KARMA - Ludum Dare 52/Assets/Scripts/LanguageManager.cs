using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public enum Language
    {
        French,
        English
    }
    public Language language;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void ChangeLanguage()
    {
        switch(language)
        {
            case Language.French:
                language = Language.English;
                break;
            case Language.English:
                language = Language.French;
                break;
        }
    }

    public void PutLanguage(string languageTemp)
    {
        switch (languageTemp)
        {
            case "English":
                language = Language.English;
                break;
            case "French":
                language = Language.French;
                break;
        }
    }
    public static void UpdateLanguage(string language)
    {
        // Mettre à jour les dialogues et les textes de l'interface utilisateur en fonction de la langue sélectionnée
        if (language == "francais")
        {
            // Mettre à jour les dialogues en français
            // Mettre à jour les textes de l'interface utilisateur en français
        }
        else
        {
            // Mettre à jour les dialogues en anglais
            // Mettre à jour les textes de l'interface utilisateur en anglais
        }
    }
}



