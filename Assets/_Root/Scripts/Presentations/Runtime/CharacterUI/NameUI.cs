using _Root.Scripts.Datas.Runtime.Interfaces;
using TMPro;

namespace _Root.Scripts.Presentations.Runtime.CharacterUI
{
    public class NameUI : StatusUI
    {
        public TextMeshPro textMeshPro;

        private void OnEnable()
        {
            textMeshPro.text = GetComponentInParent<INameStr>().NameStr;
        }
        
        private void Reset()
        {
            textMeshPro = GetComponent<TextMeshPro>();
        }
    }
}