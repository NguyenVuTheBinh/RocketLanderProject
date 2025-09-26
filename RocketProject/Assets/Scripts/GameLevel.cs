using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform initialCameraTarget;
    [SerializeField] private float initialCameraOrthographicSize;

    public int GetLevelNumber()
    {
        return levelNumber;
    }
    public Vector3 GetSpawnPosition()
    {
        return spawnPosition.position;
    }
    public Transform GetInitialCameraTarget()
    {
        return initialCameraTarget;
    }
    public float GetInitialCameraOrthographicSize()
    {
        return initialCameraOrthographicSize;
    }
}
