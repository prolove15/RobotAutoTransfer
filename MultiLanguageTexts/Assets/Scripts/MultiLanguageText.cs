using System.Collections.Generic;

namespace UnityEngine.UI
{    

    [AddComponentMenu("UI/MultiLanguageText", 10)]
    public class MultiLanguageText : Text
    {

        public Traduction currentTraduction = new Traduction();
        public List<Traduction> otherTraductions = new List<Traduction>();

        protected override void OnEnable()
        {
            base.OnEnable();
            UpdateLanguage(MultiLanguageManager.GetCurrentLanguage());
        }

        public Traduction AddTraduction(SystemLanguage _language)
        {
            Traduction _traduction = new Traduction(_language);
            otherTraductions.Add(_traduction);
            return _traduction;
        }

        public void RemoveTraduction(SystemLanguage _langauge)
        {
            otherTraductions.Remove(otherTraductions.Find(x => x.language == _langauge));
        }

        public void RemoveTraduction(Traduction _traduction)
        {
            otherTraductions.Remove(_traduction);
        }

        public void UpdateLanguage(Traduction _traduction)
        {
            otherTraductions.Add(currentTraduction);
            currentTraduction = _traduction;
            otherTraductions.Remove(_traduction);
            text = currentTraduction.text;
        }

        public void UpdateLanguage(SystemLanguage _langauge)
        {
            if(_langauge == currentTraduction.language) { return; }
            foreach (Traduction _traduction in otherTraductions)
            {
                if (_langauge == _traduction.language)
                {
                    UpdateLanguage(_traduction);
                    return;
                }
            }            
            Debug.LogWarning("Language not present");
            text = "";
        }
    }
}