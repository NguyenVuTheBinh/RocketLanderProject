using Unity.Cinemachine;
using UnityEngine;

public class CinemachineCameraZoom2D : MonoBehaviour
{
    private const float NORMAL_ORTHOGRAPHIC_SIZE = 10f;
    public static CinemachineCameraZoom2D Instance { get; private set; }

    [SerializeField] private CinemachineCamera cinemacineCamera;

    private float targetCameraOrthographicSize;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        float zoomSpeed = 2f;
        cinemacineCamera.Lens.OrthographicSize = Mathf.Lerp(cinemacineCamera.Lens.OrthographicSize,
            targetCameraOrthographicSize, zoomSpeed * Time.deltaTime);
    }

    public void SetTargetCameraOrthographicSize(float targetCameraOrthographicSize)
    {
        this.targetCameraOrthographicSize = targetCameraOrthographicSize;
    }

    public void SetNormalOrthographicSize()
    {
        SetTargetCameraOrthographicSize(NORMAL_ORTHOGRAPHIC_SIZE);
    }
}
