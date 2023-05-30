using UnityEngine;

[System.Serializable]
public class Traduction
{
    public SystemLanguage language;
    [TextArea] public string text;

    public Traduction()
    {
        language = SystemLanguage.English;
        text = "";
    }

    public Traduction(SystemLanguage _language)
    {
        language = _language;
        text = "";
    }

}