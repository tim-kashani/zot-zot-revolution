using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceMember : MonoBehaviour
{
    [SerializeField] Material material;

    public void SetScale(float scale)
    {
        transform.localScale = new(1, scale, 1);
    }

    public void SetTexture(Texture2D texture)
    {
        material.SetTexture("_Texture2D", texture);
    }
}
