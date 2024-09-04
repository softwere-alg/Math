//
//  ViewController.swift
//  MyMathLibApp
//
//  Created by 田中祐汰 on 2024/09/03.
//

import UIKit
import MyMathLib

typealias BenchmarkFunc = () -> TimeInterval

func DoBenchmark(benchmarkFunc: BenchmarkFunc) async -> TimeInterval {
    return benchmarkFunc()
}

class ViewController: UIViewController {

    @IBOutlet weak var table: UITableView!
    
    var data: [(String, Double)] = []
    
    let benchmarkSet = [
        ("AddVVBenchmark", Benchmark.AddVVBenchmark),
        ("AddVSBenchmark", Benchmark.AddVSBenchmark),
        ("SubVVBenchmark", Benchmark.SubVVBenchmark),
        ("SubVSBenchmark", Benchmark.SubVSBenchmark),
        ("MultiplyVVBenchmark", Benchmark.MultiplyVVBenchmark),
        ("MultiplyVSBenchmark", Benchmark.MultiplyVSBenchmark),
        ("DevideVVBenchmark", Benchmark.DevideVVBenchmark),
        ("DevideVSBenchmark", Benchmark.DevideVSBenchmark),
        ("Log10Benchmark", Benchmark.Log10Benchmark),
        ("SinBenchmark", Benchmark.SinBenchmark),
        ("CosBenchmark", Benchmark.CosBenchmark),
        ("FFTBenchmark", Benchmark.FFTBenchmark),
        ("AbsBenchmark", Benchmark.AbsBenchmark),
        ("ClampBenchmark", Benchmark.ClampBenchmark)
    ]
    
    override func viewDidLoad() {
        super.viewDidLoad()
    }

    @IBAction func Start(_ sender: Any) {
        let activitiyViewController = ActivityViewController(message: "Benckmark...")
        present(activitiyViewController, animated: true, completion: nil)
        
        data = []
        table.reloadData()
        
        Task {
            for i in 0..<benchmarkSet.count {
                let result = await DoBenchmark(benchmarkFunc: benchmarkSet[i].1)
                
                data.append((benchmarkSet[i].0, result))
                table.reloadData()
            }
            
            dismiss(animated: true)
        }
    }
}

extension ViewController: UITableViewDelegate, UITableViewDataSource {
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        data.count
    }

    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        // セルを取得する
        let cell: UITableViewCell = tableView.dequeueReusableCell(withIdentifier: "cell", for: indexPath)

        // TableViewCellの中に配置したLabelを取得する
        let label1 = cell.contentView.viewWithTag(1) as! UILabel
        let label2 = cell.contentView.viewWithTag(2) as! UILabel

        // Labelにテキストを設定する
        label1.text = data[indexPath.row].0
        label2.text = String(format: "%.2f", data[indexPath.row].1 * 1000)

        return cell
    }
}

