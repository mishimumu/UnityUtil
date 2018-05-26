using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace UScroll
{
    public class SliderPageScrollView : ScrollRect, IPointerDownHandler, IPointerUpHandler
    {

        //限制只能左右滑动或者上下滑动
        private bool isVerticalDrag;
        private bool isHozonticalDrag;
        public SliderPageControl view;

        protected override void Start()
        {
            base.Start();
            view = GetComponentInParent<SliderPageControl>();
            if (!view)
            {
                Debug.LogError("SliderPageControl is no exit");
            }
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            view.scrollRect.OnBeginDrag(eventData);
            isVerticalDrag = true;
            isHozonticalDrag = true;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (Mathf.Abs(eventData.delta.x) >= Mathf.Abs(eventData.delta.y))
            {
                if (isHozonticalDrag)
                {
                    isVerticalDrag = false;
                    Debug.Log("开始水平" + "_" + eventData.delta);
                    view.scrollRect.OnDrag(eventData);
                }
            }
            else
            {
                if (isVerticalDrag)
                {
                    isHozonticalDrag = false;
                    Debug.Log("开始垂直" + "_" + eventData.delta);
                    base.OnDrag(eventData);
                }
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (isHozonticalDrag)
            {
                Debug.Log("水平结束" + "_" + eventData.selectedObject);
                isHozonticalDrag = false;
            }
            view.scrollRect.OnEndDrag(eventData);
            if (isVerticalDrag)
            {
                isVerticalDrag = false;
                Debug.Log("垂直结束" + "_" + eventData.selectedObject);
                base.OnEndDrag(eventData);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("OnPointerUp" + "_" + eventData.selectedObject);
            view.OnPointerUp(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown" + "_" + eventData.selectedObject);
            view.OnPointerDown(eventData);

        }
    }

}
