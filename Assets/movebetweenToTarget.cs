using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movebetweenToTarget : MonoBehaviour
{
    [SerializeField] GameObject[] objectifs;
    int currentobjectif;
    GameObject currenttarget;
    [SerializeField] float speed = 0.2f;
    [SerializeField] float rot_speed;
    float angle;
    void Start()
    {
        currentobjectif = 0;
        NextTarget();
    }


    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
                                                    currenttarget.transform.position, 
                                                    speed * Time.fixedDeltaTime);



        transform.rotation = Quaternion.RotateTowards(transform.rotation, currenttarget.transform.rotation, rot_speed);

        float angle = Quaternion.Angle(transform.rotation, currenttarget.transform.rotation);
        float distance = Vector3.Distance(transform.position, currenttarget.transform.position);

        if (distance < 0.01 && angle < 0.1)
            NextTarget();
    }

    private void NextTarget()
    {
        currentobjectif++;
        if (currentobjectif > objectifs.Length - 1)
            currentobjectif = 0;
        currenttarget = objectifs[currentobjectif];
        angle = Quaternion.Angle(transform.rotation, currenttarget.transform.rotation);

        float distance = Vector3.Distance(transform.position, currenttarget.transform.position);
        float tempsestime = distance / speed;
        rot_speed = angle / tempsestime * Time.fixedDeltaTime;
    }
}
