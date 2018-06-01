using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Text;
using System.IO;
using UScroll;

public class UScrollRect : ScrollRect, IPointerDownHandler, IPointerUpHandler
{
    public static bool isVerticalDrag;
    public static bool isHozonticalDrag;
    public static bool isNormal;
    public static bool isPress;
    public static int touches;
    public static float startTime;
    public static float endTime;
    public static Vector3 startPos;
    public static Vector3 endPos;
    public static PagePos pagePos;
    public UScrollList uScrollList;
    public static bool isMove = false;
    public SliderPageControl sliderPageControl;
    protected override void Start()
    {
        base.Start();
        uScrollList = GetComponentInParent<UScrollList>();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (isHozonticalDrag)
        {
            base.OnBeginDrag(eventData);//水平方向
        }
        else if (isVerticalDrag)
        {
            uScrollList.OnBeginDrag(eventData);
        }
        else
        {
            if (Mathf.Abs(eventData.delta.x) >= Mathf.Abs(eventData.delta.y))
            {
                isHozonticalDrag = true;
                isVerticalDrag = false;
                base.OnBeginDrag(eventData);//水平方向
            }
            else
            {
                isVerticalDrag = true;
                isHozonticalDrag = false;
                uScrollList.OnBeginDrag(eventData);
            }
        }
        startTime = Time.realtimeSinceStartup;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (isHozonticalDrag)
        {
            base.OnDrag(eventData);//水平方向
        }
        else if (isVerticalDrag)
        {
            uScrollList.OnDrag(eventData);
        }
        else
        {
            Debug.LogError("can't both choose Hozontical and Vertical");
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {

        endTime = Time.realtimeSinceStartup;
        if (isHozonticalDrag)
        {
            base.OnEndDrag(eventData);
        }
        else if (isVerticalDrag)
        {
            uScrollList.OnEndDrag(eventData);
        }

        base.OnEndDrag(eventData);
        //  Debug.Log("OnEndDrag" + "_" + eventData.delta);
        //  Debug.Log("OnEndDrag" + "_---" + JsonUtility.ToJson(eventData));
        //    log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},eligibleForClick:{4},pointerId:{5},position:{6},pressPosition:{7},scrollDelta: {8}", "OnEndDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.eligibleForClick, eventData.pointerId, eventData.position, eventData.pressPosition, eventData.scrollDelta);
        //    log += string.Format("useDragThreshold:{0},IsPointerMoving:{1}\n", eventData.useDragThreshold, eventData.IsPointerMoving());
        //log += string.Format("状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnEndDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition);


        // Debug.Log(string.Format("外部状态:{0},点击次数:{1},点击时间:{2},delta:{3},pointerId:{4},position:{5},pressPosition:{6}\n", "OnEndDrag", eventData.clickCount, eventData.clickTime, eventData.delta, eventData.pointerId, eventData.position, eventData.pressPosition));
        // byte[] bytes = System.Text.ASCIIEncoding.Unicode.GetBytes(log);
        endPos = eventData.position;
        float deltaX = endPos.x - startPos.x;
        //  Debug.Log(string.Format("{0},{1}", deltaX + "---" + viewport.rect.width / 2, "外部"));
        //   Debug.Log(string.Format("{0},{1},{2}", endTime - startTime, "外部", ((SliderPageScrollView)target).sliderPage.page));

        if ((endTime - startTime < 0.3f) || (Mathf.Abs(deltaX) > viewport.rect.width / 2))
        {
        //    if (deltaX < 0)
        //    {
        //        switch (((SliderPageScrollView)target).sliderPage.page)
        //        {
        //            case PagePos.Left:
        //                view.Do(0.5f, true);
        //                break;
        //            case PagePos.Center:
        //                view.Do(1f, true);
        //                break;
        //            case PagePos.Right:
        //                // Debug.Log("最右不移动");
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        switch (((SliderPageScrollView)target).sliderPage.page)
        //        {
        //            case PagePos.Left:
        //                //  Debug.Log("最左不移动");
        //                break;
        //            case PagePos.Center:
        //                view.Do(0f, false);
        //                break;
        //            case PagePos.Right:
        //                view.Do(0.5f, false);
        //                break;
        //        }
        //    }

        //}
        //else
        //{


        //    switch (((SliderPageScrollView)target).sliderPage.page)
        //    {
        //        case PagePos.Left:
        //            if (horizontalScrollbar.value < 0.5f)
        //            {
        //                view.Do(0, false);
        //            }
        //            else
        //            {
        //                Debug.LogError("返回异常" + horizontalScrollbar.value);
        //            }
        //            break;
        //        case PagePos.Center:
        //            if (horizontalScrollbar.value < 0.5f)
        //            {
        //                view.Do(0.5f, true);
        //            }
        //            else
        //            {
        //                view.Do(0.5f, false);
        //            }
        //            break;
        //        case PagePos.Right:
        //            Debug.Log("最右不移动");
        //            if (horizontalScrollbar.value > 0.5f)
        //            {
        //                view.Do(1, true);
        //            }
        //            else
        //            {
        //                Debug.LogError("返回异常" + horizontalScrollbar.value);
        //            }
        //            break;
        //    }
        }

    }

    bool isPlus;
    float time;
    public float target;
    public float origin;
    private float scrollBarValue;

    public void Do(float value, bool isPlus)
    {
        isNormal = false;
        isMove = true;
        scrollBarValue = value;
        this.isPlus = isPlus;
        time = 0;
        origin = horizontalScrollbar.value;
        target = Mathf.Abs(horizontalScrollbar.value - value);
    }

    void Update()
    {


        if (isMove && !isPress)
        {
            time += Time.deltaTime;
            float distance = Mathf.Abs(horizontalScrollbar.value - scrollBarValue);

            if (distance < 0.01f)
            {
                horizontalScrollbar.value = scrollBarValue;
                isMove = false;
                time = 0;
                isNormal = true;

                return;
            }

            if (isPlus)
            {
                horizontalScrollbar.value = origin + target * sliderPageControl.curve.Evaluate(time * sliderPageControl.para);
            }
            else
            {
                horizontalScrollbar.value = origin - target * sliderPageControl.curve.Evaluate(time * sliderPageControl.para);
            }
        }


    }


    public void OnPointerUp(PointerEventData eventData)
    {
        touches--;

        if (touches < 0)
        {
            Debug.LogError("按压错误");
            touches = 0;
        }

        if (touches == 0)
        {
            isPress = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPress = true;
        if (isNormal)
        {
            isHozonticalDrag = false;
            isVerticalDrag = false;
        }
        else
        {
            isVerticalDrag = false;
        }
        touches++;
        startPos = eventData.position;
    }
}


