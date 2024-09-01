//
//  MyMathLibTests.swift
//  MyMathLibTests
//
//  Created by 田中祐汰 on 2024/08/31.
//

import XCTest
@testable import MyMathLib

class MyMathLibTests: XCTestCase {
    
    let accuracy: Float = 0.00001

    override func setUpWithError() throws {
        // Put setup code here. This method is called before the invocation of each test method in the class.
    }

    override func tearDownWithError() throws {
        // Put teardown code here. This method is called after the invocation of each test method in the class.
    }

    func testAddVV() throws {
        let input1: [Float] = [0, 1, 2]
        let input2: [Float] = [3, 4, 5]
        var output: [Float] = Array(repeating: 0.0, count: input1.count)
        
        input1.withUnsafeBufferPointer { inPtr1 in
            input2.withUnsafeBufferPointer { inPtr2 in
                output.withUnsafeMutableBufferPointer { outPtr in
                    vDSPWrapper.vadd(inPtr1.baseAddress!, inPtr2.baseAddress!, outPtr.baseAddress!, Int32(input1.count))
                }
            }
        }
        
        XCTAssertEqual(output[0], 3.0)
        XCTAssertEqual(output[1], 5.0)
        XCTAssertEqual(output[2], 7.0)
    }
    
    func testAddVS() throws {
        let input: [Float] = [0, 1, 2]
        var output1: [Float] = Array(repeating: 0.0, count: input.count)
        var output2: [Float] = Array(repeating: 0.0, count: input.count)
        
        input.withUnsafeBufferPointer { inPtr in
            output1.withUnsafeMutableBufferPointer { outPtr1 in
                output2.withUnsafeMutableBufferPointer { outPtr2 in
                    vDSPWrapper.vsadd(inPtr.baseAddress!, 3.0, outPtr1.baseAddress!, Int32(input.count))
                    vDSPWrapper.svadd(3.0, inPtr.baseAddress!, outPtr2.baseAddress!, Int32(input.count))
                }
            }
        }
        
        XCTAssertEqual(output1[0], 3.0)
        XCTAssertEqual(output1[1], 4.0)
        XCTAssertEqual(output1[2], 5.0)
        XCTAssertEqual(output2[0], 3.0)
        XCTAssertEqual(output2[1], 4.0)
        XCTAssertEqual(output2[2], 5.0)
    }
    
    func testSubVV() throws {
        let input1: [Float] = [0, 1, 2]
        let input2: [Float] = [3, 4, 5]
        var output: [Float] = Array(repeating: 0.0, count: input1.count)
        
        input1.withUnsafeBufferPointer { inPtr1 in
            input2.withUnsafeBufferPointer { inPtr2 in
                output.withUnsafeMutableBufferPointer { outPtr in
                    vDSPWrapper.vsub(inPtr1.baseAddress!, inPtr2.baseAddress!, outPtr.baseAddress!, Int32(input1.count))
                }
            }
        }
        
        XCTAssertEqual(output[0], -3.0)
        XCTAssertEqual(output[1], -3.0)
        XCTAssertEqual(output[2], -3.0)
    }
    
    func testSubVS() throws {
        let input: [Float] = [0, 1, 2]
        var output1: [Float] = Array(repeating: 0.0, count: input.count)
        var output2: [Float] = Array(repeating: 0.0, count: input.count)
        
        input.withUnsafeBufferPointer { inPtr in
            output1.withUnsafeMutableBufferPointer { outPtr1 in
                output2.withUnsafeMutableBufferPointer { outPtr2 in
                    vDSPWrapper.vssub(inPtr.baseAddress!, 3.0, outPtr1.baseAddress!, Int32(input.count))
                    vDSPWrapper.svsub(3.0, inPtr.baseAddress!, outPtr2.baseAddress!, Int32(input.count))
                }
            }
        }
        
        XCTAssertEqual(output1[0], -3.0)
        XCTAssertEqual(output1[1], -2.0)
        XCTAssertEqual(output1[2], -1.0)
        XCTAssertEqual(output2[0], 3.0)
        XCTAssertEqual(output2[1], 2.0)
        XCTAssertEqual(output2[2], 1.0)
    }
    
    func testMultiplyVV() throws {
        let input1: [Float] = [0, 1, 2]
        let input2: [Float] = [3, 4, 5]
        var output: [Float] = Array(repeating: 0.0, count: input1.count)
        
        input1.withUnsafeBufferPointer { inPtr1 in
            input2.withUnsafeBufferPointer { inPtr2 in
                output.withUnsafeMutableBufferPointer { outPtr in
                    vDSPWrapper.vmul(inPtr1.baseAddress!, inPtr2.baseAddress!, outPtr.baseAddress!, Int32(input1.count))
                }
            }
        }
        
        XCTAssertEqual(output[0], 0.0)
        XCTAssertEqual(output[1], 4.0)
        XCTAssertEqual(output[2], 10.0)
    }
    
