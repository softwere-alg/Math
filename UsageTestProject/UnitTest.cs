using System.Collections;

namespace UsageTestProject;

[TestFixture]
public class Tests
{
    [SetUp]
    public void Setup()
    {
        // cast
    }

    [Test]
    public void AddVV()
    {
        var builder = Vector<float>.Build;

        Vector<float> a = builder.DenseOfArray([0, 1, 2]);
        Vector<float> b = builder.DenseOfArray([3, 4, 5]);

        Vector<float> c = a + b;

        Assert.That(c[0], Is.EqualTo(3.0f));
        Assert.That(c[1], Is.EqualTo(5.0f));
        Assert.That(c[2], Is.EqualTo(7.0f));
    }

    [Test]
    public void AddVS()
    {
        var builder = Vector<float>.Build;

        Vector<float> a = builder.DenseOfArray([0, 1, 2]);

        Vector<float> c = a + 3.0f;
        Vector<float> d = 3.0f + a;

        Assert.That(c[0], Is.EqualTo(3.0f));
        Assert.That(c[1], Is.EqualTo(4.0f));
        Assert.That(c[2], Is.EqualTo(5.0f));
        Assert.That(d[0], Is.EqualTo(3.0f));
        Assert.That(d[1], Is.EqualTo(4.0f));
        Assert.That(d[2], Is.EqualTo(5.0f));
    }

    [Test]
    public void SubVV()
    {
        var builder = Vector<float>.Build;

        Vector<float> a = builder.DenseOfArray([0, 1, 2]);
        Vector<float> b = builder.DenseOfArray([3, 4, 5]);

        Vector<float> c = a - b;

        Assert.That(c[0], Is.EqualTo(-3.0f));
        Assert.That(c[1], Is.EqualTo(-3.0f));
        Assert.That(c[2], Is.EqualTo(-3.0f));
    }

    [Test]
    public void SubVS()
    {
        var builder = Vector<float>.Build;

        Vector<float> a = builder.DenseOfArray([0, 1, 2]);

        Vector<float> c = a - 3.0f;
        Vector<float> d = 3.0f - a;

        Assert.That(c[0], Is.EqualTo(-3.0f));
        Assert.That(c[1], Is.EqualTo(-2.0f));
        Assert.That(c[2], Is.EqualTo(-1.0f));
        Assert.That(d[0], Is.EqualTo(3.0f));
        Assert.That(d[1], Is.EqualTo(2.0f));
        Assert.That(d[2], Is.EqualTo(1.0f));
    }

    [Test]
    public void MultiplyVV()
    {
        var builder = Vector<float>.Build;

        Vector<float> a = builder.DenseOfArray([0, 1, 2]);
        Vector<float> b = builder.DenseOfArray([3, 4, 5]);

        Vector<float> c = a.PointwiseMultiply(b);

        Assert.That(c[0], Is.EqualTo(0.0f));
        Assert.That(c[1], Is.EqualTo(4.0f));
        Assert.That(c[2], Is.EqualTo(10.0f));
    }

    [Test]
    public void MultiplyVS()
    {
        var builder = Vector<float>.Build;

        Vector<float> a = builder.DenseOfArray([0, 1, 2]);

        Vector<float> c = a * 3.0f;
        Vector<float> d = 3.0f * a;

        Assert.That(c[0], Is.EqualTo(0.0f));
        Assert.That(c[1], Is.EqualTo(3.0f));
        Assert.That(c[2], Is.EqualTo(6.0f));
        Assert.That(d[0], Is.EqualTo(0.0f));
        Assert.That(d[1], Is.EqualTo(3.0f));
        Assert.That(d[2], Is.EqualTo(6.0f));
    }

    [Test]
    public void DevideVV()
    {
        var builder = Vector<float>.Build;

        Vector<float> a = builder.DenseOfArray([0, 1, 2]);
        Vector<float> b = builder.DenseOfArray([3, 4, 5]);

        Vector<float> c = a.PointwiseDivide(b);

        Assert.That(c[0], Is.EqualTo(0.0f));
        Assert.That(c[1], Is.EqualTo(1.0f / 4.0f));
        Assert.That(c[2], Is.EqualTo(2.0f / 5.0f));
    }

