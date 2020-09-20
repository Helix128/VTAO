using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
public class VGI_Main : MonoBehaviour
{

    public static void BakeObject(GameObject obj,float rayLength)
    {

        Mesh mesh = obj.GetComponent<MeshFilter>().sharedMesh;
        List<Vector3> vert = new List<Vector3>();
        mesh.GetVertices(vert);
        List<Vector3> norm = new List<Vector3>();
        mesh.GetNormals(norm);
        Color[] colors = new Color[vert.Count];

        var results = new NativeArray<RaycastHit>(mesh.vertexCount, Allocator.TempJob);

        var commands = new NativeArray<RaycastCommand>(mesh.vertexCount, Allocator.TempJob);


        for (int i = 0; i < mesh.vertexCount; i++)
        {
            commands[i] = new RaycastCommand(obj.transform.localToWorldMatrix.MultiplyPoint(vert[i]) + obj.transform.localToWorldMatrix.MultiplyVector(norm[i]) * 0.002f, obj.transform.localToWorldMatrix.MultiplyVector(norm[i]), rayLength);


        }
        JobHandle handle = RaycastCommand.ScheduleBatch(commands, results, 1, default(JobHandle));
        handle.Complete();
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i].collider != null)
            {
                RaycastHit hit = results[i];
             
                    colors[i] = fromValue(Mathf.Clamp01(hit.distance / rayLength));
                
            }
            else
            {
                colors[i] = Color.white;
            }
        }
        commands.Dispose();
        results.Dispose();
        mesh.SetColors(colors);

    }
    public static void BakeObjectAlt(GameObject obj)
    {
        Vector3 sunDir = new Vector3();
        foreach (Light light in FindObjectsOfType<Light>())
        {
            if (light.type == LightType.Directional)
            {
                sunDir = light.transform.forward;
            }
        }
        Mesh mesh = obj.GetComponent<MeshFilter>().mesh;
        List<Vector3> vert = new List<Vector3>();
        mesh.GetVertices(vert);
        List<Vector3> norm = new List<Vector3>();
        mesh.GetNormals(norm);
        Color[] colors = new Color[vert.Count];

        var results = new NativeArray<RaycastHit>(mesh.vertexCount, Allocator.TempJob);

        var commands = new NativeArray<RaycastCommand>(mesh.vertexCount, Allocator.TempJob);


        for (int i = 0; i < mesh.vertexCount; i++)
        {
            commands[i] = new RaycastCommand(obj.transform.localToWorldMatrix.MultiplyPoint(vert[i]) + obj.transform.localToWorldMatrix.MultiplyVector(norm[i]) * 0.001f, Vector3.up, 512f);


        }
        JobHandle handle = RaycastCommand.ScheduleBatch(commands, results, 1, default(JobHandle));
        handle.Complete();
        for (int i = 0; i < results.Length; i++)
        {   
            if (results[i].collider != null)
            {
                colors[i] = fromValue(0.33f);
            }
            else
            {
                colors[i] = Color.white;
            }
        }
        commands.Dispose();
        results.Dispose();
        mesh.SetColors(colors); 

    }
    public static Color fromValue(float value)
    {
        return new Color(value, value, value, 1);
    }
    public static bool isPointLit(Vector3 point)
    {
        Vector3 sunDir = new Vector3();
        foreach (Light light in FindObjectsOfType<Light>())
        {
            if (light.type == LightType.Directional)
            {
                sunDir = light.transform.forward;
            }
        }
     
        return !Physics.Raycast(point, -sunDir);

    }
    public static Color RayToColor(RaycastHit hit)
    {
        if (hit.collider == null)
            return Color.clear;


        MeshRenderer rend = hit.collider.GetComponent<MeshRenderer>();
        Texture2D texture2D = rend.sharedMaterial.mainTexture as Texture2D;
        Color pixelColor;
        if (texture2D != null)
        {
            Vector2 pixelUV = hit.textureCoord;
            pixelUV.x *= texture2D.width;
            pixelUV.y *= texture2D.height;
            Vector2 tiling = rend.sharedMaterial.mainTextureScale;
            pixelColor = Color.white;

            if (texture2D.isReadable)
            {   
                pixelColor = texture2D.GetPixel((int)pixelUV.x * (int)tiling.x, (int)pixelUV.y * (int)tiling.y) * rend.sharedMaterial.color;
                return pixelColor;
            }
            else
            {
                pixelColor = pixelColor = rend.sharedMaterial.color;
                return pixelColor;
            }
        }
        else
        {

            pixelColor = rend.sharedMaterial.color;
            return pixelColor;
        }

    }

}