    func testMultiplyVS() throws {
        let input: [Float] = [0, 1, 2]
        var output1: [Float] = Array(repeating: 0.0, count: input.count)
        var output2: [Float] = Array(repeating: 0.0, count: input.count)
        
        input.withUnsafeBufferPointer { inPtr in
            output1.withUnsafeMutableBufferPointer { outPtr1 in
                output2.withUnsafeMutableBufferPointer { outPtr2 in
                    vDSPWrapper.vsmul(inPtr.baseAddress!, 3.0, outPtr1.baseAddress!, Int32(input.count))
                    vDSPWrapper.svmul(3.0, inPtr.baseAddress!, outPtr2.baseAddress!, Int32(input.count))
                }
            }
        }
        
        XCTAssertEqual(output1[0], 0.0)
        XCTAssertEqual(output1[1], 3.0)
        XCTAssertEqual(output1[2], 6.0)
        XCTAssertEqual(output2[0], 0.0)
        XCTAssertEqual(output2[1], 3.0)
        XCTAssertEqual(output2[2], 6.0)
    }
    
    func testDevideVV() throws {
        let input1: [Float] = [0, 1, 2]
        let input2: [Float] = [3, 4, 5]
        var output: [Float] = Array(repeating: 0.0, count: input1.count)
        
        input1.withUnsafeBufferPointer { inPtr1 in
            input2.withUnsafeBufferPointer { inPtr2 in
                output.withUnsafeMutableBufferPointer { outPtr in
                    vDSPWrapper.vdiv(inPtr1.baseAddress!, inPtr2.baseAddress!, outPtr.baseAddress!, Int32(input1.count))
                }
            }
        }
        
        XCTAssertEqual(output[0], 0.0 / 3.0, accuracy: accuracy)
        XCTAssertEqual(output[1], 1.0 / 4.0, accuracy: accuracy)
        XCTAssertEqual(output[2], 2.0 / 5.0, accuracy: accuracy)
    }
    
    func testDevideVS() throws {
        let input: [Float] = [1, 2, 3]
        var output1: [Float] = Array(repeating: 0.0, count: input.count)
        var output2: [Float] = Array(repeating: 0.0, count: input.count)
        
        input.withUnsafeBufferPointer { inPtr in
            output1.withUnsafeMutableBufferPointer { outPtr1 in
                output2.withUnsafeMutableBufferPointer { outPtr2 in
                    vDSPWrapper.vsdiv(inPtr.baseAddress!, 3.0, outPtr1.baseAddress!, Int32(input.count))
                    vDSPWrapper.svdiv(3.0, inPtr.baseAddress!, outPtr2.baseAddress!, Int32(input.count))
                }
            }
        }
        
        XCTAssertEqual(output1[0], 1.0 / 3.0, accuracy: accuracy)
        XCTAssertEqual(output1[1], 2.0 / 3.0, accuracy: accuracy)
        XCTAssertEqual(output1[2], 3.0 / 3.0, accuracy: accuracy)
        XCTAssertEqual(output2[0], 3.0 / 1.0, accuracy: accuracy)
        XCTAssertEqual(output2[1], 3.0 / 2.0, accuracy: accuracy)
        XCTAssertEqual(output2[2], 3.0 / 3.0, accuracy: accuracy)
    }
    
    func testLog10() throws {
        let input: [Float] = [1, 2, 3]
        var output: [Float] = Array(repeating: 0.0, count: input.count)
        
        input.withUnsafeBufferPointer { inPtr in
            output.withUnsafeMutableBufferPointer { outPtr in
                vDSPWrapper.vlog10(inPtr.baseAddress!, outPtr.baseAddress!, Int32(input.count))
            }
        }
        
        XCTAssertEqual(output[0], log10(1.0), accuracy: accuracy)
        XCTAssertEqual(output[1], log10(2.0), accuracy: accuracy)
        XCTAssertEqual(output[2], log10(3.0), accuracy: accuracy)
    }
    
