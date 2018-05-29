using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Text;
using System.IO;

namespace UScroll
{
    public class SliderPageOutScrollView : ScrollRect, IPointerDownHandler, IPointerUpHandler
    {

        private bool isVerticalDrag;
        private bool isHozonticalDrag;
        public string log;
        public Button btn;
        private Vector3 startPos;
        private Vector3 endPos;
        public SliderPageControl view;

        protected override void Start()
        {
            base.Start();
            view = GetComponentInParent<SliderPageControl>();

        }
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            SliderPageControl.startPos = eventData.position;
            // Debug.Log("OnBeginDrag" + "_" + eventData.delta);
            // Debug.Log("OnBeginDrag" + "_-----" +JsonUtility.ToJson(eventData));
            //  log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},eligibleForClick:{4},pointerId:{5},position:{6},pressPosition:{7},scrollDelta: {8}", "OnBeginDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.eligibleForClick, eventData.pointerId, eventData.position, eventData.pressPosition, eventData.scrollDelta);
            //   log += string.Format("useDragThreshold:{0},IsPointerMoving:{1}\n", eventData.useDragThreshold, eventData.IsPointerMoving());
            //  log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnBeginDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition);
            // log += string.Format("IsPointerMoving:{1}\n", eventData.useDragThreshold, eventData.IsPointerMoving());
            //StringBuilder sb = new StringBuilder();
           // Debug.Log(string.Format("外部状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnBeginDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition));

        }

        public void Write()
        {
            string path = Application.dataPath + "/log.xml";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.WriteAllText(path, log);
        }

        public override void OnDrag(PointerEventData eventData)
        {

            //   Debug.Log("OnDrag" + "_----" + JsonUtility.ToJson(eventData));
            //  log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},eligibleForClick:{4},pointerId:{5},position:{6},pressPosition:{7},scrollDelta: {8}", "OnDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.eligibleForClick, eventData.pointerId, eventData.position, eventData.pressPosition, eventData.scrollDelta);
            //  log += string.Format("useDragThreshold:{0},IsPointerMoving:{1}\n", eventData.useDragThreshold, eventData.IsPointerMoving());
            //  log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition);
            //   Debug.Log("OnDrag" + "_" + eventData.delta);
            //if (Mathf.Abs(eventData.delta.x) >= Mathf.Abs(eventData.delta.y))
            //{
            //    if (isHozonticalDrag)
            //    {
            //        isVerticalDrag = false;
            //        base.OnDrag(eventData);
            //    }
            //}
            //else
            //{
            //    if (isVerticalDrag)
            //    {
            //        isHozonticalDrag = false;
            //    }
            //}
            // Debug.Log(string.Format("外部状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition));

            base.OnDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            //  Debug.Log("OnEndDrag" + "_" + eventData.delta);
            //  Debug.Log("OnEndDrag" + "_---" + JsonUtility.ToJson(eventData));
            //    log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},eligibleForClick:{4},pointerId:{5},position:{6},pressPosition:{7},scrollDelta: {8}", "OnEndDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.eligibleForClick, eventData.pointerId, eventData.position, eventData.pressPosition, eventData.scrollDelta);
            //    log += string.Format("useDragThreshold:{0},IsPointerMoving:{1}\n", eventData.useDragThreshold, eventData.IsPointerMoving());
            //log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnEndDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition);
            if (!SliderPageControl.isHozonticalDrag)
            {
                Debug.LogError(string.Format("{0},{1}", "OnEndDrag", "外部"));
                return;
            }
            Debug.Log(string.Format("外部状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnEndDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition));
            // byte[] bytes = System.Text.ASCIIEncoding.Unicode.GetBytes(log);
            SliderPageControl.endPos = eventData.position;
            float deltaX = SliderPageControl.endPos.x - SliderPageControl.startPos.x;
            Debug.Log(string.Format("{0},{1}", deltaX + "---" + viewport.rect.width / 2, "外部"));

            switch (SliderPageControl.pagePos)
            {
                case PagePos.Left:
                    if ((SliderPageControl.endTime - SliderPageControl.startTime < 0.3f) || (Mathf.Abs(deltaX) > viewport.rect.width / 2))
                    {
                        if (deltaX < 0)
                        {
                            view.Do(0.5f, true);
                        }
                        else
                        {
                            SliderPageControl.isMove = true;

                        }

                    }
                    else
                    {
                        if (horizontalScrollbar.value > 0f)
                        {
                            view.Do(0, false);
                        }
                    }
                    break;
                case PagePos.Center:
                    if ((SliderPageControl.endTime - SliderPageControl.startTime < 0.3f) || Mathf.Abs(deltaX) > viewport.rect.width / 2)
                    {
                        if (deltaX > 0)
                        {
                            view.Do(0, false);
                        }
                        else
                        {
                            view.Do(1, true);
                        }
                    }
                    else
                    {
                        if (horizontalScrollbar.value > 0.5f)
                        {
                            view.Do(0.5f, false);
                        }
                        else
                        {
                            view.Do(0.5f, true);
                        }

                    }
                    break;
                case PagePos.Right:
                    if ((SliderPageControl.endTime - SliderPageControl.startTime < 0.3f) || Mathf.Abs(deltaX) > viewport.rect.width / 2)
                    {
                        if (deltaX > 0)
                        {
                            //view.Do(1, true);
                            view.Do(0.5f, false);
                        }
                        else
                        {
                            SliderPageControl.isMove = true;

                        }
                    }
                    else
                    {
                        if (horizontalScrollbar.value < 1f)
                        {
                            view.Do(1f, true);
                        }
                    }
                    break;
            }

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("OnPointerUp" + "_" + eventData.selectedObject);
            log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnPointerUp", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition);
            // view.OnPointerUp(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown" + "_" + eventData.selectedObject);
            log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnPointerDown", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition);

            //  view.OnPointerDown(eventData);

        }
    }

}
