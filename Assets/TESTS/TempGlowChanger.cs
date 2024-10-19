using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGlowChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Material material = GetComponent<SpriteRenderer>().material;
        material.SetColor("EdgeColor", Color.green);
        material.SetFloat("EdgeAmount", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
