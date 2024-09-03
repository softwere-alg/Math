//
//  Benchmark.swift
//  MyMathLibApp
//
//  Created by 田中祐汰 on 2024/09/03.
//

import Foundation
import MyMathLib

class Benchmark {
    public static let VectorSize = 128
    public static let BenchmarkCount = 10000 * 10
    
    private static func RandomValue() -> Float {
        return Float.random(in:0...1)
    }

    private static func CreateRandomList(n: Int, nonZero: Bool = false, positive: Bool = false) -> [Float] {
        var data: [Float] = []

        for _ in 0..<n {
            if (positive)
            {
                var val = RandomValue()
                while (nonZero && val == 0.0)
                {
                    val = RandomValue()
                }
                data.append(val)
            }
            else
            {
                var val = 2.0 * (RandomValue() - 0.5)
                while (nonZero && val == 0.0)
                {
                    val = 2.0 * (RandomValue() - 0.5)
                }
                data.append(val)
            }
        }

        return data;
    }

    private static func CreateZeroList(n: Int) -> [Float] {
        var data: [Float] = []

        for _ in 0..<n
        {
            data.append(0.0)
        }

        return data;
    }
    
    private static func BenchmarkProcess1(action: (UnsafePointer<Float>, UnsafePointer<Float>, Float, UnsafeMutablePointer<Float>, Int) -> Void) -> TimeInterval {
        var input1: [[Float]] = []
        var input2: [[Float]] = []
        let input3: [Float] = CreateRandomList(n: BenchmarkCount)
        var output: [[Float]] = []
        for _ in 0..<BenchmarkCount {
            input1.append(CreateRandomList(n: VectorSize))
            input2.append(CreateRandomList(n: VectorSize))
            output.append(CreateZeroList(n: VectorSize))
        }
        
        let dt = Date()

        for i in 0..<BenchmarkCount
        {
            input1[i].withUnsafeBufferPointer { inPtr1 in
                input2[i].withUnsafeBufferPointer { inPtr2 in
                    output[i].withUnsafeMutableBufferPointer { outPtr in
                        action(inPtr1.baseAddress!, inPtr2.baseAddress!, input3[i], outPtr.baseAddress!, inPtr1.count)
                    }
                }
            }
        }

        return Date().timeIntervalSince(dt)
    }
    
    private static func BenchmarkProcess2(action: (UnsafePointer<Float>, UnsafePointer<Float>, Float, UnsafeMutablePointer<Float>, Int) -> Void) -> TimeInterval {
        var input1: [[Float]] = []
        var input2: [[Float]] = []
        let input3: [Float] = CreateRandomList(n: BenchmarkCount, nonZero: true)
        var output: [[Float]] = []
        for _ in 0..<BenchmarkCount {
            input1.append(CreateRandomList(n: VectorSize))
            input2.append(CreateRandomList(n: VectorSize, nonZero: true))
            output.append(CreateZeroList(n: VectorSize))
        }
        
        let dt = Date()

        for i in 0..<BenchmarkCount
        {
            input1[i].withUnsafeBufferPointer { inPtr1 in
                input2[i].withUnsafeBufferPointer { inPtr2 in
                    output[i].withUnsafeMutableBufferPointer { outPtr in
                        action(inPtr1.baseAddress!, inPtr2.baseAddress!, input3[i], outPtr.baseAddress!, inPtr1.count)
                    }
                }
            }
        }

        return Date().timeIntervalSince(dt)
    }
    
    private static func BenchmarkProcess3(action: (UnsafePointer<Float>, UnsafePointer<Float>, Float, UnsafeMutablePointer<Float>, Int) -> Void) -> TimeInterval {
        var input1: [[Float]] = []
        var input2: [[Float]] = []
        let input3: [Float] = CreateRandomList(n: BenchmarkCount)
        var output: [[Float]] = []
        for _ in 0..<BenchmarkCount {
            input1.append(CreateRandomList(n: VectorSize, positive: true))
            input2.append(CreateRandomList(n: VectorSize))
            output.append(CreateZeroList(n: VectorSize))
        }
        
        let dt = Date()

        for i in 0..<BenchmarkCount
        {
            input1[i].withUnsafeBufferPointer { inPtr1 in
                input2[i].withUnsafeBufferPointer { inPtr2 in
                    output[i].withUnsafeMutableBufferPointer { outPtr in
                        action(inPtr1.baseAddress!, inPtr2.baseAddress!, input3[i], outPtr.baseAddress!, inPtr1.count)
                    }
                }
            }
        }

        return Date().timeIntervalSince(dt)
    }
    
