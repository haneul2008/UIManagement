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
            /// 모든 UI를 활성화/비활성화 함
            /// </summary>
            /// <param name="active">활성화/비활성화</param>
            public static void ActiveAllUI(bool active)
            {
                int uiCount = UIManager.Instance.Uis.Count;

                for(int i = 0; i < uiCount; i++)
                {
                    UIManager.Instance.ActiveUI((UiType)i, active);
                }
            }
            /// <summary>
            /// UI의 컴포넌트를 가져옴
            /// </summary>
            /// <typeparam name="T">가져올 타입</typeparam>
            /// <param name="uiType">타입을 가져올 UI의 키</param>
            /// <param name="setActiveTrue">UI가 비활성화 되어있을 때 활성화 시킬지</param>
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
            /// UI오브젝트의 순서를 정해줌
            /// </summary>
            /// <param name="uiType">순서를 정할 UI의 키</param>
            /// <param name="siblingIndex">정할 순서</param>
            public static void SetSiblingIndex(UiType uiType, int siblingIndex)
            {
                UIManager.Instance.UiPairs[uiType].transform.SetSiblingIndex(siblingIndex);
            }
        }
    }
}
