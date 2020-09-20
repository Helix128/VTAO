using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoRealtimeGI : MonoBehaviour
{
    public float updateRate;
    float timer;
    MeshFilter[] meshList;
    private void Start()
    {
        meshList = FindObjectsOfType<MeshFilter>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {

            timer = updateRate;
            for (int i = 0; i < meshList.Length; i++)
            {
                VGI_Main.BakeObject(meshList[i].gameObject,1f);
            }
        }
    }
}
