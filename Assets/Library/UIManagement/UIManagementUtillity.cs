using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Library
{
    namespace UIManagement
    {
        public static class UIManagementUtillity
        {
            /// <summary>
            /// ��� UI�� Ȱ��ȭ/��Ȱ��ȭ ��
            /// </summary>
            /// <param name="active">Ȱ��ȭ/��Ȱ��ȭ</param>
            public static void ActiveAllUI(bool active)
            {
                int uiCount = UIManager.Instance.Uis.Count;

                for(int i = 0; i < uiCount; i++)
                {
                    UIManager.Instance.ActiveUI((UiType)i, active);
                }
            }
            /// <summary>
            /// UI�� ������Ʈ�� ������
            /// </summary>
            /// <typeparam name="T">������ Ÿ��</typeparam>
            /// <param name="uiType">Ÿ���� ������ UI�� Ű</param>
            /// <param name="setActiveTrue">UI�� ��Ȱ��ȭ �Ǿ����� �� Ȱ��ȭ ��ų��</param>
            /// <returns></returns>
            public static T GetComponent<T>(UiType uiType, bool setActiveTrue = false) where T : Component
            {
                if(UIManager.Instance.UiPairs[uiType].TryGetComponent(out T component))
                {
                    if(setActiveTrue)
                        UIManager.Instance.ActiveUI(uiType, true);

                    return component;
                }

                return null;
            }
            /// <summary>
            /// UI������Ʈ�� ������ ������
            /// </summary>
            /// <param name="uiType">������ ���� UI�� Ű</param>
            /// <param name="siblingIndex">���� ����</param>
            public static void SetSiblingIndex(UiType uiType, int siblingIndex)
            {
                UIManager.Instance.UiPairs[uiType].transform.SetSiblingIndex(siblingIndex);
            }
        }
    }
}
