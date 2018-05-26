using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace UScroll
{
    [RequireComponent(typeof(SliderPageOutScrollView))]
    public sealed class SliderPageControl : MonoBehaviour
    {

        private float sliderTime;
        private float sliderSpeed;
        public SliderPageOutScrollView scrollRect;
        private float scrollBarValue;
        private bool isMove = false;
        private float areaValue;
        private bool isPress;
        private float pressTime;
        private float onPointerDownValue;
        private float onPointerUpValue;
        [SerializeField]
        private float currentPage = 0;//方便计算使用float类型
        public float CurrentPage
        {
            get
            {
                return currentPage;
            }

            private set
            {
                lastPage = currentPage;
                currentPage = value;
                if (currentPage > pageNumber)
                {
                    currentPage = pageNumber;
                }
                else if (currentPage <= 0)
                {
                    currentPage = 0;
                }
                if (FinishChangePageEvent != null)
                {
                    FinishChangePageEvent((int)lastPage, (int)CurrentPage);
                }
            }
        }
        private float velocity;
        private float lastPage;
        [SerializeField]
        private float pageNumber;//值为总页数-1
        [SerializeField]
        private int defaultPage;

        public int screenMoveRate = 4;//屏幕滑动距离1/n，用来比较滑动时是复位还是左右滑动
        public System.Action<int, int> FinishChangePageEvent;

        // Use this for initialization
        void Start()
        {
            if (pageNumber == 0)
            {
                this.enabled = false;
                Debug.LogWarning("list is null");
            }
            else
            {
                scrollRect = GetComponent<SliderPageOutScrollView>();
                // scrollRect.onValueChanged.AddListener(Onchange);
                AddEvent();
                areaValue = 1f / pageNumber / screenMoveRate;

                scrollRect.horizontalScrollbar.value = CurrentPage / pageNumber;
            }

        }

        void Onchange(Vector2 pos)
        {
            Debug.LogWarning("变化：" + pos);
        }

        /// <summary>
        /// 初始化滑动列表
        /// </summary>
        /// <param name="total"></param>
        /// <param name="defaultPage">从0开始</param>
        /// <param name="callback"></param>
        public void Init(int total, int defaultPage, System.Action<int, int> callback)
        {
            total -= 1;
            if (total == 0 || total < defaultPage)
            {
                return;
            }
            pageNumber = total;
            CurrentPage = defaultPage;
            FinishChangePageEvent = callback;
        }


        void AddEvent()
        {
            EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
            UnityAction<BaseEventData> pointerDown = new UnityAction<BaseEventData>(OnPointerDown);
            EventTrigger.Entry onPointerDown = new EventTrigger.Entry();
            onPointerDown.eventID = EventTriggerType.PointerDown;
            onPointerDown.callback.AddListener(pointerDown);

            UnityAction<BaseEventData> pointerUp = new UnityAction<BaseEventData>(OnPointerUp);
            EventTrigger.Entry onPointerUp = new EventTrigger.Entry();
            onPointerUp.eventID = EventTriggerType.PointerUp;
            onPointerUp.callback.AddListener(pointerUp);
            trigger.triggers.Add(onPointerDown);
            trigger.triggers.Add(onPointerUp);

        }

        void Update()
        {
            if (isPress)
            {
                //  Debug.LogWarning("列表按下");
                pressTime += Time.deltaTime;
            }

            if (isMove)
            {
                // Debug.LogWarning("外列表移动");

                if (Mathf.Abs(scrollRect.horizontalScrollbar.value - scrollBarValue) < 0.0001f)
                {
                    scrollRect.horizontalScrollbar.value = scrollBarValue;
                    isMove = false;


                    return;
                }

                scrollRect.horizontalScrollbar.value = Mathf.SmoothDamp(scrollRect.horizontalScrollbar.value, scrollBarValue, ref velocity, sliderTime);
            }


        }

        public void OnPointerDown(BaseEventData data)
        {
            isMove = false;
            Debug.Log("down" + scrollRect.horizontalScrollbar.value + "_" + data.selectedObject);
            isPress = true;
            pressTime = 0;
            onPointerDownValue = scrollRect.horizontalScrollbar.value;
        }

        public void OnPointerUp(BaseEventData data)
        {
            isPress = false;
            onPointerUpValue = scrollRect.horizontalScrollbar.value;
            float temp = onPointerUpValue - onPointerDownValue;
            Debug.Log("up  按压时间：" + pressTime + ",滑动距离" + temp + "_" + data.selectedObject);

            if (temp == 0)
            {
                return;
            }

            if (pressTime < 0.3f)
            {
                if (temp > 0)
                {

                    //屏幕右移
                    CurrentPage++;
                    Debug.Log("屏幕右移：" + CurrentPage);

                    scrollBarValue = CurrentPage / pageNumber;
                }
                else if (temp < 0)
                {

                    CurrentPage--;
                    Debug.Log("屏幕左移：" + CurrentPage);

                    scrollBarValue = CurrentPage / pageNumber;
                }
            }
            else
            {
                if (temp > 0)
                {
                    if (Mathf.Abs(temp) >= areaValue)
                    {


                        CurrentPage++;
                        Debug.Log("屏幕滑动右移：" + CurrentPage);

                        scrollBarValue = CurrentPage / pageNumber;
                    }
                    else
                    {
                        Debug.Log("屏幕滑动归位：" + CurrentPage);


                        scrollBarValue = CurrentPage / pageNumber;
                    }
                }
                else if (temp < 0)
                {
                    if (Mathf.Abs(temp) >= areaValue)
                    {

                        CurrentPage--;
                        Debug.Log("屏幕滑动左移：" + CurrentPage);

                        scrollBarValue = CurrentPage / pageNumber;
                    }
                    else
                    {
                        Debug.Log("屏幕滑动归位：" + CurrentPage);


                        scrollBarValue = CurrentPage / pageNumber;
                    }
                }
            }

            Debug.Log("up" + scrollBarValue);

            //移动距离比较小的时候，缩小时间
            if (Mathf.Abs(scrollRect.horizontalScrollbar.value - scrollBarValue) < 0.05f)
            {
                sliderTime = Mathf.Clamp(pressTime, 0f, 0.2f);

            }
            else
            {
                sliderTime = Mathf.Clamp(pressTime, 0f, 0.5f);

            }
            isMove = true;
            velocity = 0;
        }





    }

}
