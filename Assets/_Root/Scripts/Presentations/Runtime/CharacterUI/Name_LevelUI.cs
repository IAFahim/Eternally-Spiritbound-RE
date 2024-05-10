using _Root.Scripts.Datas.Runtime.Interfaces;
using _Root.Scripts.Datas.Runtime.Statistics;
using PrimeTween;
using TMPro;
using static System.String;

namespace _Root.Scripts.Presentations.Runtime.CharacterUI
{
    public class NameLevelUI : StatusUI
    {
        public TextMeshPro textMeshPro;
        private string format = "{0} {1}";
        private string knownName;
        public bool isTeamMember;
        public int offset = 100;
        private ILevel iLevel;

        public TweenSettings<float> tweenSettings;
        private Tween _tween;
        private int _level, _alpha;
        private float _size;
        private bool _enableCall;

        private void OnEnable()
        {
            _enableCall = true;
            format = textMeshPro.text;
            var iName = GetComponentInParent<IName>();
            knownName = iName.Title;
            _size = textMeshPro.fontSize;
            iLevel = GetComponentInParent<ILevel>();
            iLevel.Level.OnValueChanged += OnLevelChanged;
            OnLevelChanged(iLevel.Level);
            _enableCall = false;
        }

        private void OnDisable()
        {
            iLevel.Level.OnValueChanged -= OnLevelChanged;
        }

        private void OnLevelChanged(float levelProgress)
        {
            _level = (int)iLevel.Level.Value;
            if (_level > 0)
            {
                _alpha = GetLevelAlpha(levelProgress);
                if (_enableCall)
                {
                    UpdateText(textMeshPro, 0);
                    return;
                }

                if (_tween.isAlive) _tween.Complete();
                _tween = Tween.Custom(textMeshPro, tweenSettings, UpdateText);
            }
            else
            {
                textMeshPro.text = IsTeamMemberName();
            }
        }

        private void UpdateText(TextMeshPro target, float progressSize)
        {
            string levelText = $"<size={_size + progressSize + _alpha / 256f}>{IsTeamMemberName()} <alpha=#{_alpha:X}>{_level}</size>";
            target.text = levelText;
        }

        private string IsTeamMemberName()
        {
            return isTeamMember ? "<u>" + knownName + "</u>" : knownName;
        }

        private int GetLevelAlpha(float levelProgress)
        {
            return (int)((levelProgress - (int)levelProgress) * 100) + offset;;
        }

        private void Reset()
        {
            textMeshPro = GetComponent<TextMeshPro>();
        }
    }
}