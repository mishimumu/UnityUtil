using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UScroll;
public class SliderPageButton : MonoBehaviour
{
    [SerializeField]
    private int index;
    private SliderPage page;
    [HideInInspector]
    public Button uBtn;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private SliderPageOutScrollView view;

    void Awake()
    {
        uBtn = GetComponent<Button>();
        if (uBtn != null)
        {
            uBtn.onClick.AddListener(Onclick);
        }
    }

    public void Init(SliderPage page)
    {
        if (view != null && view.pages.Count > index && page != null)
        {
            this.page = page;
            view.FreshEvent += Refresh;
        }
    }

    void OnDestroy()
    {
        if (view != null)
        {
            view.FreshEvent -= Refresh;
        }
    }

    void Onclick()
    {
        if (page != null && view != null)
        {
            SliderPageScrollView.Refresh();
            view.horizontalScrollbar.value = page.bar;
            view.Fresh();
        }
    }

    private void Refresh(float value)
    {
        Color color = icon.color;
        if (page.min <= value && value <= page.max)
        {
            float per = 1 - Mathf.Abs(value - page.bar) / page.length;
            color.a = per;
        }
        else
        {
            color.a = 0;
        }
        icon.color = color;
    }
}
