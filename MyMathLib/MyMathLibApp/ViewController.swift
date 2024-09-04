//
//  ViewController.swift
//  MyMathLibApp
//
//  Created by 田中祐汰 on 2024/09/03.
//

import UIKit
import MyMathLib

class ViewController: UIViewController {

    @IBOutlet weak var table: UITableView!
    
    var data: [(String, Double)] = []
    
    override func viewDidLoad() {
        super.viewDidLoad()
    }

    @IBAction func Start(_ sender: Any) {
        data.append(("AddVVBenchmark", Benchmark.AddVVBenchmark()))
        data.append(("AddVSBenchmark", Benchmark.AddVSBenchmark()))
        data.append(("SubVVBenchmark", Benchmark.SubVVBenchmark()))
        data.append(("SubVSBenchmark", Benchmark.SubVSBenchmark()))
        data.append(("MultiplyVVBenchmark", Benchmark.MultiplyVVBenchmark()))
        data.append(("MultiplyVSBenchmark", Benchmark.MultiplyVSBenchmark()))
        data.append(("DevideVVBenchmark", Benchmark.DevideVVBenchmark()))
        data.append(("DevideVSBenchmark", Benchmark.DevideVSBenchmark()))
        data.append(("Log10Benchmark", Benchmark.Log10Benchmark()))
        data.append(("SinBenchmark", Benchmark.SinBenchmark()))
        data.append(("CosBenchmark", Benchmark.CosBenchmark()))
        data.append(("FFTBenchmark", Benchmark.FFTBenchmark()))
        data.append(("AbsBenchmark", Benchmark.AbsBenchmark()))
        data.append(("ClampBenchmark", Benchmark.ClampBenchmark()))
        table.reloadData()
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

