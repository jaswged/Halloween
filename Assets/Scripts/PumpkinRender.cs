using UnityEngine;
using System.Collections;

public class PumpkinRender : MonoBehaviour {
    public Material greenPumpkinMaterial;
    public Material yellowPumpkinMaterial;
    private Material defaultPumpkinMaterial;

    //private Renderer pumpkinRenderer;
    private MeshRenderer pumpkinMeshRenderer;

    void Awake() {
        //pumpkinRenderer = GetComponent<Renderer>();
        pumpkinMeshRenderer = GetComponent<MeshRenderer>();
        //defaultPumpkinMaterial = pumpkinRenderer.material;
    }

    public void ResetRenderer() {
        //pumpkinRenderer.material = defaultPumpkinMaterial;
    }

    public void ChangeColor(PumpkinColors color) {
        /*if(pumpkinRenderer == null) {
           Debug.LogWarning("renderer is null nothing will happen");
        }*/
        if (pumpkinMeshRenderer == null) {
            Debug.LogWarning("Mesh renderer is null nothing will happen! " + color);
        }/*
        if (color == PumpkinColors.GreenPumpkin) {
        Debug.Log("Changing the color to green: " + color);
            pumpkinRenderer.material = greenPumpkinMaterial;
        }
        if (color == PumpkinColors.YellowPumpkin) {
            Debug.Log("Changing the color to yellow: " + color );
            pumpkinRenderer.material = greenPumpkinMaterial;
        }*/
    }

    public enum PumpkinColors { GreenPumpkin, YellowPumpkin  }
}