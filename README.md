## Overview
[Math.Net](https://numerics.mathdotnet.com/)と[Accelerate](https://developer.apple.com/documentation/accelerate)の速度比較を行うためのアプリです。

## Benchmark
要素数128のベクトルまたはスカラーに対して、それぞれ10,000回計算した結果は下記の通りとなりました。
* **R**:実数のベクトル
* **C**:複素数のベクトル
* r:実数（スカラー）

iPhoneSE2を使用して計測しています。

| | Math.Net [ms] | Accelerate [ms] |
| ---- | :---: | :---: |
| **R** + **R** | 13 | 8 |
| **R** + r | 8 | 8 |
| **R** - **R** | 8 | 8 |
| **R** - r | 5 | 8 |
| **R** * **R** | 8 | 8 |
| **R** * r | 7 | 8 |
| **R** / **R** | 9 | 9 |
| **R** / r | 43 | 8 |
| Log10(**R**) | 53 | 10 |
| Sin(**R**) | 53 | 10 |
| Cos(**R**) | 52 | 10 |
| FFT(**C**) | 70 | 14 |
| Abs(**C**) | 61 | 10 |
