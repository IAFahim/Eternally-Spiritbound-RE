using System.Globalization;
using _Root.Scripts.Datas.Runtime.Statistics;
using _Root.Scripts.Utilities;
using TMPro;
using UnityEngine;

namespace _Root.Scripts.Presentations.Runtime.CharacterUI
{
    public class HealthUI : StatusUI
    {
        [SerializeField] private TextMeshPro textMeshPro;
        [SerializeField] private MeshRenderer meshRenderer;

        private IHealth health;
        private MaterialPropertyBlock matBlock;
        private static readonly int Fill = Shader.PropertyToID("_Fill");

        private void Awake()
        {
            matBlock = new MaterialPropertyBlock();
        }

        private void OnEnable()
        {
            health = GetComponentInParent<IHealth>();
            health.Health.OnValueChanged += OnHealthChanged;
            OnHealthChanged(health.Health.Value);
        }

        private void OnHealthChanged(Vector2 obj)
        {
            textMeshPro.text = obj.Current().ToString(CultureInfo.InvariantCulture);
            UpdateHealthBarFill(obj);
        }

        private void UpdateHealthBarFill(Vector2 obj)
        {
            meshRenderer.GetPropertyBlock(matBlock);
            matBlock.SetFloat(Fill, obj.GetPercentage());
            meshRenderer.SetPropertyBlock(matBlock);
        }

        private void OnDisable()
        {
            health.Health.OnValueChanged -= OnHealthChanged;
        }

        private void Reset()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            textMeshPro = GetComponentInChildren<TextMeshPro>();
        }
    }
}