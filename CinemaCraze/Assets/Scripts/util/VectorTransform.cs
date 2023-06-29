using UnityEngine;
public class VectorTransform
{
    public static Vector2 ToVec2XZ(Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.z);
    }

}