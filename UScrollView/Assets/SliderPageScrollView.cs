using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using System.IO;

namespace UScroll
{
    public class SliderPageScrollView : ScrollRect, IPointerDownHandler, IPointerUpHandler
    {
        public static string src;
        private static Dictionary<int, PointerEventData> pointerMap = new Dictionary<int, PointerEventData>();
        [SerializeField]
        private ScrollRect parentScrollRect;
        private static bool isVerticalMove;
        public static bool isHozonticalMove;
        private static bool isDrag;
        private static int id = -1;
        private static List<int> touches = new List<int>();

        protected override void Start()
        {
            base.Start();
        }

        public void Print()
        {
            string path = Application.persistentDataPath + "/log.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.WriteAllText(path, src);
        }

        public static bool IsPress()
        {
            return touches.Count == 0 ? false : true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //解决多点触控:一个控件多个按压和移动时多个控件多个按压
            //微信的解决方案：第一按压a时，可以操作，当这个按压还在情况另外个按压b进来，那么上次按压a就无效了，新的按压b可以操作
            //此时按压b离开，则按压a再次激活
            //按压id：按压到屏幕上，分配一个id（从0开始，找到一个未使用的id）,并标记为使用，这个id不会被其他按压占有。弹起则释放这个id，重新标记为未使用
            //当上一个按压离开后，需要更新下一个拖拽处理器的状态(onbegindrag)
            id = eventData.pointerId;
            if (!pointerMap.ContainsKey(id))
            {
                pointerMap.Add(id, eventData);
            }

            if (!touches.Contains(id))
            {
                touches.Add(eventData.pointerId);
            }

        }


        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (id != eventData.pointerId) return;
            isDrag = true;
            if (isHozonticalMove)
            {
                parentScrollRect.OnBeginDrag(eventData);
            }
            else if (isVerticalMove)
            {
                base.OnBeginDrag(eventData);
            }
            else
            {
                if (Mathf.Abs(eventData.delta.x) >= Mathf.Abs(eventData.delta.y))
                {
                    isHozonticalMove = true;
                    isVerticalMove = false;
                    parentScrollRect.OnBeginDrag(eventData);
                }
                else
                {
                    isVerticalMove = true;
                    isHozonticalMove = false;
                    base.OnBeginDrag(eventData);
                }
            }
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (id != eventData.pointerId) return;

            if (isHozonticalMove)
            {
                Vector2 delta = eventData.delta;
                if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
                    delta.x = delta.y;
                delta.y = 0;
                eventData.delta = delta;
                parentScrollRect.OnDrag(eventData);
            }
            else if (isVerticalMove)
            {
                Vector2 delta = eventData.delta;
                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                    delta.y = delta.x;
                delta.x = 0;
                base.OnDrag(eventData);
            }

        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (touches.Count != 0) return;

            isDrag = false;
            if (isHozonticalMove)
            {
                parentScrollRect.OnEndDrag(eventData);
            }
            else if (isVerticalMove)
            {
                base.OnEndDrag(eventData);
            }

            isVerticalMove = false;

        }

        public static void Refresh()
        {
            isHozonticalMove = false;
            isVerticalMove = false;
            isDrag = false;
            id = -1;
        }

        public void OnPointerUp(PointerEventData eventData)
        {

            if (touches.Contains(eventData.pointerId))
            {
                touches.Remove(eventData.pointerId);
            }

            if (pointerMap.ContainsKey(eventData.pointerId))
            {
                pointerMap.Remove(eventData.pointerId);
            }

            if (touches.Count == 0)
            {
                id = -1;
                if (!isDrag && isHozonticalMove)
                {
                    ((SliderPageOutScrollView)parentScrollRect).PointUp();
                }
            }
            else
            {
                if (id == eventData.pointerId)
                {
                    touches.Sort();
                    id = touches[0];
                    PointerEventData data;
                    if (pointerMap.TryGetValue(id, out data))
                    {
                        OnBeginDrag(data);
                    }
                }
            }

        }

    }

}


