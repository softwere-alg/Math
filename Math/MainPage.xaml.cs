using System.Collections.ObjectModel;
using System.Reflection;

namespace Math;

public partial class MainPage : ContentPage
{
	public class Result
	{
		public string Calc { get; set; } = "";
		public long Time { get; set; }
	}

	private ObservableCollection<Result> list = new ObservableCollection<Result>();
	private string[] benchmarkTarget = [
		"AddVVBenchmark", "AddVSBenchmark",
		"SubVVBenchmark", "SubVSBenchmark",
		"MultiplyVVBenchmark", "MultiplyVSBenchmark",
		"DevideVVBenchmark", "DevideVSBenchmark",
		"Log10Benchmark",
		"SinBenchmark", "CosBenchmark",
		"FFTBenchmark",
		"AbsBenchmark"
	];

	public MainPage()
	{
		InitializeComponent();

		ResultList.ItemsSource = list;
	}

	private async void OnStartClicked(object sender, EventArgs e)
	{
		WaitBack.IsVisible = true;
		WaitIndicator.IsVisible = true;

		list.Clear();

		await Task.Run(() =>
		{
			foreach (var target in benchmarkTarget)
			{
				Type type = typeof(BenchmarkMathNet);
				MethodInfo method = type.GetMethod(target, BindingFlags.Static | BindingFlags.Public);
				long result = Convert.ToInt64(method.Invoke(null, null));

				MainThread.BeginInvokeOnMainThread(() =>
				{
					Console.WriteLine($"{target}: {result}ms");
					list.Add(new Result { Calc = target, Time = result });
				});
			}

			foreach (var target in benchmarkTarget)
			{
				Type type = typeof(BenchmarkMyMathLib);
				MethodInfo method = type.GetMethod(target, BindingFlags.Static | BindingFlags.Public);
				long result = Convert.ToInt64(method.Invoke(null, null));

				MainThread.BeginInvokeOnMainThread(() =>
				{
					Console.WriteLine($"{target}: {result}ms");
					list.Add(new Result { Calc = target, Time = result });
				});
			}
		});

		WaitBack.IsVisible = false;
		WaitIndicator.IsVisible = false;
	}
}

