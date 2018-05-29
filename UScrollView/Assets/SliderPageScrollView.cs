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
        public PointerEventData startData;
        public PointerEventData endData;
        public float startTime;
        public float endTime;
        public Vector3 startPos;
        public Vector3 endPos;
        public ScrollRect rect;
        protected override void Start()
        {
            base.Start();
            //view = GetComponentInParent<SliderPageControl>();
            //if (!view)
            //{
            //    Debug.LogError("SliderPageControl is no exit");
            //}

            // rect= transform.Find("Node").GetComponent<ScrollRect>();
            rect = GameObject.Find("Node").GetComponent<ScrollRect>();
            // Debug.Log(rect.name);
        }

        protected virtual void OnChangePage()
        {
            Debug.Log("刷新数据");
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (SliderPageControl.isHozonticalDrag)
            {
                SliderPageControl.startTime = Time.realtimeSinceStartup;
                rect.OnBeginDrag(eventData);//水平
            }
            else if (SliderPageControl.isVerticalDrag)
            {
                base.OnBeginDrag(eventData);//垂直方向
            }
            else
            {
                if (Mathf.Abs(eventData.delta.x) >= Mathf.Abs(eventData.delta.y))
                {
                    SliderPageControl.isHozonticalDrag = true;
                    SliderPageControl.isVerticalDrag = false;
                    rect.OnBeginDrag(eventData);//水平
                }
                else
                {
                    SliderPageControl.isVerticalDrag = true;
                    SliderPageControl.isHozonticalDrag = false;
                    base.OnBeginDrag(eventData);//垂直方向

                }
            }
            SliderPageControl.startTime = Time.realtimeSinceStartup;

            // // isVerticalDrag = true;
            //   isHozonticalDrag = true;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            //if (Mathf.Abs(eventData.delta.x) >= Mathf.Abs(eventData.delta.y))
            //{
            //    if (isHozonticalDrag)
            //    {
            //        isVerticalDrag = false;
            //        // Debug.Log("开始水平" + "_" + eventData.delta);
            //        Debug.Log(string.Format("内部状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition));
            //        //  view.scrollRect.OnDrag(eventData);
            //        rect.OnDrag(eventData);
            //    }
            //}
            //else
            //{
            //    if (isVerticalDrag)
            //    {
            //        isHozonticalDrag = false;
            //        //  Debug.Log("开始垂直" + "_" + eventData.delta);
            //        base.OnDrag(eventData);
            //    }
            //}
            if (SliderPageControl.isHozonticalDrag)
            {
                rect.OnDrag(eventData);

            }
            else if (SliderPageControl.isVerticalDrag)
            {
                base.OnDrag(eventData);
            }
            else
            {

            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            /*
            if (isHozonticalDrag)
            {
                isHozonticalDrag = false;
                endData = eventData;
                endTime = Time.realtimeSinceStartup;
                endPos = eventData.position;

                Debug.Log(string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnEndDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition));
                //Debug.Log("水平结束" + "_" + Math.Abs(startData.position.x - endData.position.x));
                //Debug.Log("水平结束" + "_" + viewport.rect.width);
                //Debug.Log("水平结束" + "_" + startPos);
                //Debug.Log("水平结束" + "_" + endPos);
                //Debug.Log("水平结束" + "_" + Math.Abs(startPos.x - endPos.x));

                rect.OnEndDrag(eventData);
                if (endTime - startTime < 0.3f || Math.Abs(startPos.x - endPos.x) > viewport.rect.width / 2)
                {
                    OnChangePage();
                }
            }
            // view.scrollRect.OnEndDrag(eventData);
            if (isVerticalDrag)
            {
                isVerticalDrag = false;
                Debug.Log("垂直结束" + "_" + eventData.selectedObject);
                base.OnEndDrag(eventData);
            }

    */
            SliderPageControl.endTime = Time.realtimeSinceStartup;
            if (SliderPageControl.isHozonticalDrag)
            {
                rect.OnEndDrag(eventData);
            }
            else if (SliderPageControl.isVerticalDrag)
            {
                base.OnEndDrag(eventData);
            }
            else
            {

            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // Debug.Log(string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnPointerUp", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition));
           // Debug.Log(string.Format("{0},{1}", "OnPointerUp", "内部"));
            //   view.OnPointerUp(eventData);
           // SliderPageControl.isVerticalDrag = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //  Debug.Log(string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnPointerDown", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition));
          //  Debug.Log(string.Format("{0},{1}", "OnPointerDown", "内部"));
            if (SliderPageControl.isNormal)
            {
                SliderPageControl.isHozonticalDrag = false;
                SliderPageControl.isVerticalDrag = false;

            }
            else
            {
                SliderPageControl.isVerticalDrag = false;
            }
            //  view.OnPointerDown(eventData);

            startData = eventData;
            SliderPageControl.startPos = eventData.position;
            SliderPageControl.isMove = false;
        }
    }

}

public enum PagePos
{
    Left,
    Center,
    Right
}
