using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Text;
using System.IO;

namespace UScroll
{
    public class SliderPageOutScrollView : ScrollRect
    {
        private Vector3 startPos;
        private Vector3 endPos;
        private float scrollBarValue;
        public float para = 6f;
        public float time = 0;
        public float targetValue;
        public float origin;
        public int total = 3;//3 or 4
        public AnimationCurve curve;
        public float dir;
        public static float startTime;
        public static float endTime;
        public List<SliderPage> pages = new List<SliderPage>();
        public float width = 2160f;
        public event System.Action<float> FreshEvent;
        public bool IsMove
        {
            get
            {
                return SliderPageScrollView.isHozonticalMove;
            }

            set
            {
                SliderPageScrollView.isHozonticalMove = value;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            pages = Calculate(total);
        }

        protected override void OnDestroy()
        {
            FreshEvent = null;
        }

        private SliderPage GetPage(float value)
        {
            foreach (var page in pages)
            {
                if (page.min <= value && page.max >= value)
                {
                    return page;
                }
            }
            return null;
        }

        public static List<SliderPage> Calculate(int total, float para = 0.5f)
        {
            if (total < 3)
            {
                Debug.LogError("");
                return null;
            }
            List<SliderPage> pages = new List<SliderPage>();
            float space = 1 / (float)(total - 1);

            for (int i = 0; i < total; i++)
            {
                float bar = i * space;
                float min = bar - space * para <= 0 ? 0 : bar - space * para;
                float max = bar + space * para >= 1 ? 1 : bar + space * para;
                SliderPage page = new SliderPage(bar, min, max, i, space);
                pages.Add(page);
            }

            return pages;

        }

        public void Fresh()
        {
            if (FreshEvent != null)
            {
                FreshEvent(horizontalScrollbar.value);
            }
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            if (IsMove && !SliderPageScrollView.IsPress())
            {
                time += Time.deltaTime;
                float distance = Mathf.Abs(horizontalScrollbar.value - scrollBarValue);

                if (distance < 0.01f)
                {
                    horizontalScrollbar.value = scrollBarValue;
                    IsMove = false;
                    time = 0;
                    return;
                }
                float value = dir * targetValue * curve.Evaluate(time * para);
                horizontalScrollbar.value = origin + value;
                Fresh();
            }
        }


        public void CalculateMovement(float value)
        {
            scrollBarValue = value;
            time = 0;
            origin = horizontalScrollbar.value;
            targetValue = Mathf.Abs(horizontalScrollbar.value - scrollBarValue);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            startTime = Time.realtimeSinceStartup;
            startPos = eventData.position;
            base.OnBeginDrag(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            //if ((horizontalScrollbar.value == 0 && eventData.delta.x >= 0) || (horizontalScrollbar.value == 1 && eventData.delta.x <= 0))
            //{
            //    //屏幕两边外不处理
            //}
            //else
            //{
            //base.OnDrag(eventData);
            //if (horizontalScrollbar.value == 0 && content.localPosition.x > 0)
            //{
            //    content.anchoredPosition3D = Vector3.zero;
            //}
            //else if (horizontalScrollbar.value == 1 && content.localPosition.x < -width)
            //{
            //    content.anchoredPosition3D = new Vector3(-width, 0, 0);
            //}
            //}
            base.OnDrag(eventData);
            if (horizontalScrollbar.value == 0 && content.localPosition.x > 0)
            {
                content.anchoredPosition3D = Vector3.zero;
            }
            else if (horizontalScrollbar.value == 1 && content.localPosition.x < -width)
            {
                content.anchoredPosition3D = new Vector3(-width, 0, 0);
            }
            Fresh();
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            endTime = Time.realtimeSinceStartup;
            endPos = eventData.position;

            float deltaX = endPos.x - startPos.x;

            SliderPage page = GetPage(horizontalScrollbar.value);
            if (deltaX != 0 && ((endTime - startTime < 0.3f) || (Mathf.Abs(deltaX) > viewport.rect.width / 2)))
            {
                dir = -deltaX / Mathf.Abs(deltaX);
                int index = page.index;
                int nextPage = dir > 0 ? index == total - 1 ? total - 1 : index + (int)dir : index == 0 ? 0 : index + (int)dir;
                CalculateMovement(pages[nextPage].bar);
            }
            else
            {
                dir = horizontalScrollbar.value >= page.bar ? -1 : 1;
                CalculateMovement(page.bar);
            }

        }

        public void PointUp()
        {
            SliderPage page = GetPage(horizontalScrollbar.value);
            dir = horizontalScrollbar.value >= page.bar ? -1 : 1;
            CalculateMovement(page.bar);
        }



    }

}
