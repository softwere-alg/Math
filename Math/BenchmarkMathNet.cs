using System.Runtime.CompilerServices;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using MathNet.Numerics.LinearAlgebra;

namespace Math;

public static class BenchmarkMathNet
{
    public static int VectorSize = 128;
    public static int BenchmarkCount = 10000;

    private static T RandomValue<T>() where T : struct, IEquatable<T>, IFormattable
    {
        Random rnd = new Random();

        if (typeof(T) == typeof(float))
        {
            float val = rnd.NextSingle();
            return Unsafe.As<float, T>(ref val);
        }
        else if (typeof(T) == typeof(Complex32))
        {
            Complex32 val = new Complex32(rnd.NextSingle(), rnd.NextSingle());
            return Unsafe.As<Complex32, T>(ref val);
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    private static List<T> CreateRandomList<T>(int n, bool nonZero = false, bool positive = false) where T : struct, IEquatable<T>, IFormattable
    {
        List<T> data = new List<T>();

        for (int i = 0; i < n; i++)
        {
            if (typeof(T) == typeof(float))
            {
                if (positive)
                {
                    float val = RandomValue<float>();
                    while (nonZero && val == 0.0f)
                    {
                        val = RandomValue<float>();
                    }
                    data.Add(Unsafe.As<float, T>(ref val));
                }
                else
                {
                    float val = 2.0f * (RandomValue<float>() - 0.5f);
                    while (nonZero && val == 0.0f)
                    {
                        val = 2.0f * (RandomValue<float>() - 0.5f);
                    }
                    data.Add(Unsafe.As<float, T>(ref val));
                }
            }
            else if (typeof(T) == typeof(Complex32))
            {
                Complex32 val = new Complex32(2.0f * (RandomValue<float>() - 0.5f), 2.0f * (RandomValue<float>() - 0.5f));
                while (nonZero && val == Complex32.Zero)
                {
                    val = val = new Complex32(2.0f * (RandomValue<float>() - 0.5f), 2.0f * (RandomValue<float>() - 0.5f));
                }
                data.Add(Unsafe.As<Complex32, T>(ref val));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        return data;
    }

    private static List<T> CreateZeroList<T>(int n) where T : struct, IEquatable<T>, IFormattable
    {
        List<T> data = new List<T>();

        for (int i = 0; i < n; i++)
        {
            data.Add(default(T));
        }

        return data;
    }

    private static Vector<T> CreateRandomVector<T>(int n, bool nonZero = false, bool positive = false) where T : struct, IEquatable<T>, IFormattable
    {
        return Vector<T>.Build.DenseOfEnumerable(CreateRandomList<T>(n, nonZero, positive));
    }

    private static Vector<T> CreateZeroVector<T>(int n) where T : struct, IEquatable<T>, IFormattable
    {
        return Vector<T>.Build.DenseOfEnumerable(CreateZeroList<T>(n));
    }

    private static long BenchmarkProcess<T>(
        List<Vector<T>> input1,
        List<Vector<T>> input2,
        List<T> input3,
        List<Vector<T>> output,
        Action<Vector<T>, Vector<T>, T, List<Vector<T>>, int> action) where T : struct, IEquatable<T>, IFormattable
    {
        var sw = new System.Diagnostics.Stopwatch();

        sw.Start();

        for (int i = 0; i < BenchmarkCount; i++)
        {
            action(input1[i], input2[i], input3[i], output, i);
        }

        sw.Stop();

        return sw.ElapsedMilliseconds;
    }

    public static long AddVVBenchmark()
    {
        List<Vector<float>> input1 = new List<Vector<float>>();
        List<Vector<float>> input2 = new List<Vector<float>>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<Vector<float>> output = new List<Vector<float>>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomVector<float>(VectorSize));
            input2.Add(CreateRandomVector<float>(VectorSize));
            output.Add(CreateZeroVector<float>(VectorSize));
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1, i) =>
        {
            out1[i] = in1 + in2;
        });
    }

    public static long AddVSBenchmark()
    {
        List<Vector<float>> input1 = new List<Vector<float>>();
        List<Vector<float>> input2 = new List<Vector<float>>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<Vector<float>> output = new List<Vector<float>>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomVector<float>(VectorSize));
            input2.Add(CreateRandomVector<float>(VectorSize));
            output.Add(CreateZeroVector<float>(VectorSize));
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1, i) =>
        {
            out1[i] = in1 + in3;
        });
    }

    public static long SubVVBenchmark()
    {
        List<Vector<float>> input1 = new List<Vector<float>>();
        List<Vector<float>> input2 = new List<Vector<float>>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<Vector<float>> output = new List<Vector<float>>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomVector<float>(VectorSize));
            input2.Add(CreateRandomVector<float>(VectorSize));
            output.Add(CreateZeroVector<float>(VectorSize));
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1, i) =>
        {
            out1[i] = in1 - in2;
        });
    }

    public static long SubVSBenchmark()
    {
        List<Vector<float>> input1 = new List<Vector<float>>();
        List<Vector<float>> input2 = new List<Vector<float>>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<Vector<float>> output = new List<Vector<float>>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomVector<float>(VectorSize));
            input2.Add(CreateRandomVector<float>(VectorSize));
            output.Add(CreateZeroVector<float>(VectorSize));
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1, i) =>
        {
            out1[i] = in1 - in3;
        });
    }

    public static long MultiplyVVBenchmark()
    {
        List<Vector<float>> input1 = new List<Vector<float>>();
        List<Vector<float>> input2 = new List<Vector<float>>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<Vector<float>> output = new List<Vector<float>>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomVector<float>(VectorSize));
            input2.Add(CreateRandomVector<float>(VectorSize));
            output.Add(CreateZeroVector<float>(VectorSize));
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1, i) =>
        {
            out1[i] = in1.PointwiseMultiply(in2);
        });
    }

    public static long MultiplyVSBenchmark()
    {
        List<Vector<float>> input1 = new List<Vector<float>>();
        List<Vector<float>> input2 = new List<Vector<float>>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<Vector<float>> output = new List<Vector<float>>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomVector<float>(VectorSize));
            input2.Add(CreateRandomVector<float>(VectorSize));
            output.Add(CreateZeroVector<float>(VectorSize));
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1, i) =>
        {
            out1[i] = in1 * in3;
        });
    }

    public static long DevideVVBenchmark()
    {
        List<Vector<float>> input1 = new List<Vector<float>>();
        List<Vector<float>> input2 = new List<Vector<float>>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<Vector<float>> output = new List<Vector<float>>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomVector<float>(VectorSize));
            input2.Add(CreateRandomVector<float>(VectorSize, nonZero: true));
            output.Add(CreateZeroVector<float>(VectorSize));
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1, i) =>
        {
            out1[i] = in1.PointwiseDivide(in2);
        });
    }

    public static long DevideVSBenchmark()
    {
        List<Vector<float>> input1 = new List<Vector<float>>();
        List<Vector<float>> input2 = new List<Vector<float>>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount, nonZero: true);
        List<Vector<float>> output = new List<Vector<float>>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomVector<float>(VectorSize));
            input2.Add(CreateRandomVector<float>(VectorSize));
            output.Add(CreateZeroVector<float>(VectorSize));
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1, i) =>
        {
            out1[i] = in1 / in3;
        });
    }

    public static long Log10Benchmark()
    {
        List<Vector<float>> input1 = new List<Vector<float>>();
        List<Vector<float>> input2 = new List<Vector<float>>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<Vector<float>> output = new List<Vector<float>>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomVector<float>(VectorSize, positive: true));
            input2.Add(CreateRandomVector<float>(VectorSize));
            output.Add(CreateZeroVector<float>(VectorSize));
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1, i) =>
        {
            out1[i] = Vector<float>.Log10(in1);
        });
    }

    public static long SinBenchmark()
    {
        List<Vector<float>> input1 = new List<Vector<float>>();
        List<Vector<float>> input2 = new List<Vector<float>>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<Vector<float>> output = new List<Vector<float>>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomVector<float>(VectorSize));
            input2.Add(CreateRandomVector<float>(VectorSize));
            output.Add(CreateZeroVector<float>(VectorSize));
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1, i) =>
        {
            out1[i] = Vector<float>.Sin(in1);
        });
    }

    public static long CosBenchmark()
    {
        List<Vector<float>> input1 = new List<Vector<float>>();
        List<Vector<float>> input2 = new List<Vector<float>>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<Vector<float>> output = new List<Vector<float>>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomVector<float>(VectorSize));
            input2.Add(CreateRandomVector<float>(VectorSize));
            output.Add(CreateZeroVector<float>(VectorSize));
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1, i) =>
        {
            out1[i] = Vector<float>.Cos(in1);
        });
    }

    public static long FFTBenchmark()
    {
        List<Complex32[]> input = new List<Complex32[]>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input.Add(CreateRandomList<Complex32>(VectorSize).ToArray());
        }

        var sw = new System.Diagnostics.Stopwatch();

        sw.Start();

        for (int i = 0; i < BenchmarkCount; i++)
        {
            Fourier.Forward(input[i], FourierOptions.NoScaling);
        }

        sw.Stop();

        return sw.ElapsedMilliseconds;
    }

    public static long AbsBenchmark()
    {
        List<Vector<Complex32>> input1 = new List<Vector<Complex32>>();
        List<Vector<Complex32>> input2 = new List<Vector<Complex32>>();
        List<Complex32> input3 = CreateRandomList<Complex32>(BenchmarkCount);
        List<Vector<Complex32>> output = new List<Vector<Complex32>>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomVector<Complex32>(VectorSize));
            input2.Add(CreateRandomVector<Complex32>(VectorSize));
            output.Add(CreateZeroVector<Complex32>(VectorSize));
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1, i) =>
        {
            out1[i] = Vector<Complex32>.Abs(in2);
        });
    }

    public static long ClampBenchmark()
    {
        List<Vector<float>> input1 = new List<Vector<float>>();
        List<Vector<float>> input2 = new List<Vector<float>>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<Vector<float>> output = new List<Vector<float>>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            // Clamp用に-500~500のランダムデータ作成
            input1.Add(CreateRandomVector<float>(VectorSize) * 500);
            input2.Add(CreateRandomVector<float>(VectorSize));
            output.Add(CreateZeroVector<float>(VectorSize));
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1, i) =>
        {
            out1[i] = in1.Clamp(0.0f, 255.0f);
        });
    }
}

public static class SingleVectorExtension
{
    public static Vector<float> Clamp(this Vector<float> v, float min, float max)
    {
        return v.Map((float value) => {
            return System.Math.Min(System.Math.Max(min, value), max);
        });
    }
}
