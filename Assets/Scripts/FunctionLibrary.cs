using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public static float Wave(float x, float z, float t)
    {
        return Sin(PI * (x + z + t));
    }

    public static float MultiWave(float x, float z, float t)
    {
        float y = Sin(PI * (x + 0.5f * t));
        y += Sin(2f * PI * (z + t)) * 0.5f;
        y += Sin(PI * (x + z + 0.25f * t));
        // y = y * (2f / 3f); 尽量不使用变量的除法（常量可以）并减少运算次数
        return y * (1f / 2.5f);
    }

    public static float Ripple(float x, float z, float t)
    {
        float d = Sqrt(x * x + z * z);
        float y = Sin(PI * (4f * d - t));
        return y / (1f + 10f * d);
    }

    public delegate float Function(float x, float z, float t);

    public enum FunctionName
    {
        Wave,
        MultiWave,
        Ripple
    }

    static Function[] functions = { Wave, MultiWave, Ripple };

    public static Function GetFunction(FunctionName name)
    {
        return functions[(int)name];
        //enumeration cannot be implicitly cast to an integer
    }
}