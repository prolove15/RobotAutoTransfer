using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [Header("UI")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject openMenuButton;

    [Header("Languages")]
    [SerializeField] private SystemLanguage[] languages;
    private int currentLanguage = 0;

	void Start ()
    {
        CloseAllPanels();
        FindCurrentLanguageIndex();
    }

    void FindCurrentLanguageIndex()
    {
        for(int i = 0; i < languages.Length; i++)
        {
            if(languages[i] == MultiLanguageManager.GetCurrentLanguage())
            {
                currentLanguage = i;
                return;
            }
        }
    }

    public void CloseAllPanels()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        openMenuButton.SetActive(true);
    }

    public void OpenMainMenu()
    {
        optionsPanel.SetActive(false);
        openMenuButton.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        openMenuButton.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void ChangeLanguage()
    {
        if(languages.Length == 0) { Debug.LogWarning("You have to set some languages in MenuManager before switching them."); return; }
        currentLanguage = (currentLanguage + 1) % languages.Length;
        MultiLanguageManager.SetLanguage(languages[currentLanguage]);
    }
	
	public void LoadScene (string _name)
    {
        SceneManager.LoadScene(_name);
	}
}
