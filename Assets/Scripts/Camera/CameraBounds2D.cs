using UnityEngine;

public class CameraBounds2D : MonoBehaviour
{
    public Vector2 scaleBound = new Vector2(1, 1);
    public Vector2 offset;

    [HideInInspector] public Vector2 maxXlimit;
    [HideInInspector] public Vector2 maxYlimit;

    Camera _camera;

    public void Initialize(Camera temp_camera)
    {
        _camera = temp_camera;
        CalculateBounds();
    }

    public void CalculateBounds()
    {
        float cameraHalfWidth = _camera.aspect * _camera.orthographicSize;
        maxXlimit = new Vector2((transform.position.x + offset.x - (scaleBound.x / 2)) + cameraHalfWidth, (transform.position.x + offset.x + (scaleBound.x / 2)) - cameraHalfWidth);
        maxYlimit = new Vector2((transform.position.y + offset.y - (scaleBound.y / 2)) + _camera.orthographicSize, (transform.position.y + offset.y + (scaleBound.y / 2)) - _camera.orthographicSize);
    }

    public Vector3 GetEdgePosition()
    {
        Vector3 edgePosition = transform.position;
        edgePosition.x += (scaleBound.x * 0.5f) + offset.x - (_camera.aspect * _camera.orthographicSize);
        return edgePosition;
    }
}
