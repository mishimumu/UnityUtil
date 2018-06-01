using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class UScrollRectChild : ScrollRect, IPointerDownHandler, IPointerUpHandler
{

    private bool isVerticalDrag;
    private bool isHozonticalDrag;
    public string log;
    public Button btn;

    private Vector3 startPos;
    private Vector3 endPos;
    public static ScrollRect target;
    protected override void Start()
    {
        base.Start();

    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
     //   SliderPageControl.startPos = eventData.position;
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
        //string path = Application.dataPath + "/log.xml";
        //if (File.Exists(path))
        //{
        //    File.Delete(path);
        //}
        //File.WriteAllText(path, log);
        // txt.text = "";
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
        Debug.Log(string.Format("外部状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition));

        if (horizontalScrollbar.value == 0 && eventData.delta.x >= 0)
        {
            // Debug.LogError(string.Format("{0},{1}", "左边界", "外部"));
        }
        else if (horizontalScrollbar.value == 1 && eventData.delta.x <= 0)
        {
            // Debug.LogError(string.Format("{0},{1}", "右边界", "外部"));
        }
        else
        {
            base.OnDrag(eventData);
            if (horizontalScrollbar.value == 0 && content.localPosition.x > 0)
            {
                content.anchoredPosition3D = Vector3.zero;
            }
            else if (horizontalScrollbar.value == 1 && content.localPosition.x < -2160f)
            {
                content.anchoredPosition3D = new Vector3(-2160f, 0, 0);

            }
        }

        Debug.Log(string.Format("外部状态:{0},点击次数:{1}", content.anchoredPosition3D, content.localPosition));
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        //  Debug.Log("OnEndDrag" + "_" + eventData.delta);
        //  Debug.Log("OnEndDrag" + "_---" + JsonUtility.ToJson(eventData));
        //    log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},eligibleForClick:{4},pointerId:{5},position:{6},pressPosition:{7},scrollDelta: {8}", "OnEndDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.eligibleForClick, eventData.pointerId, eventData.position, eventData.pressPosition, eventData.scrollDelta);
        //    log += string.Format("useDragThreshold:{0},IsPointerMoving:{1}\n", eventData.useDragThreshold, eventData.IsPointerMoving());
        //log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnEndDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition);
       


        // Debug.Log(string.Format("外部状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnEndDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition));
        // byte[] bytes = System.Text.ASCIIEncoding.Unicode.GetBytes(log);
     //   SliderPageControl.endPos = eventData.position;
      //  float deltaX = SliderPageControl.endPos.x - SliderPageControl.startPos.x;
        //  Debug.Log(string.Format("{0},{1}", deltaX + "---" + viewport.rect.width / 2, "外部"));
        //   Debug.Log(string.Format("{0},{1},{2}", SliderPageControl.endTime - SliderPageControl.startTime, "外部", ((SliderPageScrollView)target).sliderPage.page));

        

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //  Debug.Log("OnPointerUp" + "_" + eventData.selectedObject);
        // log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnPointerUp", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition);
        // view.OnPointerUp(eventData);
        //  Debug.Log("OnPointerUp" + "_" + Input.touchCount);
        //  Debug.Log("OnPointerUp" + "_" + Input.touches.Length);
        // Debug.Log("OnPointerUp" + "_" + Input.touches.Length);
        if (Input.touchCount == 1)
        {

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown" + "_" + eventData.selectedObject);
        log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnPointerDown", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition);

        //  view.OnPointerDown(eventData);

    }
}
