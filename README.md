## Overview
[Math.Net](https://numerics.mathdotnet.com/)と[Accelerate](https://developer.apple.com/documentation/accelerate)の速度比較を行うためのアプリです。

## Benchmark
要素数128のベクトルまたはスカラーに対して、それぞれ10,000回計算した結果は下記の通りとなりました。
* **R**:実数のベクトル
* **C**:複素数のベクトル
* r:実数（スカラー）

iPhoneSE2を使用して計測しています。

| | Math.Net [ms] | Accelerate [ms] | Accelerate(Native) [ms] |
| ---- | :---: | :---: | :---: |
| **R** + **R** | 13 | 8 | 2.1 |
| **R** + r | 8 | 8 | 2.2 |
| **R** - **R** | 8 | 8 | 1.8 |
| **R** - r | 5 | 8 | 1.6 |
| **R** * **R** | 8 | 8 | 1.4 |
| **R** * r | 7 | 8 | 1.5 |
| **R** / **R** | 9 | 9 | 1.4 |
| **R** / r | 43 | 8 | 1.8 |
| Log10(**R**) | 53 | 10 | 2.3 |
| Sin(**R**) | 53 | 10 | 1.9 |
| Cos(**R**) | 52 | 10 | 2.0 |
| FFT(**C**) | 70 | 14 | 3.4 |
| Abs(**C**) | 61 | 10 | 2.0 |
