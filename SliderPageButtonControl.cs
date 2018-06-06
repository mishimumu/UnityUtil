using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UScroll;
public class SliderPageButtonControl : MonoBehaviour
{
    [SerializeField]
    private SliderPageButton[] btnArr;
    private List<SliderPage> pages = new List<SliderPage>();

    // Use this for initialization
    void Start()
    {
        if (btnArr != null)
        {
            pages = SliderPageOutScrollView.Calculate(btnArr.Length, 1);

            if (pages.Count == btnArr.Length)
            {
                for (int i = 0; i < btnArr.Length; i++)
                {
                    btnArr[i].Init(pages[i]);
                }
            }
        }

    }

}
