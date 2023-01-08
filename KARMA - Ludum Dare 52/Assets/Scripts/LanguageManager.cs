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
        // Mettre � jour les dialogues et les textes de l'interface utilisateur en fonction de la langue s�lectionn�e
        if (language == "francais")
        {
            // Mettre � jour les dialogues en fran�ais
            // Mettre � jour les textes de l'interface utilisateur en fran�ais
        }
        else
        {
            // Mettre � jour les dialogues en anglais
            // Mettre � jour les textes de l'interface utilisateur en anglais
        }
    }
}



