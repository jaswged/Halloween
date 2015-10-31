using UnityEngine;
using System.Collections;

public class PumpkinRender : MonoBehaviour {
    public Material greenPumpkinMaterial;
    public Material bluePumpkinMaterial;
    public Material defaultPumpkinMaterial;
    public Material redPumpkinMaterial;

    private MeshRenderer pumpkinMeshRenderer;

    public void ResetRenderer() {
        pumpkinMeshRenderer.material = defaultPumpkinMaterial;
    }

    public void ChangeColor(PumpkinColors color) {
        pumpkinMeshRenderer = GetComponent<MeshRenderer>();
        if (pumpkinMeshRenderer == null) {
            Debug.LogWarning("Mesh renderer is null nothing will happen! " + color);
        }
        if (color == PumpkinColors.GreenPumpkin) {
            pumpkinMeshRenderer.material = bluePumpkinMaterial;
        }
        else if (color == PumpkinColors.BluePumpkin) {
            pumpkinMeshRenderer.material = greenPumpkinMaterial;
        }
        else if (color == PumpkinColors.RedPumpkin) {
            pumpkinMeshRenderer.material = redPumpkinMaterial;
        }
        else {
            pumpkinMeshRenderer.material = defaultPumpkinMaterial;
        }
    }

    public enum PumpkinColors { GreenPumpkin, BluePumpkin, Default, RedPumpkin }
}