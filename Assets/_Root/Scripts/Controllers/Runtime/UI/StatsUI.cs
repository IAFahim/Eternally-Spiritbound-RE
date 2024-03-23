// using _Root.Scripts.Controllers.Runtime.Characters;
// using _Root.Scripts.Controllers.Runtime.Main;
// using _Root.Scripts.Datas.Runtime.Characters;
// using _Root.Scripts.Datas.Runtime.Statistics;
// using Pancake;
// using Pancake.Scriptable;
// using UnityEngine;
// using UnityEngine.Serialization;
// using UnityProgressBar;
//
// namespace _Root.Scripts.Controllers.Runtime.UI
// {
//     public class StatsUI : GameComponent
//     {
//         public MainCharacterSelect mainCharacterSelect;
//
//         [Header("Health")] [SerializeField] private ProgressBar healthBar;
//         [SerializeField] private Vector2 healthBarRange = new(HealthBase.DefaultStaring, 400);
//         [SerializeField] private RectTransform healthBarRect;
//
//         [Header("Mana")] [SerializeField] private ProgressBar manaBar;
//         [SerializeField] private Vector2 manaBarRange = new(40, 100);
//
//
//         private void OnValidate()
//         {
//             healthBarRect ??= healthBar.GetComponent<RectTransform>();
//         }
//
//         private void OnEnable()
//         {
//             mainCharacterSelect.OnRaised += OnCharacterUpdate;
//             if (mainCharacterSelect.main != null) OnCharacterUpdate(mainCharacterSelect.main);
//         }
//
//         private void OnDisable()
//         {
//             mainCharacterSelect.OnRaised -= OnCharacterUpdate;
//             RemoveOldListeners();
//         }
//
//         private void OnCharacterUpdate(CharacterData mainCharacterData)
//         {
//             RemoveOldListeners();
//             AddNewListeners(mainCharacterData);
//         }
//
//         private void AddNewListeners(CharacterData mainCharacterData)
//         {
//             var stats = mainCharacterData.stats;
//             AddListenerForHealth(mainCharacterData.healthVariable);
//             AddListenerForMana(stats.manaCurrent, stats.manaMax);
//         }
//
//         private void AddListenerForHealth(HealthBase healthBase)
//         {
//             healthBase.OnMaxChanged += HealthBarWidthUpdate;
//             healthBase.OnCurrentChanged += HealthBarFillUpdate;
//
//             HealthBarWidthUpdate(healthBase.Max);
//             HealthBarFillUpdate(healthBase.Current);
//         }
//
//         private void AddListenerForMana(FloatReference manaCurrent, FloatReference manaMax)
//         {
//             if (!manaCurrent.useLocal) manaCurrent.variable.OnValueChanged += UpdateBarMana;
//             UpdateBarMana(manaCurrent);
//             if (!manaMax.useLocal) manaMax.variable.OnValueChanged += UpdateBarMana;
//         }
//
//         private void RemoveOldListeners()
//         {
//             if (mainCharacterSelect.main != null)
//             {
//                 var character = mainCharacterSelect.main;
//                 HealthListenerAllRemove(character.healthBase);
//                 ManaListenerAllRemove(character.stats.manaCurrent, character.stats.manaMax);
//             }
//         }
//
//         private void HealthListenerAllRemove(HealthBase healthBase)
//         {
//             healthBase.OnCurrentChanged -= HealthBarFillUpdate;
//             healthBase.OnMaxChanged -= HealthBarWidthUpdate;
//         }
//
//         private void ManaListenerAllRemove(FloatReference statsManaCurrent, FloatReference statsManaMax)
//         {
//             if (!statsManaCurrent.useLocal) statsManaCurrent.variable.OnValueChanged -= UpdateBarMana;
//             if (!statsManaMax.useLocal) statsManaMax.variable.OnValueChanged -= UpdateBarMana;
//         }
//
//
//         private void HealthBarFillUpdate(float current)
//         {
//             healthBar.Value = current / mainCharacterSelect.main.healthBase.Max;
//         }
//
//         private void UpdateBarMana(float current)
//         {
//             manaBar.Value = current / mainCharacterSelect.main.stats.manaMax;
//         }
//
//         private void HealthBarWidthUpdate(float maxHealth)
//         {
//             var size = healthBarRect.sizeDelta;
//             var barSize = healthBarRange.y - healthBarRange.x;
//             var newSize = healthBarRange.x + Mathf.Log(maxHealth + 1, healthBarRange.x) * barSize - barSize;
//             healthBarRect.sizeDelta = new Vector2(newSize, size.y);
//         }
//     }
// }