    private static func BenchmarkProcess4(action: (UnsafePointer<Float>, UnsafePointer<Float>, UnsafeMutablePointer<Float>, UnsafeMutablePointer<Float>, Int) -> Void) -> TimeInterval {
        var inputReal: [[Float]] = []
        var inputImag: [[Float]] = []
        var outputReal: [[Float]] = []
        var outputImag: [[Float]] = []
        for _ in 0..<BenchmarkCount {
            inputReal.append(CreateRandomList(n: VectorSize))
            inputImag.append(CreateRandomList(n: VectorSize))
            outputReal.append(CreateZeroList(n: VectorSize))
            outputImag.append(CreateZeroList(n: VectorSize))
        }
        
        let dt = Date()

        for i in 0..<BenchmarkCount
        {
            inputReal[i].withUnsafeBufferPointer { inPtrReal in
                inputImag[i].withUnsafeBufferPointer { inPtrImag in
                    outputReal[i].withUnsafeMutableBufferPointer { outPtrReal in
                        outputImag[i].withUnsafeMutableBufferPointer { outPtrImag in
                            action(inPtrReal.baseAddress!, inPtrImag.baseAddress!, outPtrReal.baseAddress!, outPtrImag.baseAddress!, inPtrReal.count)
                        }
                    }
                }
            }
        }

        return Date().timeIntervalSince(dt)
    }
    
    public static func AddVVBenchmark() -> TimeInterval {
        return BenchmarkProcess1 { in1, in2, in3, out1, length in
            vDSPWrapper.vadd(in1, in2, out1, Int32(length))
        }
    }
    
    public static func AddVSBenchmark() -> TimeInterval {
        return BenchmarkProcess1 { in1, in2, in3, out1, length in
            vDSPWrapper.vsadd(in1, in3, out1, Int32(length))
        }
    }
    
    public static func SubVVBenchmark() -> TimeInterval {
        return BenchmarkProcess1 { in1, in2, in3, out1, length in
            vDSPWrapper.vsub(in1, in2, out1, Int32(length))
        }
    }
    
    public static func SubVSBenchmark() -> TimeInterval {
        return BenchmarkProcess1 { in1, in2, in3, out1, length in
            vDSPWrapper.vssub(in1, in3, out1, Int32(length))
        }
    }
    
    public static func MultiplyVVBenchmark() -> TimeInterval {
        return BenchmarkProcess1 { in1, in2, in3, out1, length in
            vDSPWrapper.vmul(in1, in2, out1, Int32(length))
        }
    }
    
    public static func MultiplyVSBenchmark() -> TimeInterval {
        return BenchmarkProcess1 { in1, in2, in3, out1, length in
            vDSPWrapper.vsmul(in1, in3, out1, Int32(length))
        }
    }
    
    public static func DevideVVBenchmark() -> TimeInterval {
        return BenchmarkProcess2 { in1, in2, in3, out1, length in
            vDSPWrapper.vdiv(in1, in2, out1, Int32(length))
        }
    }
    
    public static func DevideVSBenchmark() -> TimeInterval {
        return BenchmarkProcess2 { in1, in2, in3, out1, length in
            vDSPWrapper.vsdiv(in1, in3, out1, Int32(length))
        }
    }
    
    public static func Log10Benchmark() -> TimeInterval {
        return BenchmarkProcess3 { in1, in2, in3, out1, length in
            vDSPWrapper.vlog10(in1, out1, Int32(length))
        }
    }
    
    public static func SinBenchmark() -> TimeInterval {
        return BenchmarkProcess1 { in1, in2, in3, out1, length in
            vDSPWrapper.vsin(in1, out1, Int32(length))
        }
    }
    
    public static func CosBenchmark() -> TimeInterval {
        return BenchmarkProcess1 { in1, in2, in3, out1, length in
            vDSPWrapper.vcos(in1, out1, Int32(length))
        }
    }
    
    public static func FFTBenchmark() -> TimeInterval {
        return BenchmarkProcess4 { in1, in2, out1, out2, length in
            vDSPWrapper.vfft(in1, in2, out1, out2, Int32(length))
        }
    }
    
    public static func AbsBenchmark() -> TimeInterval {
        return BenchmarkProcess4 { in1, in2, out1, out2, length in
            vDSPWrapper.vabs(in1, in2, out1, Int32(length))
        }
    }
}
