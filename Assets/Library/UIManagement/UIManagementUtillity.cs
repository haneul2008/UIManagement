using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Library
{
    namespace UIManagement
    {
        public enum UIPos
            {
                Center,
                LeftCenter,
                RightCenter,
                TopCenter,
                BottomCenter,
                LeftTop,
                RightTop,
                LeftBottom,
                RightBottom
            }

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
                    if(!UIManager.Instance.UiInstantiated[(int)uiType] && setActiveTrue)
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

            /// <summary>
            /// UI의 위치를 설정해줌
            /// </summary>
            /// <param name="uiType">위치를 정할 UI의 키</param>
            /// <param name="type">조정할 위치 타입</param>
            /// <param name="offset">위치 타입을 기준으로 더 조정할 값</param>
            public static void SetUiPos(UiType uiType, UIPos type, Vector2 offset)
            {
                Vector2 pos = Vector2.zero;
                Vector2 defOffSet = Vector2.zero;

                switch (type)
                {
                    case UIPos.Center:
                        pos = new Vector2(0.5f, 0.5f);
                        break;

                    case UIPos.LeftCenter:
                        pos = new Vector2(0, 0.5f);
                        defOffSet = new Vector2(1, 0);
                        break;

                    case UIPos.RightCenter:
                        pos = new Vector2(1, 0.5f);
                        defOffSet = new Vector2(-1, 0);
                        break;

                    case UIPos.TopCenter:
                        pos = new Vector2(0.5f, 1);
                        defOffSet = new Vector2(0, -1);
                        break;

                    case UIPos.BottomCenter:
                        pos = new Vector2(0.5f, 0);
                        defOffSet = new Vector2(0, 1);
                        break;

                    case UIPos.LeftTop:
                        pos = new Vector2(0, 1);
                        defOffSet = new Vector2(1, -1);
                        break;

                    case UIPos.RightTop:
                        pos = new Vector2(1, 1);
                        defOffSet = new Vector2(-1, -1);
                        break;

                    case UIPos.LeftBottom:
                        pos = new Vector2(0, 0);
                        defOffSet = new Vector2(1, 1);
                        break;

                    case UIPos.RightBottom:
                        pos = new Vector2(1, 0);
                        defOffSet = new Vector2(-1, 1);
                        break;
                }

                if (UIManager.Instance.UiPairs[uiType].TryGetComponent(out RectTransform rectTrm))
                {
                    defOffSet *= new Vector2(rectTrm.sizeDelta.x / 2, rectTrm.sizeDelta.y / 2);
                }

                UiPos(uiType, pos, defOffSet + offset, false, rectTrm);
            }
            
            //pos = 앵커 위치, offset = 앵커를 기준으로 조정할 위치, worldPos = 월드 포지션인지, uiRectTrm = 위치를 수정할 ui의 rectTrm이 있는 경우 할당
            private static void UiPos(UiType uiType, Vector2 pos, Vector2 offset, bool worldPos = true, RectTransform uiRectTrm = null)
            {
                if (worldPos)
                    Camera.main.WorldToScreenPoint(pos);

                if (!UIManager.Instance.UiInstantiated[(int)uiType])
                    UIManager.Instance.ActiveUI(uiType, true);

                GameObject ui = UIManager.Instance.UiPairs[uiType].gameObject;

                RectTransform rectTrmToEdit = null;

                if(uiRectTrm is null)
                {
                    if (ui.TryGetComponent(out RectTransform rectTrm))
                    {
                        rectTrmToEdit = rectTrm;
                    }
                    if (rectTrmToEdit is null) return;
                }
                else
                    rectTrmToEdit = uiRectTrm;

                rectTrmToEdit.anchorMax = pos;
                rectTrmToEdit.anchorMin = pos;
                rectTrmToEdit.anchoredPosition = offset;
            }
        }
    }
}
