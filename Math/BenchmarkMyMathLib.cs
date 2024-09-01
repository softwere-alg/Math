using System.Runtime.CompilerServices;
using MyMathLibBinding;

namespace Math;

public static unsafe class BenchmarkMyMathLib
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

    private static long BenchmarkProcess<T>(
        List<T[]> input1,
        List<T[]> input2,
        List<T> input3,
        List<T[]> output,
        Action<T[], T[], T, T[]> action) where T : struct, IEquatable<T>, IFormattable
    {
        var sw = new System.Diagnostics.Stopwatch();

        sw.Start();

        for (int i = 0; i < BenchmarkCount; i++)
        {
            action(input1[i], input2[i], input3[i], output[i]);
        }

        sw.Stop();

        return sw.ElapsedMilliseconds;
    }

    public static long AddVVBenchmark()
    {
        List<float[]> input1 = new List<float[]>();
        List<float[]> input2 = new List<float[]>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<float[]> output = new List<float[]>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomList<float>(VectorSize).ToArray());
            input2.Add(CreateRandomList<float>(VectorSize).ToArray());
            output.Add(CreateZeroList<float>(VectorSize).ToArray());
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1) =>
        {
            fixed (float* i1 = in1)
            {
                fixed (float* i2 = in2)
                {
                    fixed (float* o1 = out1)
                    {
                        vDSPWrapper.VAdd(i1, i2, o1, in1.Length);
                    }
                }
            }
        });
    }

    public static long AddVSBenchmark()
    {
        List<float[]> input1 = new List<float[]>();
        List<float[]> input2 = new List<float[]>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<float[]> output = new List<float[]>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomList<float>(VectorSize).ToArray());
            input2.Add(CreateRandomList<float>(VectorSize).ToArray());
            output.Add(CreateZeroList<float>(VectorSize).ToArray());
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1) =>
        {
            fixed (float* i1 = in1)
            {
                fixed (float* i2 = in2)
                {
                    fixed (float* o1 = out1)
                    {
                        vDSPWrapper.VSAdd(i1, in3, o1, in1.Length);
                    }
                }
            }
        });
    }

    public static long SubVVBenchmark()
    {
        List<float[]> input1 = new List<float[]>();
        List<float[]> input2 = new List<float[]>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<float[]> output = new List<float[]>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomList<float>(VectorSize).ToArray());
            input2.Add(CreateRandomList<float>(VectorSize).ToArray());
            output.Add(CreateZeroList<float>(VectorSize).ToArray());
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1) =>
        {
            fixed (float* i1 = in1)
            {
                fixed (float* i2 = in2)
                {
                    fixed (float* o1 = out1)
                    {
                        vDSPWrapper.VSub(i1, i2, o1, in1.Length);
                    }
                }
            }
        });
    }

    public static long SubVSBenchmark()
    {
        List<float[]> input1 = new List<float[]>();
        List<float[]> input2 = new List<float[]>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<float[]> output = new List<float[]>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomList<float>(VectorSize).ToArray());
            input2.Add(CreateRandomList<float>(VectorSize).ToArray());
            output.Add(CreateZeroList<float>(VectorSize).ToArray());
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1) =>
        {
            fixed (float* i1 = in1)
            {
                fixed (float* i2 = in2)
                {
                    fixed (float* o1 = out1)
                    {
                        vDSPWrapper.VSSub(i1, in3, o1, in1.Length);
                    }
                }
            }
        });
    }

    public static long MultiplyVVBenchmark()
    {
        List<float[]> input1 = new List<float[]>();
        List<float[]> input2 = new List<float[]>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<float[]> output = new List<float[]>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomList<float>(VectorSize).ToArray());
            input2.Add(CreateRandomList<float>(VectorSize).ToArray());
            output.Add(CreateZeroList<float>(VectorSize).ToArray());
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1) =>
        {
            fixed (float* i1 = in1)
            {
                fixed (float* i2 = in2)
                {
                    fixed (float* o1 = out1)
                    {
                        vDSPWrapper.VMul(i1, i2, o1, in1.Length);
                    }
                }
            }
        });
    }

    public static long MultiplyVSBenchmark()
    {
        List<float[]> input1 = new List<float[]>();
        List<float[]> input2 = new List<float[]>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<float[]> output = new List<float[]>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomList<float>(VectorSize).ToArray());
            input2.Add(CreateRandomList<float>(VectorSize).ToArray());
            output.Add(CreateZeroList<float>(VectorSize).ToArray());
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1) =>
        {
            fixed (float* i1 = in1)
            {
                fixed (float* i2 = in2)
                {
                    fixed (float* o1 = out1)
                    {
                        vDSPWrapper.VSMul(i1, in3, o1, in1.Length);
                    }
                }
            }
        });
    }

    public static long DevideVVBenchmark()
    {
        List<float[]> input1 = new List<float[]>();
        List<float[]> input2 = new List<float[]>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<float[]> output = new List<float[]>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomList<float>(VectorSize).ToArray());
            input2.Add(CreateRandomList<float>(VectorSize, nonZero: true).ToArray());
            output.Add(CreateZeroList<float>(VectorSize).ToArray());
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1) =>
        {
            fixed (float* i1 = in1)
            {
                fixed (float* i2 = in2)
                {
                    fixed (float* o1 = out1)
                    {
                        vDSPWrapper.VDiv(i1, i2, o1, in1.Length);
                    }
                }
            }
        });
    }

    public static long DevideVSBenchmark()
    {
        List<float[]> input1 = new List<float[]>();
        List<float[]> input2 = new List<float[]>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount, nonZero: true);
        List<float[]> output = new List<float[]>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomList<float>(VectorSize).ToArray());
            input2.Add(CreateRandomList<float>(VectorSize).ToArray());
            output.Add(CreateZeroList<float>(VectorSize).ToArray());
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1) =>
        {
            fixed (float* i1 = in1)
            {
                fixed (float* i2 = in2)
                {
                    fixed (float* o1 = out1)
                    {
                        vDSPWrapper.VSDiv(i1, in3, o1, in1.Length);
                    }
                }
            }
        });
    }

    public static long Log10Benchmark()
    {
        List<float[]> input1 = new List<float[]>();
        List<float[]> input2 = new List<float[]>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<float[]> output = new List<float[]>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomList<float>(VectorSize, positive: true).ToArray());
            input2.Add(CreateRandomList<float>(VectorSize).ToArray());
            output.Add(CreateZeroList<float>(VectorSize).ToArray());
        };

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1) =>
        {
            fixed (float* i1 = in1)
            {
                fixed (float* i2 = in2)
                {
                    fixed (float* o1 = out1)
                    {
                        vDSPWrapper.VLog10(i1, o1, in1.Length);
                    }
                }
            }
        });
    }

    public static long SinBenchmark()
    {
        List<float[]> input1 = new List<float[]>();
        List<float[]> input2 = new List<float[]>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<float[]> output = new List<float[]>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomList<float>(VectorSize).ToArray());
            input2.Add(CreateRandomList<float>(VectorSize).ToArray());
            output.Add(CreateZeroList<float>(VectorSize).ToArray());
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1) =>
        {
            fixed (float* i1 = in1)
            {
                fixed (float* i2 = in2)
                {
                    fixed (float* o1 = out1)
                    {
                        vDSPWrapper.VSin(i1, o1, in1.Length);
                    }
                }
            }
        });
    }

    public static long CosBenchmark()
    {
        List<float[]> input1 = new List<float[]>();
        List<float[]> input2 = new List<float[]>();
        List<float> input3 = CreateRandomList<float>(BenchmarkCount);
        List<float[]> output = new List<float[]>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            input1.Add(CreateRandomList<float>(VectorSize).ToArray());
            input2.Add(CreateRandomList<float>(VectorSize).ToArray());
            output.Add(CreateZeroList<float>(VectorSize).ToArray());
        }

        return BenchmarkProcess(input1, input2, input3, output, (in1, in2, in3, out1) =>
        {
            fixed (float* i1 = in1)
            {
                fixed (float* i2 = in2)
                {
                    fixed (float* o1 = out1)
                    {
                        vDSPWrapper.VCos(i1, o1, in1.Length);
                    }
                }
            }
        });
    }

    public static long FFTBenchmark()
    {
        List<float[]> inputReal = new List<float[]>();
        List<float[]> inputImag = new List<float[]>();
        List<float[]> outputReal = new List<float[]>();
        List<float[]> outputImag = new List<float[]>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            inputReal.Add(CreateRandomList<float>(VectorSize).ToArray());
            inputImag.Add(CreateRandomList<float>(VectorSize).ToArray());
            outputReal.Add(CreateZeroList<float>(VectorSize).ToArray());
            outputImag.Add(CreateZeroList<float>(VectorSize).ToArray());
        }

        var sw = new System.Diagnostics.Stopwatch();

        sw.Start();

        for (int i = 0; i < BenchmarkCount; i++)
        {
            fixed (float* iR = inputReal[i])
            {
                fixed (float* iI = inputImag[i])
                {
                    fixed (float* oR = outputReal[i])
                    {
                        fixed (float* oI = outputImag[i])
                        {
                            vDSPWrapper.VFFT(iR, iI, oR, oI, VectorSize);
                        }
                    }
                }
            }
        }

        sw.Stop();

        return sw.ElapsedMilliseconds;
    }

    public static long AbsBenchmark()
    {
        List<float[]> inputReal = new List<float[]>();
        List<float[]> inputImag = new List<float[]>();
        List<float[]> output = new List<float[]>();
        for (int i = 0; i < BenchmarkCount; i++)
        {
            inputReal.Add(CreateRandomList<float>(VectorSize).ToArray());
            inputImag.Add(CreateRandomList<float>(VectorSize).ToArray());
            output.Add(CreateZeroList<float>(VectorSize).ToArray());
        }

        var sw = new System.Diagnostics.Stopwatch();

        sw.Start();

        for (int i = 0; i < BenchmarkCount; i++)
        {
            fixed (float* iR = inputReal[i])
            {
                fixed (float* iI = inputImag[i])
                {
                    fixed (float* o = output[i])
                    {
                        vDSPWrapper.VAbs(iR, iI, o, VectorSize);
                    }
                }
            }
        }

        sw.Stop();

        return sw.ElapsedMilliseconds;
    }
}
