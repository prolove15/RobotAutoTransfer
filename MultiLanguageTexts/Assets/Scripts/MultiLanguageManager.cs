using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MultiLanguageManager {

    private static SystemLanguage currentLanguage = (SystemLanguage)PlayerPrefs.GetInt("Language", (int)Application.systemLanguage);

    public static void SetLanguage(SystemLanguage _language)
    {
        currentLanguage = _language;
        PlayerPrefs.SetInt("Language", (int)_language);
        UpdateAllLanguages(currentLanguage);
    }

    public static SystemLanguage GetCurrentLanguage ()
    {
        return currentLanguage;
	}

    public static void UpdateAllLanguages()
    {
        MultiLanguageText[] allMultiLanguagesTexts = GameObject.FindObjectsOfType<MultiLanguageText>();
        foreach(MultiLanguageText _multiLanguage in allMultiLanguagesTexts)
        {
            _multiLanguage.UpdateLanguage(currentLanguage);
        }
    }

    public static void UpdateAllLanguages(SystemLanguage _language)
    {
        currentLanguage = _language;
        MultiLanguageText[] allMultiLanguagesTexts = GameObject.FindObjectsOfType<MultiLanguageText>();
        foreach (MultiLanguageText _multiLanguage in allMultiLanguagesTexts)
        {
            _multiLanguage.UpdateLanguage(_language);
        }
    }
}
