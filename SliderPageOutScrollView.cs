using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Text;
using System.IO;

namespace UScroll
{
    public class SliderPageOutScrollView : ScrollRect
    {

        private bool isVerticalDrag;
        private bool isHozonticalDrag;
        public string log;
        public Button btn;
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            Debug.Log("OnBeginDrag" + "_" + eventData.delta);
            // Debug.Log("OnBeginDrag" + "_-----" +JsonUtility.ToJson(eventData));
            //  log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},eligibleForClick:{4},pointerId:{5},position:{6},pressPosition:{7},scrollDelta: {8}", "OnBeginDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.eligibleForClick, eventData.pointerId, eventData.position, eventData.pressPosition, eventData.scrollDelta);
            //   log += string.Format("useDragThreshold:{0},IsPointerMoving:{1}\n", eventData.useDragThreshold, eventData.IsPointerMoving());
            //  log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnBeginDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition);
            // log += string.Format("IsPointerMoving:{1}\n", eventData.useDragThreshold, eventData.IsPointerMoving());
            //StringBuilder sb = new StringBuilder();
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
            //    log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition);
            Debug.Log("OnDrag" + "_" + eventData.delta);
            if (Mathf.Abs(eventData.delta.x) >= Mathf.Abs(eventData.delta.y))
            {
                if (isHozonticalDrag)
                {
                    isVerticalDrag = false;
                    base.OnDrag(eventData);
                }
            }
            else
            {
                if (isVerticalDrag)
                {
                    isHozonticalDrag = false;
                }
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            Debug.Log("OnEndDrag" + "_" + eventData.delta);
            //  Debug.Log("OnEndDrag" + "_---" + JsonUtility.ToJson(eventData));
            //    log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},eligibleForClick:{4},pointerId:{5},position:{6},pressPosition:{7},scrollDelta: {8}", "OnEndDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.eligibleForClick, eventData.pointerId, eventData.position, eventData.pressPosition, eventData.scrollDelta);
            //    log += string.Format("useDragThreshold:{0},IsPointerMoving:{1}\n", eventData.useDragThreshold, eventData.IsPointerMoving());
            //   log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnEndDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition);

            // byte[] bytes = System.Text.ASCIIEncoding.Unicode.GetBytes(log);

        }
    }

}
