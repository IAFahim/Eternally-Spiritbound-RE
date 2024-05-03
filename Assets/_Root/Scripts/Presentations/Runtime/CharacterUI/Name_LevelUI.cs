using _Root.Scripts.Datas.Runtime.Interfaces;
using _Root.Scripts.Datas.Runtime.Statistics;
using _Root.Scripts.Utilities;
using TMPro;
using static System.String;

namespace _Root.Scripts.Presentations.Runtime.CharacterUI
{
    public class NameLevelUI : StatusUI
    {
        public TextMeshPro textMeshPro;
        private string format = "{0} <alpha=#{2}>{1}";
        private string knownName;
        public bool isTeamMember;

        private void OnEnable()
        {
            format = textMeshPro.text;
            var iName = GetComponentInParent<IName>();
            knownName = iName.Title;
            var iLevel = GetComponentInParent<ILevel>();
            iLevel.Level.OnValueChanged += OnLevelChanged;
            OnLevelChanged(iLevel.Level);
        }

        private void OnLevelChanged(float levelProgress)
        {
            var (level, alpha) = GetLevelProgress(levelProgress);
            textMeshPro.text = Format(format, IsTeamMemberName(), level, alpha.ToString("X"));
        }

        private string IsTeamMemberName()
        {
            return isTeamMember ? "<u>" + knownName + "</u>" : knownName;
        }

        private (int level, int alpha) GetLevelProgress(float levelProgress)
        {
            int level = (int)levelProgress;
            int progress = (int)((levelProgress - level) * 100);
            int alpha = (int)MathU.Remap(progress, 0, 100, 50, 100);
            return (level, alpha);
        }

        private void Reset()
        {
            textMeshPro = GetComponent<TextMeshPro>();
        }
    }
}