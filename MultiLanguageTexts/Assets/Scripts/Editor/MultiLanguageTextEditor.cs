using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
//using System.Collections;

[CustomEditor(typeof(MultiLanguageText))]
public class MultiLanguageTextEditor : UnityEditor.UI.TextEditor
{
	public override void OnInspectorGUI ()
    {
        MultiLanguageText multiLanguageText = (MultiLanguageText)target;

        //SHOW THE CURRENT TRADUCTION IN EDITOR//
        GUILayout.Label("Current traduction: " + multiLanguageText.currentTraduction.language.ToString(), EditorStyles.boldLabel);
        base.OnInspectorGUI();

        //ASSIGN THE EDITOR TEXT TO THE OBJECT//
        multiLanguageText.currentTraduction.text = multiLanguageText.text;

        //SHOW OTHER TRADUCTIONS//
        GUILayout.Label("Traductions", EditorStyles.boldLabel);
        foreach(Traduction _traduction in multiLanguageText.otherTraductions)
        {
            GUILayout.BeginHorizontal();
            if(GUILayout.Button("Select " + _traduction.language.ToString()))
            {
                multiLanguageText.UpdateLanguage(_traduction);
                Repaint();
                return;
            }
            else if(GUILayout.Button("Remove " + _traduction.language.ToString()))
            {
                multiLanguageText.RemoveTraduction(_traduction);
                return;
            }
            GUILayout.EndHorizontal();
        }

        //ADD LANGUAGE//
        SystemLanguage newLanguage = multiLanguageText.currentTraduction.language;
        newLanguage = (SystemLanguage)EditorGUILayout.EnumPopup("Add", newLanguage);
        if(newLanguage != multiLanguageText.currentTraduction.language)
        {
            Traduction newTraduction = multiLanguageText.AddTraduction(newLanguage);
            multiLanguageText.UpdateLanguage(newTraduction);
            Repaint();
            return;
        }        
    }

}
