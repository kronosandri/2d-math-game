using UnityEngine;

public static class MathGenerator
{
    public static (string problem, string answer) GenerateProblem()
    {
        int type = Random.Range(0, 4); // Picks 0, 1, 2, or 3

        switch (type)
        {
            case 0: return GenerateLinearEquation();
            case 1: return GeneratePythagorean();
            case 2: return GenerateQuadratic();
            case 3: return GenerateTriangleArea();
            default: return ("1 + 1 = ?", "2");
        }
    }

    private static (string, string) GenerateLinearEquation()
    {
        // Solving for x: ax + b = c  =>  x = (c - b) / a
        int a = Random.Range(2, 10);
        int b = Random.Range(1, 20);
        int x = Random.Range(1, 10);
        int c = (a * x) + b;
        Debug.Log(x);
        return ($"Solve for x: {a}x + {b} = {c}", x.ToString());
    }

    private static (string, string) GeneratePythagorean()
    {
        // a^2 + b^2 = c^2. Using common triples like 3,4,5 or 6,8,10
        int[] aValues = { 3, 6, 5 };
        int[] bValues = { 4, 8, 12 };
        int i = Random.Range(0, 3);
        int a = aValues[i];
        int b = bValues[i];
        double c = System.Math.Sqrt((a * a) + (b * b));
        Debug.Log(c);
        return ($"Find hypotenuse: a={a}, b={b}. c = ?", c.ToString("F0"));
    }

    private static (string, string) GenerateQuadratic()
    {
        // Simple case: (x - a)(x - b) = x^2 - (a+b)x + (ab) = 0
        int a = Random.Range(1, 5);
        int b = Random.Range(1, 5);
        int sum = a + b;
        int prod = a * b;
        Debug.Log(a);
        return ($"Find the smaller root of x^2 - {sum}x + {prod} = 0", a.ToString());
    }

private static (string, string) GenerateTriangleArea()
{
    int b = Random.Range(2, 20);
    int h = Random.Range(2, 20);
    double area = 0.5 * b * h;
    
    // Force "F1" or "F0" so the string is always clean (e.g., "136" or "136.0")
    // Let's use "G" which removes unnecessary trailing zeros
    Debug.Log(area);
    return ($"Area of triangle: base={b}, height={h}. Area = ?", area.ToString("G"));
}
}