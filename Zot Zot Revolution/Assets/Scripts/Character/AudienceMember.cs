using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceMember : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetScale(float scale)
    {
        transform.localScale = new(1, scale, 1);
    }
}
