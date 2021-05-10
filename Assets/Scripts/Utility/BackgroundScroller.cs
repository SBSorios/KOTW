using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(-1, 1)]
    public float scrollSpeed = 0.5f;
    private float offset;
    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offset += (Time.deltaTime * scrollSpeed / 10);
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
