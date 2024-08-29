using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Library
{
    namespace UIManagement
    {
        public class UIManager : MonoBehaviour
        {
            public Dictionary<UiType, UIMono> UiPairs = new Dictionary<UiType, UIMono>();
            private List<bool> _uiInstantiated = new List<bool>();

            private static UIManager _instance = null;
            public Canvas standardCanvas;

            public static UIManager Instance
            {
                get
                {
                    if (_instance is null)
                    {
                        _instance = FindObjectOfType<UIManager>();

                        if (_instance is null)
                            Debug.LogWarning("UiManager singleton is not exits");
                    }

                    return _instance;
                }
            }
            [field: SerializeField] public List<UiElementSO> Uis { get; private set; }

            private string _uiElementPath = "Assets/Library/UIManagement/UiElements";

            private void Awake()
            {
                SetPairs();
            }

            public void ActiveUI(UiType uiType, bool active, int siblingIndex = -1)
            {
                UIMono ui = UiPairs[uiType];

                if (ui == null) return;

                if (!_uiInstantiated[(int)uiType] && active)
                {
                    _uiInstantiated[(int)uiType] = true;

                    UIMono uiObject = Instantiate(ui, standardCanvas.transform);
                    uiObject.gameObject.name = UiPairs[uiType].gameObject.name;
                    UiPairs[uiType] = uiObject;
                }

                ui.gameObject.SetActive(active);

                if(siblingIndex >= 0 && active) 
                    UIManagementUtillity.SetSiblingIndex(uiType, siblingIndex);

                if (active)
                    ui.OnActiveTrue();
                else
                    ui.OnActiveFalse();
            }

            #region 에디터에서
            public void GenerateUiElementsEnumFile()
            {
                StringBuilder codeBuilder = new StringBuilder();
                foreach (UiElementSO item in Uis)
                {
                    codeBuilder.Append(item.key);
                    codeBuilder.Append(",");
                }

                string code = string.Format(CodeFormat.UITypeFormat, codeBuilder.ToString());
                string path = $"{Application.dataPath}/Library/UIManagement/Uitype.cs";
                Debug.Log(path);
                File.WriteAllText(path, code);

                AssetDatabase.Refresh();
            }

            private void SetPairs()
            {
                UiPairs.Clear();

                for(int i = 0; i < Uis.Count; i++)
                {
                    UiPairs.Add((UiType)i, Uis[i].ui);
                    _uiInstantiated.Add(false);
                }
            }

            public void GenerateUiList()
            {
                List<UiElementSO> list = CreatAssetDatabase();

                Uis = list;
            }

            private List<UiElementSO> CreatAssetDatabase()
            {
                List<UiElementSO> list = new List<UiElementSO>();

                string[] assetGuids = AssetDatabase.FindAssets("", new[] { _uiElementPath });

                foreach (var guid in assetGuids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    UiElementSO item = AssetDatabase.LoadAssetAtPath<UiElementSO>(path);

                    if (item != null)
                    {
                        list.Add(item);
                    }
                }

                return list;
            }
            #endregion
        }
    }
}

