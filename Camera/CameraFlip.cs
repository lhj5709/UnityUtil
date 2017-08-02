using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlip : MonoBehaviour {

    public Camera camera;


    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void OnPreCull()
    {
        camera.ResetProjectionMatrix();
        camera.projectionMatrix = camera.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
    }
    public void OnPreRender()
    {
        GL.invertCulling = true;
    }
    public void OnPostRender()
    {
        GL.invertCulling = false;
    }
}
