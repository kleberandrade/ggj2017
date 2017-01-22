using UnityEngine;

public class Helper : MonoBehaviour {

    /// <summary>
    /// Convert transform component to direction in 2D
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static Vector3 TransformToDirection2D(Transform transform)
    {
        float angle = transform.eulerAngles.z;
        float x = Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector3 direction = new Vector3(x, y, 0.0f);
        return direction;
    } 
}
