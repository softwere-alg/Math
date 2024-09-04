//
//  vDSPWrapper.m
//  MyMathLib
//
//  Created by 田中祐汰 on 2024/08/31.
//

#import <Foundation/Foundation.h>
#import <Accelerate/Accelerate.h>

#import "MyMathLib.h"

void vadd(const float* input1, const float* input2, float* output, int length) {
    vDSP_vadd(input1, 1, input2, 1, output, 1, length);
}

void vsadd(const float* input1, float input2, float* output, int length) {
    vDSP_vsadd(input1, 1, &input2, output, 1, length);
}

void svadd(float input1, const float* input2, float* output, int length) {
    vsadd(input2, input1, output, length);
}

void vsub(const float* input1, const float* input2, float* output, int length) {
    vDSP_vsub(input2, 1, input1, 1, output, 1, length);
}

void vssub(const float* input1, float input2, float* output, int length) {
    float _input2 = -input2;
    vsadd(input1, _input2, output, length);
}

void svsub(float input1, const float* input2, float* output, int length) {
    float _input3 = -1;
    vDSP_vsmsa(input2, 1, &_input3, &input1, output, 1, length);
}

void vmul(const float* input1, const float* input2, float* output, int length) {
    vDSP_vmul(input1, 1, input2, 1, output, 1, length);
}

void vsmul(const float* input1, float input2, float* output, int length) {
    vDSP_vsmul(input1, 1, &input2, output, 1, length);
}

void svmul(float input1, const float* input2, float* output, int length) {
    vsmul(input2, input1, output, length);
}

void vdiv(const float* input1, const float* input2, float* output, int length) {
    vDSP_vdiv(input2, 1, input1, 1, output, 1, length);
}

void vsdiv(const float* input1, float input2, float* output, int length) {
    vDSP_vsdiv(input1, 1, &input2, output, 1, length);
}

void svdiv(float input1, const float* input2, float* output, int length) {
    vDSP_vfill(&input1, output, 1, length);
    vdiv(output, input2, output, length);
}

void vlog10(const float* input, float* output, int length) {
    vvlog10f(output, input, &length);
}

void vsin(const float* input, float* output, int length) {
    vvsinf(output, input, &length);
}

void vcos(const float* input, float* output, int length) {
    vvcosf(output, input, &length);
}

vDSP_DFT_Setup setup = nil;
int setupLength = -1;
void vfft(const float* inputReal, const float* inputImag, float* outputReal, float* outputImag, int length) {
    if (setup == nil || length != setupLength) {
        if (setup != nil) {
            vDSP_DFT_DestroySetup(setup);
        }
        setup = vDSP_DFT_zop_CreateSetup(nil, length, vDSP_DFT_FORWARD);
        setupLength = length;
    }
    
    vDSP_DFT_Execute(setup, inputReal, inputImag, outputReal, outputImag);
}

void vabs(const float* inputReal, const float* inputImag, float* output, int length) {
    DSPSplitComplex input;
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Wincompatible-pointer-types-discards-qualifiers"
    input.realp = inputReal;
    input.imagp = inputImag;
#pragma clang diagnostic pop
    
    vDSP_zvabs(&input, 1, output, 1, length);
}

void vclamp(const float* input, float* output, int length, float min, float max) {
    vDSP_vclip(input, 1, &min, &max, output, 1, length);
}

@implementation vDSPWrapper

+ (void) vadd:(const float*)input1 :(const float*)input2 :(float*)output :(int)length {
    vadd(input1, input2, output, length);
}

+ (void)vsadd:(const float *)input1 :(float)input2 :(float *)output :(int)length {
    vsadd(input1, input2, output, length);
}

+ (void)svadd:(float)input1 :(const float *)input2 :(float *)output :(int)length {
    svadd(input1, input2, output, length);
}

+ (void)vsub:(const float *)input1 :(const float *)input2 :(float *)output :(int)length {
    vsub(input1, input2, output, length);
}

+ (void)vssub:(const float *)input1 :(float)input2 :(float *)output :(int)length {
    vssub(input1, input2, output, length);
}

+ (void)svsub:(float)input1 :(const float *)input2 :(float *)output :(int)length {
    svsub(input1, input2, output, length);
}

+ (void)vmul:(const float *)input1 :(const float *)input2 :(float *)output :(int)length {
    vmul(input1, input2, output, length);
}

+ (void)vsmul:(const float *)input1 :(float)input2 :(float *)output :(int)length {
    vsmul(input1, input2, output, length);
}

+ (void)svmul:(float)input1 :(const float *)input2 :(float *)output :(int)length {
    svmul(input1, input2, output, length);
}

+ (void)vdiv:(const float *)input1 :(const float *)input2 :(float *)output :(int)length {
    vdiv(input1, input2, output, length);
}

+ (void)vsdiv:(const float *)input1 :(float)input2 :(float *)output :(int)length {
    vsdiv(input1, input2, output, length);
}

+ (void)svdiv:(float)input1 :(const float *)input2 :(float *)output :(int)length {
    svdiv(input1, input2, output, length);
}

+ (void)vlog10:(const float *)input :(float *)output :(int)length {
    vlog10(input, output, length);
}

+ (void)vsin:(const float *)input :(float *)output :(int)length {
    vsin(input, output, length);
}

+ (void)vcos:(const float *)input :(float *)output :(int)length {
    vcos(input, output, length);
}

+ (void)vfft:(const float *)inputReal :(const float *)inputImag :(float *)outputReal :(float *)outputImag :(int)length {
    vfft(inputReal, inputImag, outputReal, outputImag, length);
}

+ (void)vabs:(const float *)inputReal :(const float *)inputImag :(float *)output :(int)length {
    vabs(inputReal, inputImag, output, length);
}

+ (void) vclamp:(const float*)input :(float*)output :(int)length :(float)min :(float)max {
    vclamp(input, output, length, min, max);
}

@end
