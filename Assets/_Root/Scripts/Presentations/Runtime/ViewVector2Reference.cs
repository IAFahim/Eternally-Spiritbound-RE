using _Root.Scripts.Datas.Runtime.Statistics;
using _Root.Scripts.Datas.Runtime.Variables;
using TMPro;
using UnityEngine;

namespace _Root.Scripts.Presentations
{
    [DefaultExecutionOrder(999)]
    public class ViewVector2Reference : MonoBehaviour
    {
        public MonoBehaviour script;
        public Vector2ReferenceR reference;
        public TextMeshProUGUI title;
        public TextMeshProUGUI value;

        private string _valueFormat = "{0}/{1}";

        private void OnValidate()
        {
            if (title == null || value == null)
            {
                TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
                title = texts[0];
                value = texts[1];
            }
        }

        private void OnEnable()
        {
            reference = script.GetComponent<IOnlyOneVector2ReferenceR>().ReferenceR;
            reference.OnValueChanged += OnValueChanged;
            if (reference.variable) title.text = reference.variable.name;
            _valueFormat = value.text;
            OnValueChanged(reference.Value);
        }

        private void OnDisable()
        {
            reference.OnValueChanged -= OnValueChanged;
        }

        private void OnValueChanged(Vector2 obj)
        {
            value.SetText(_valueFormat, obj.x, obj.y);
            Debug.Log($"Value changed to {obj}");
        }
    }
}