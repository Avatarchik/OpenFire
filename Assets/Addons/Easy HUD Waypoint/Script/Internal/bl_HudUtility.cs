//////////////////////////////////////////////////////////////////////////////
// bl_HudUtility
//
//
// -Helper functions for Huds operations.
//
//                     Lovatto Studio
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections;

public static class bl_HudUtility
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <returns></returns>
    public static float GetRotation(float x1, float y1, float x2, float y2)
    {
        float pi = 3.141593f;
        float diferenceX = x2 - x1;
        float diferenceY = y2 - y1;
        float Atan = (Mathf.Atan(diferenceY / diferenceX) * 180) / pi;
        if (diferenceX < 0)
        {
            Atan += 180;
        }
        return Atan;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static Vector2 GetPivot(float h, float v, float size)
    {
        float num = h - (mCamera.pixelWidth / (2));
        float num2 = v - (mCamera.pixelHeight / (2));
        float num3 = num2 / num;
        Vector2 vector = Vector2.zero;
        float num4 = new float();
        if ((num3 > GetScreenSlope) || (num3 < -GetScreenSlope))
        {
            num4 = (MidlleHeight - HalfSize(size)) / num2;
            if (num2 < 0)
            {
                vector.y = HalfSize(size);
                num4 *= -1;
            }
            else
            {
                vector.y = mCamera.pixelHeight - HalfSize(size);
            }
            vector.x = MiddleWidth + (num * num4);
            return vector;
        }
        num4 = (MiddleWidth - HalfSize(size)) / num;
        if (num < 0)
        {
            vector.x = HalfSize(size);
            num4 *= -1;
        }
        else
        {
            vector.x = mCamera.pixelWidth - HalfSize(size);
        }
        vector.y = MidlleHeight + (num2 * num4);
        return vector;
    }
    /// <summary>
    /// 
    /// </summary>
    public static float GetScreenSlope
    {
        get
        {
            float s = mCamera.pixelHeight / mCamera.pixelWidth;
            return s;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public static float MidlleHeight
    {
        get
        {
            Camera c = (Camera.main != null) ? Camera.main : Camera.current;
            return c.pixelHeight / 2;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public static float MiddleWidth
    {
        get
        {
            Camera c = (Camera.main != null) ? Camera.main : Camera.current;
            return c.pixelWidth / 2;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static float HalfSize(float s)
    {
        return s / 2;
    }

    /// <summary>
    /// Get the current camera
    /// </summary>
    public static Camera mCamera
    {
        get
        {
            if (Camera.main != null)
            {
                return Camera.main;
            }
            else
            {
                return Camera.current;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static float FixedPositionX(float p,float size,int leght)
    {
        float x = Screen.width - p;
        float mFixed = 0f;
        float d = ((Screen.width - size) / 100f) / 2.175f;
        if (x < (HalfSize(Screen.width) + (size)))
        {
            mFixed = ((p - (size / 2)) - (leght * 2)) - d;
        }
        else 
        {
            mFixed = ((p + size) + 3.5f) + d;
        }
        return mFixed;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static float FixedPositionY(float p, float size)
    {
        float y = Screen.height - p;
        float mFixed = 0f;
        float d = ((Screen.height - size) / 100f) / 2.175f;

        if (y < (HalfSize(Screen.height) + (size)))
        {
            mFixed = (p - (size / 2))  - d;
        }
        else
        {
            mFixed = ((p + size) + 3.5f) + d;
        }
        return mFixed;
    }
    /// <summary>
    /// Determine the adsolute position of target in screen
    /// </summary>
    /// <param name="i"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Vector3 ScreenPosition(int i,Transform t)
    {
        Vector3 screenPos;

        screenPos = bl_HudUtility.mCamera.WorldToScreenPoint(t.position);
        screenPos.x = screenPos.x / bl_HudUtility.mCamera.pixelWidth;
        screenPos.y = screenPos.y / bl_HudUtility.mCamera.pixelHeight;
        screenPos.z = t.position.z;

        return screenPos;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="screenPos"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    public static bool isOnScreen(Vector3 pos, int i,Transform t)
    {

        Vector3 mDirection = t.position - bl_HudUtility.mCamera.transform.position;
        if (Vector3.Dot(mDirection, bl_HudUtility.mCamera.transform.forward) <= 0)
        {
            return false;
        }

        float margen = 0.1f;

        if (pos.x < margen ||
            pos.x > 1 - margen ||
            pos.y < margen ||
            pos.y > 1 - margen)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}