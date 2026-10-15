namespace CaveSharpOS.Time.EngineState;

public struct Vector3
{
    public float X, Y, Z;
    public Vector3(float x, float y, float z)
    {
        X = x; Y = y; Z = z;
    }

    public static Vector3 operator +(Vector3 left, Vector3 right) =>
        new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
}
