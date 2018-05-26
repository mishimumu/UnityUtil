using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
namespace UScroll
{
    public class SliderPageButton : Button, IDropHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {

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

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");
            view.scrollRect.OnDrag(eventData);

        }


        public override void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("OnPointerExit");
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDrop");
            view.OnPointerUp(eventData);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            view.OnPointerDown(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            view.OnPointerUp(eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown");
            view.OnPointerDown(eventData);
        }


        public override void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("OnPointerUp");
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            Debug.Log("OnSubmit");
        }
    }

}
