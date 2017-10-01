using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRender : MonoBehaviour {


    public UITexture outputUITexture;

    public Texture inputTexture;

    public RenderTexture renderTexture;

    public Shader RenderShader;

    public Rect uv = new Rect(0, 0, 1.0f, 0.5f);


    public Texture2D textureMadeByCode;

    public int _UVRoll = 0;

    // Use this for initialization
    void Start () {
        renderTexture = RenderTexture.GetTemporary(1024, 1024);
        outputUITexture.mainTexture = renderTexture;

        makeTexture();
    }

    private void makeTexture()
    {
        textureMadeByCode = new Texture2D(256, 256, TextureFormat.ARGB32, mipmap: false);
        /*Color32[] colorArray = new Color32[1024 * 1024];
        for (int i = 0; i < colorArray.Length; i++)
        {
            colorArray[i] = new Color32(0,0,0,255);
        }*/

        for (int x = 0; x < 128; x++)
        {
            for (int y = 0; y < 256; y++)
            {
                //colorArray[x + y * 1024] = new Color32(255, 0, 0, 255);
                textureMadeByCode.SetPixel(x, y, new Color32((byte)y, (byte)y, (byte)(x + 128), 255));
            }
        }

        for (int x = 128; x < 256; x++)
        {
            for (int y = 0; y < 256; y++)
            {
                textureMadeByCode.SetPixel(x, y, new Color32((byte)y, (byte)(255 - y), (byte)(x * 2), 255));
            }
        }


        //textureMadeByCode.SetPixels32(colorArray);
        textureMadeByCode.Apply();

    }

    void Update () {
        // Remember currently active render texture
        RenderTexture currentActiveRT = RenderTexture.active;

        // Set the supplied RenderTexture as the active one
        RenderTexture.active = renderTexture;

        Graphics.Blit(Texture2D.whiteTexture, renderTexture);
        //Graphics.Blit(textureMadeByCode, renderTexture);

        // Create a new Texture2D and read the RenderTexture image into it
        //Texture2D tex = new Texture2D(rt.width, rt.height);
        //tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

        var material = new Material(RenderShader);


        var center = new Vector4(uv.x + uv.width / 2, uv.y + uv.height / 2, 0, 0);

        material.SetVector("_UVCenter", center);
        material.SetInt("_UVRoll", _UVRoll);
        Graphics.DrawTexture(new Rect(0, 0, 256, 128), textureMadeByCode, uv, 0,0,0,0, material);

        // Restorie previously active render texture
        RenderTexture.active = currentActiveRT;
    }
}
