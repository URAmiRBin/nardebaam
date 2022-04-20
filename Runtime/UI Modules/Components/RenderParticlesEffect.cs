using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class RenderParticlesEffect : MonoBehaviour {
    [SerializeField] private Camera particlesCamera;
    [SerializeField] private Vector2Int imageResolution = new Vector2Int(256, 256);
    [SerializeField] private RawImage targetImage;

    private RenderTexture renderTexture;

    private void Awake() {
        if (!particlesCamera) particlesCamera = GetComponent<Camera>();

        renderTexture = new RenderTexture(imageResolution.x, imageResolution.y, 32);
        particlesCamera.targetTexture = renderTexture;

        targetImage.texture = renderTexture;
    }
}