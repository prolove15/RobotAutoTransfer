Copiright 2018 www.MaxRomagnoli.com

INFOS
With this package you can have a dinamic multi-language Text UI component in your game.
You can switch all the Texts langauges with a single button, and the current language is saved in PlayerPrefs.

SINGLE MULTI-LANGUAGE-TEXT SET-UP
If you wanna use a multiLanguageText, simply replace the Text component you create with the UI/Text with the MultiLanguageText.cs,
write the English default traduction (you can verify the current traduction with the first line of the component),
and add all you desire at the bottom of the component with the "Add" enum.
You can edit the current traduction with the same text field, and remove the other traductions.
When the scene start, or the MultiLanguageText is enabled, the component automatic update in the currenSelected (default is your device language)
You can change the current selected language in the PlayerPrefs, setting the int "Language". All you have to do is to cast a SystemLanguage as int (ex PlayerPrefs.SetInt("Language", <int>SystemLanguage.Italian))

MENU SET-UP (or just open the sample scene)
1. Add an empty object called Menu Manager with the component MenuManager.cs
2. In Hierarchy create a button called "Button Open Menu" and two pannels called "Panel Main Menu" and "Panel Options"
3. Create the Buttons Play, Options and Exit in the Main Menu Panel, and the buttons Change Language and Back in the Options Panel.
4. In the Menu Manager select the languages you wanna use, and assign the Button and Panels created in step 2.
5. Replace all the Texts under the Button Components (and all you wants) with the MultiLanguageText.cs component.
6. Foreach MultiLanguageText you create, write the default traduction (the Default is English), and add other traductions selecting it with the "Add" enum.
	All the Traductions you create need to be edit as a normal Text, in the text field, when the current traduction is selected.
	If you need to edit some traduction you create previously, just select it in the Traducions field.
7. In the "Button Open Menu" assign under OnClick an event referenced to MenuManager.OpenMainMenu.
8. In the "Main Menu Panel" assign to:
	- The "Play Button" an OnClick event, referenced to MenuManager.LoadScene(*with the name of the scene of your level*)
	- The "Options Button" an OnClick event, referenced to MenuManager.OpenOptions.
	- The "Exit Button" an OnClick event, referenced to MenuManager.CloseAllPanels.
9. In the Options Panel" assign to:
	- The "Change Language Button" an OnClick event, referenced to MenuManager.ChangeLanguage (this switch all the languages selected in MenuManager.cs)
	- The "Back Button" an OnClick event, referenced to MenuManager.OpenMainMenu.

NOTE: if you dont wanna use the MenuManager.cs, or you wanna create your own, you can simply copy the "ChangeLanguage" void.

EDIT A MULTILANGUAGETEXT.CS WITH SCRIPT
If you wanna edit a multilanguage Text with a script, you can do it like that:
Example: you have a multilanguagetext for a score, with this traductions:
	English: "Score: "
	Italian: "Punteggio: "
in you script you can do a think like that:

	public MultiLanguageText multiLanguageText;

	public void AssingScore(int _score)
	{
		multiLanguageText.text = multiLanguageText.currentTraduction.text + _score.ToString();
	}

CONTACT
I hope i was clear, for any other information contact me at info@MaxRomagnoli.com