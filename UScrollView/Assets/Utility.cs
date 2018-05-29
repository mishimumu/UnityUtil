using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

public class Utility{

    public static uint GetTimeStamp()
    {
        System.TimeSpan ts = System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        return System.Convert.ToUInt32(ts.TotalSeconds);
    }
    
    public static string Md5Sum(string input)
    {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("x2"));
        }
        return sb.ToString();
    }
    
    public static string BaseFilePath()
    {
        string baseFilePath;
    #if UNITY_ANDROID
        //baseFilePath = "jar:file://" + Application.dataPath + "!/assets/" + "StreamingAssets";
        baseFilePath=Application.streamingAssetsPath+"/";
    #elif UNITY_IPHONE
        baseFilePath = Application.dataPath + "/Raw/" + "StreamingAssets";
    #elif UNITY_STANDALONE_WIN || UNITY_EDITOR
            baseFilePath = Application.dataPath + "/StreamingAssets/";
    #else
        baseFilePath = "file://" + Application.dataPath + "/StreamingAssets/";
    #endif
        return baseFilePath;
    }
    
    public static Sprite Texture2DToSprite(Texture2D t2d)
    {
        return Sprite.Create(t2d, new Rect(Vector2.zero, new Vector2(t2d.width, t2d.height)), new Vector2(0.5f, 0.5f));
    }
    
     public static Dictionary<string, double> Vector3ToDictionary(Vector3 pos)
    {
        Dictionary<string, double> tmp = new Dictionary<string, double>();
        tmp.Add("x", (double)pos.x);
        tmp.Add("y", (double)pos.y);
        tmp.Add("z", (double)pos.z);

        return tmp;
    }

    public static Vector3 DictionaryToVector3(IDictionary dict)
    {
        return new Vector3(float.Parse(dict["x"].ToString()),
                           float.Parse(dict["y"].ToString()),
                           float.Parse(dict["z"].ToString())
                           );
    }


}
