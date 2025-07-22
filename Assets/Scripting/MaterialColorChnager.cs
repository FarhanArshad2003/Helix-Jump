using UnityEngine;

public class MaterialColorChanger : MonoBehaviour
{
    public Material specificMaterial; 
    public Color newColor;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        
        renderer.material = specificMaterial;

      
        renderer.material.color = newColor;
    }
}
