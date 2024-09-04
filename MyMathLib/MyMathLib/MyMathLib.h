//
//  MyMathLib.h
//  MyMathLib
//
//  Created by 田中祐汰 on 2024/08/31.
//

#import <Foundation/Foundation.h>

//! Project version number for MyMathLib.
FOUNDATION_EXPORT double MyMathLibVersionNumber;

//! Project version string for MyMathLib.
FOUNDATION_EXPORT const unsigned char MyMathLibVersionString[];

// In this header, you should import all the public headers of your framework using statements like #import <MyMathLib/PublicHeader.h>

//void vadd(const float* input1, const float* input2, float* output, int length);
//void vsadd(const float* input1, float input2, float* output, int length);
//void svadd(float input1, const float* input2, float* output, int length);
//
//void vsub(const float* input1, const float* input2, float* output, int length);
//void vssub(const float* input1, float input2, float* output, int length);
//void svsub(float input1, const float* input2, float* output, int length);
//
//void vmul(const float* input1, const float* input2, float* output, int length);
//void vsmul(const float* input1, float input2, float* output, int length);
//void svmul(float input1, const float* input2, float* output, int length);
//
//void vdiv(const float* input1, const float* input2, float* output, int length);
//void vsdiv(const float* input1, float input2, float* output, int length);
//void svdiv(float input1, const float* input2, float* output, int length);
//
//void vlog10(const float* input, float* output, int length);
//
//void vsin(const float* input, float* output, int length);
//void vcos(const float* input, float* output, int length);
//
//void vfft(const float* inputReal, const float* inputImag, float* outputReal, float* outputImag, int length);
//
//void vabs(const float* inputReal, const float* inputImag, float* output, int length);

@interface vDSPWrapper : NSObject

+ (void) vadd:(const float*)input1 :(const float*)input2 :(float*)output :(int)length;
+ (void) vsadd:(const float*)input1 :(float)input2 :(float*)output :(int)length;
+ (void) svadd:(float)input1 :(const float*)input2 :(float*)output :(int)length;

+ (void) vsub:(const float*)input1 :(const float*)input2 :(float*)output :(int)length;
+ (void) vssub:(const float*)input1 :(float)input2 :(float*)output :(int)length;
+ (void) svsub:(float) input1 :(const float*)input2 :(float*)output :(int)length;

+ (void) vmul:(const float*)input1 :(const float*)input2 :(float*)output :(int)length;
+ (void) vsmul:(const float*)input1 :(float)input2 :(float*)output :(int)length;
+ (void) svmul:(float)input1 :(const float*)input2 :(float*)output :(int)length;

+ (void) vdiv:(const float*)input1 :(const float*)input2 :(float*)output :(int)length;
+ (void) vsdiv:(const float*)input1 :(float)input2 :(float*)output :(int)length;
+ (void) svdiv:(float)input1 :(const float*)input2 :(float*)output :(int)length;

+ (void) vlog10:(const float*)input :(float*)output :(int)length;

+ (void) vsin:(const float*)input :(float*)output :(int)length;
+ (void) vcos:(const float*)input :(float*)output :(int)length;

+ (void) vfft:(const float*)inputReal :(const float*)inputImag :(float*)outputReal :(float*)outputImag :(int)length;

+ (void) vabs:(const float*)inputReal :(const float*)inputImag :(float*)output :(int)length;

+ (void) vclamp:(const float*)input :(float*)output :(int)length :(float)min :(float)max;

@end
