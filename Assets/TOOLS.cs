using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class TOOLS
{
    public static char decimalseparator = ' ';

    public const char sep10 = '\n' ;//'+'; //aggrégation des messages dans LE message "getAllChangesFromBeginning"
    public const char sep15 = '£';
    public const char sep20 = '#';
    public const char sep30 = '|';
    public const char sep40 = '\t';

    static System.Globalization.CultureInfo ci = (System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture.Clone();

    #region xxx ToString()
    public static string TransformToString(Transform t)
    {
        return Vector3ToString(t.position) + sep30 +
               QuaternionToString(t.rotation) + sep30 +
               Vector3ToString(t.localScale);
    }
    public static string Vector3ToString(Vector3 v)
    {
        return v.x.ToString() + sep40 +
               v.y.ToString() + sep40 +
               v.z.ToString();
    }
    public static string QuaternionToString(Quaternion q)
    {
        return q.x.ToString() + sep40 +
               q.y.ToString() + sep40 +
               q.z.ToString() + sep40 +
               q.w.ToString();
    }
    public static string ColorToString(Color c)
    {
        return c.r.ToString() + sep40 +
               c.g.ToString() + sep40 +
               c.b.ToString() + sep40 +
               c.a.ToString() + sep40;
    }

    public static string GameObjectPathToString(GameObject g)
    {
        string path = g.name;
        Transform parent = g.transform.parent;
        while (parent != null)
        {
            path = parent.name + TOOLS.sep30 + path;
            parent = parent.parent;
        }
        return path;
    }
    #endregion

    #region string ToXXX()
    public struct Transform_JJ
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 localScale;
    }
    public static Transform_JJ StringToTransform(string data)
    {
        string[] tr = data.Split(TOOLS.sep30);
        return new Transform_JJ
        {
            position = TOOLS.StringToVector3(tr[0]),
            rotation = TOOLS.StringToQuaternion(tr[1]),
            localScale = TOOLS.StringToVector3(tr[2])
        };
    }

    public static Transform_JJ GameObjectToTransform_JJ(GameObject go)
    {
        return new Transform_JJ
        {
            position = go.transform.position,
            rotation = go.transform.rotation,
            localScale = go.transform.localScale
        };
    }

    public static void ApplyAboluteTransform_JJ_TOThisGameObject(Transform_JJ tjj, GameObject go)
    {
        go.transform.position = tjj.position;
        go.transform.rotation = tjj.rotation;
        go.transform.localScale = tjj.localScale;
    }

    public static void ApplyRelativeTransform_JJ_TOThisGameObject(Transform_JJ tjj, GameObject go)
    {
        go.transform.localPosition = tjj.position;
        go.transform.localRotation = tjj.rotation;
        go.transform.localScale = tjj.localScale;
    }

    public static Vector3 StringToVector3(string data)
    {
        string[] vals = data.Split(sep40);
        float x = StringToFloat(vals[0]);
        float y = StringToFloat(vals[1]);
        float z = StringToFloat(vals[2]);
        return new Vector3(x, y, z);
    }
    public static Quaternion StringToQuaternion(string data)
    {
        string[] vals = data.Split(sep40);
        float x = StringToFloat(vals[0]);
        float y = StringToFloat(vals[1]);
        float z = StringToFloat(vals[2]);
        float w = StringToFloat(vals[3]);
        return new Quaternion(x, y, z, w);
    }
    public static Color StringToColor(string data)
    {
        string[] vals = data.Split(sep40);
        float r = StringToFloat(vals[0]);
        float g = StringToFloat(vals[1]);
        float b = StringToFloat(vals[2]);
        float a = StringToFloat(vals[3]);
        return new Color(r, g, b, a);
    }
    public static GameObject StringPathToGameObject(string data)
    {
        return GameObject.Find('/' + data.Replace(TOOLS.sep30, '/'));
    }
    #endregion

    public static float StringToFloat(string data)
    {
        float val;
        ci.NumberFormat.CurrencyDecimalSeparator = ","; //"."
        data = data.Replace(".", ci.NumberFormat.CurrencyDecimalSeparator);
        if (!float.TryParse(data, System.Globalization.NumberStyles.Any, ci, out val))
            Debug.Log("ARG TryParse n'a pas marché : " + data);
        return val;
    }
}