    func testSin() throws {
        let input: [Float] = [0, 1, 2]
        var output: [Float] = Array(repeating: 0.0, count: input.count)
        
        input.withUnsafeBufferPointer { inPtr in
            output.withUnsafeMutableBufferPointer { outPtr in
                vDSPWrapper.vsin(inPtr.baseAddress!, outPtr.baseAddress!, Int32(input.count))
            }
        }
        
        XCTAssertEqual(output[0], sin(0.0), accuracy: accuracy)
        XCTAssertEqual(output[1], sin(1.0), accuracy: accuracy)
        XCTAssertEqual(output[2], sin(2.0), accuracy: accuracy)
    }
    
    func testCos() throws {
        let input: [Float] = [0, 1, 2]
        var output: [Float] = Array(repeating: 0.0, count: input.count)
        
        input.withUnsafeBufferPointer { inPtr in
            output.withUnsafeMutableBufferPointer { outPtr in
                vDSPWrapper.vcos(inPtr.baseAddress!, outPtr.baseAddress!, Int32(input.count))
            }
        }
        
        XCTAssertEqual(output[0], cos(0.0), accuracy: accuracy)
        XCTAssertEqual(output[1], cos(1.0), accuracy: accuracy)
        XCTAssertEqual(output[2], cos(2.0), accuracy: accuracy)
    }
    
    private func _FFT(real: [Float], imag: [Float]) -> ([Float], [Float]) {
        var resultReal: [Float] = Array(repeating: 0.0, count: real.count)
        var resultImag: [Float] = Array(repeating: 0.0, count: imag.count)

        for i in 0..<real.count {
            for j in 0..<real.count {
                let tmpR: Float = cos(-2.0 * Float.pi * Float(i) * Float(j) / Float(real.count))
                let tmpI: Float = sin(-2.0 * Float.pi * Float(i) * Float(j) / Float(imag.count))
                resultReal[i] += real[j] * tmpR - imag[j] * tmpI
                resultImag[i] += imag[j] * tmpR + real[j] * tmpI
            }
        }

        return (resultReal, resultImag);
    }
    
    func testFFT() throws {
        let inputReal: [Float] = [0, 2, 4, 6]
        let inputImag: [Float] = [1, 3, 5, 7]
        var outputReal: [Float] = Array(repeating: 0.0, count: inputReal.count)
        var outputImag: [Float] = Array(repeating: 0.0, count: inputReal.count)
        let expected = _FFT(real: inputReal, imag: inputImag)
        
        inputReal.withUnsafeBufferPointer { inRealPtr in
            inputImag.withUnsafeBufferPointer { inImagPtr in
                outputReal.withUnsafeMutableBufferPointer { outRealPtr in
                    outputImag.withUnsafeMutableBufferPointer { outImagPtr in
                        vDSPWrapper.vfft(inRealPtr.baseAddress!, inImagPtr.baseAddress!, outRealPtr.baseAddress!, outImagPtr.baseAddress!, Int32(inputReal.count))
                    }
                }
            }
        }
        
        XCTAssertEqual(outputReal[0], expected.0[0], accuracy: accuracy)
        XCTAssertEqual(outputReal[1], expected.0[1], accuracy: accuracy)
        XCTAssertEqual(outputReal[2], expected.0[2], accuracy: accuracy)
        XCTAssertEqual(outputReal[3], expected.0[3], accuracy: accuracy)
        XCTAssertEqual(outputImag[0], expected.1[0], accuracy: accuracy)
        XCTAssertEqual(outputImag[1], expected.1[1], accuracy: accuracy)
        XCTAssertEqual(outputImag[2], expected.1[2], accuracy: accuracy)
        XCTAssertEqual(outputImag[3], expected.1[3], accuracy: accuracy)
    }
    
    func testAbs() throws {
        let inputReal: [Float] = [0, 2, 4, 6]
        let inputImag: [Float] = [1, 3, 5, 7]
        var output: [Float] = Array(repeating: 0.0, count: inputReal.count)
        
        inputReal.withUnsafeBufferPointer { inRealPtr in
            inputImag.withUnsafeBufferPointer { inImagPtr in
                output.withUnsafeMutableBufferPointer { outPtr in
                    vDSPWrapper.vabs(inRealPtr.baseAddress!, inImagPtr.baseAddress!, outPtr.baseAddress!, Int32(inputReal.count))
                }
            }
        }
        
        XCTAssertEqual(output[0], sqrt(0 + 1), accuracy: accuracy)
        XCTAssertEqual(output[1], sqrt(4 + 9), accuracy: accuracy)
        XCTAssertEqual(output[2], sqrt(16 + 25), accuracy: accuracy)
        XCTAssertEqual(output[3], sqrt(36 + 49), accuracy: accuracy)
    }

    func testPerformanceExample() throws {
        // This is an example of a performance test case.
        self.measure {
            // Put the code you want to measure the time of here.
        }
    }

}
