using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPage
{

    public float bar;
    public float min;
    public float max;
    public int index;
    public float length;
    public SliderPage(float bar, float min, float max, int index, float length = 0)
    {
        this.bar = bar;
        this.min = min;
        this.max = max;
        this.index = index;
        this.length = length;
    }

}