    [Test]
    public void DevideVS()
    {
        var builder = Vector<float>.Build;

        Vector<float> a = builder.DenseOfArray([1, 2, 3]);

        Vector<float> c = a / 3.0f;
        Vector<float> d = 3.0f / a;

        Assert.That(c[0], Is.EqualTo(1.0f / 3.0f));
        Assert.That(c[1], Is.EqualTo(2.0f / 3.0f));
        Assert.That(c[2], Is.EqualTo(3.0f / 3.0f));
        Assert.That(d[0], Is.EqualTo(3.0f / 1.0f));
        Assert.That(d[1], Is.EqualTo(3.0f / 2.0f));
        Assert.That(d[2], Is.EqualTo(3.0f / 3.0f));
    }

    [Test]
    public void Log10()
    {
        var builder = Vector<float>.Build;

        Vector<float> a = builder.DenseOfArray([1, 2, 3]);

        Vector<float> c = Vector<float>.Log10(a);
        Assert.That(c[0], Is.EqualTo((float)Math.Log10(1.0f)));
        Assert.That(c[1], Is.EqualTo((float)Math.Log10(2.0f)));
        Assert.That(c[2], Is.EqualTo((float)Math.Log10(3.0f)));
    }

    [Test]
    public void Sin()
    {
        var builder = Vector<float>.Build;

        Vector<float> a = builder.DenseOfArray([0, 1, 2]);

        Vector<float> c = Vector<float>.Sin(a);
        Assert.That(c[0], Is.EqualTo((float)Math.Sin(0.0f)));
        Assert.That(c[1], Is.EqualTo((float)Math.Sin(1.0f)));
        Assert.That(c[2], Is.EqualTo((float)Math.Sin(2.0f)));
    }

    [Test]
    public void Cos()
    {
        var builder = Vector<float>.Build;

        Vector<float> a = builder.DenseOfArray([0, 1, 2]);

        Vector<float> c = Vector<float>.Cos(a);
        Assert.That(c[0], Is.EqualTo((float)Math.Cos(0.0f)));
        Assert.That(c[1], Is.EqualTo((float)Math.Cos(1.0f)));
        Assert.That(c[2], Is.EqualTo((float)Math.Cos(2.0f)));
    }

    private Complex32[] _FFT(Complex32[] a)
    {
        Complex32[] result = new Complex32[a.Length];

        for (int i = 0; i < a.Length; i++)
        {
            result[i] = Complex32.Zero;
            for (int j = 0; j < a.Length; j++)
            {
                result[i] += a[j] * Complex32.FromPolarCoordinates(1.0f, -2.0f * (float)Math.PI * i * j / a.Length);
            }
        }

        return result;
    }

    private class Complex32EqualityComparer : IEqualityComparer
    {
        public new bool Equals(object? x, object? y)
        {
            float within = 0.00001f;

            if (x is Complex32 a)
            {
                if (y is Complex32 b)
                {
                    return a.Real - within <= b.Real && b.Real <= a.Real + within &&
                        a.Imaginary - within <= b.Imaginary && b.Imaginary <= a.Imaginary + within;
                }
            }

            return false;
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }

    [Test]
    public void FFT()
    {
        Complex32[] a = [new Complex32(0, 1), new Complex32(2, 3), new Complex32(4, 5), new Complex32(6, 7)];
        Complex32[] c = _FFT(a);

        Fourier.Forward(a, FourierOptions.NoScaling);
        Assert.That(a[0], Is.EqualTo(c[0]).Using(new Complex32EqualityComparer()));
        Assert.That(a[1], Is.EqualTo(c[1]).Using(new Complex32EqualityComparer()));
        Assert.That(a[2], Is.EqualTo(c[2]).Using(new Complex32EqualityComparer()));
        Assert.That(a[3], Is.EqualTo(c[3]).Using(new Complex32EqualityComparer()));
    }

    [Test]
    public void Abs()
    {
        var builder = Vector<Complex32>.Build;

        Vector<Complex32> a = builder.DenseOfArray([new Complex32(0, 1), new Complex32(2, 3), new Complex32(4, 5), new Complex32(6, 7)]);

        Vector<Complex32> c = Vector<Complex32>.Abs(a);
        Assert.That(c[0].Real, Is.EqualTo((float)Math.Sqrt(0 + 1)));
        Assert.That(c[1].Real, Is.EqualTo((float)Math.Sqrt(4 + 9)));
        Assert.That(c[2].Real, Is.EqualTo((float)Math.Sqrt(16 + 25)));
        Assert.That(c[3].Real, Is.EqualTo((float)Math.Sqrt(36 + 49)));
    }
}
