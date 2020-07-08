using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

    [ExecuteInEditMode]
public class CopyTransformRunTimeToPasteInUnity : MonoBehaviour
{
    [SerializeField] bool forceWrite, forceRead;


    // Start is called before the first frame update
    void Start()
    {
        if (Application.isPlaying)
        {
            SAVE();
        }
        else
        {
            LOAD();
        }
//#if UNITY_EDITOR
//        
//#else
//            
//#endif
    }

    string path()
    {
        DirectoryInfo di = new DirectoryInfo(Application.dataPath);
        string chemin = di.FullName + "//test.txt";
        return chemin;
    }
    
    void SAVE()
    {
        FILE(true);
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path(), true);

        string MSG = "";//Hierarchy et Transform
        MSG = TOOLS.GameObjectPathToString(gameObject);
        MSG += TOOLS.sep20;
        MSG += TOOLS.TransformToString(gameObject.transform);
        writer.WriteLine(MSG);
        writer.Close();
        Debug.Log("SAVE OK");
    }
    void LOAD()
    {
        if (!FILE(false))
            return;

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path());

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            Debug.Log(line);
            string[] MSG = line.Split(TOOLS.sep20);
            GameObject go = TOOLS.StringPathToGameObject(MSG[0]);
            if (go != null)
                if (go == gameObject)
                    TOOLS.ApplyAboluteTransform_JJ_TOThisGameObject(TOOLS.StringToTransform(MSG[1]), gameObject);
        }
        reader.Close();
        Debug.Log("READ OK");
    }
    
    bool FILE(bool create)
    {
        FileInfo fi = new FileInfo(path());
        if (!fi.Exists && create)
        {
            StreamWriter writer = new StreamWriter(path(), true);
            writer.WriteLine("Test");
            writer.Close();

//            File.CreateText(path()); 
        }    
        return fi.Exists;
    }

    void Update()
    {
        if (forceRead)
        {
            LOAD();
            forceRead = false;
        }
        if (forceWrite)
        {
            SAVE();
            forceWrite = false;
        }
    }
}
