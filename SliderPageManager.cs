using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace UScroll
{
    public class SliderPageManager : MonoBehaviour
    {
        /*
        目前处理的对象都是当作不可复用的界面，后续加入是否可以重用的
        */
        private SliderPageControl sliderPageView;
        public Dictionary<string, GameObject> cacheMap = new Dictionary<string, GameObject>();//缓存页面，name唯一
        public List<string> pageList = new List<string>();//页面列表，保存着页面的顺序和名字
        public Transform parentNode;
        public float pageWidth;
        public float pageHeight;
        public List<string> currentPageName = new List<string>();//如果lastPageName的值已经不在currentPageName时，将这个值的游戏物体隐藏
        public List<string> lastPageName = new List<string>();

        /// <summary>
        /// 初始化页面列表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="defaultPage"></param>
        public void PageEntry(List<string> list, int defaultPage)
        {
            if (list == null)
            {
                return;
            }

            this.pageList = list;
            parentNode.GetComponent<RectTransform>().sizeDelta = new Vector2(pageWidth * list.Count, pageHeight);
            sliderPageView = gameObject.AddComponent<SliderPageControl>();
            sliderPageView.Init(list.Count, defaultPage, ChangePage);
            InitPage(defaultPage);
        }

        /// <summary>
        /// 动态更新页面列表
        /// </summary>
        public void FreshPageEntry(List<string> pageList)
        {

        }

        /// <summary>
        /// 跳转到指定页面
        /// </summary>
        public void AssignPageEntry(List<string> pageList)
        {

        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        /// <param name="initPage"></param>
        void InitPage(int initPage)
        {
            //加载默认显示页
            LoadPageObj(initPage);

            //加载默认显示页左右页面
            ChangePage(-1, initPage);
        }

        /// <summary>
        /// 加载显示页左右两个页面
        /// </summary>
        /// <param name="last"></param>
        /// <param name="target"></param>
        void ChangePage(int last, int target)
        {
            //如果没有切换页面，则不处理
            if (last == target)
            {
                return;
            }
            currentPageName.Clear();
            currentPageName.Add(pageList[target]);
            int rightPage = target + 1;
            if (rightPage < pageList.Count)
            {
                currentPageName.Add(pageList[rightPage]);

                LoadPageObj(rightPage);
            }

            int leftPage = target - 1;
            if (leftPage >= 0)
            {
                currentPageName.Add(pageList[leftPage]);

                LoadPageObj(leftPage);
            }

            HideoObj();

        }


        void LoadPageObj(int page)
        {
            string name = pageList[page];
            // Debug.Log("加载界面：" + name + ",页码:" + page);
            if (cacheMap.ContainsKey(name))
            {
                GameObject obj = cacheMap[name];
                obj.GetComponent<RectTransform>().sizeDelta = new Vector2(pageWidth, pageHeight);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(page * pageWidth, 0);

                obj.SetActive(true);
            }
            else
            {
                SliderLoader.Load(name, parentNode, new Vector3(page * pageWidth, 0, 0), new Vector2(pageWidth, pageHeight), SaveObj);
            }
        }

        void SaveObj(string name, GameObject obj)
        {
            if (!cacheMap.ContainsKey(name))
            {
                cacheMap.Add(name, obj);
            }
            else
            {
                Debug.LogError("exit same the key !");
            }
        }

        /// <summary>
        /// 比较上次的界面名称是否还在当前的界面名称列表中，如果不在则进行隐藏
        /// </summary>
        void HideoObj()
        {
            int curPageListCount = currentPageName.Count;
            int lastPageListCount = lastPageName.Count;
            for (int i = 0; i < lastPageListCount; i++)
            {
                bool isShow = false;
                for (int j = 0; j < curPageListCount; j++)
                {
                    if (currentPageName[j] == lastPageName[i])
                    {
                        isShow = true;
                        break;
                    }
                }
                string name = lastPageName[i];
                if (cacheMap.ContainsKey(name))
                {
                    cacheMap[name].SetActive(isShow);
                }
            }

            lastPageName.Clear();
            for (int j = 0; j < curPageListCount; j++)
            {
                lastPageName.Add(currentPageName[j]);
            }

        }

    }

}




