using Foundation;
using System.Runtime.InteropServices;

namespace MyMathLibBinding
{
	// The first step to creating a binding is to add your native framework ("MyLibrary.xcframework")
	// to the project.
	// Open your binding csproj and add a section like this
	// <ItemGroup>
	//   <NativeReference Include="MyLibrary.xcframework">
	//     <Kind>Framework</Kind>
	//     <Frameworks></Frameworks>
	//   </NativeReference>
	// </ItemGroup>
	//
	// Once you've added it, you will need to customize it for your specific library:
	//  - Change the Include to the correct path/name of your library
	//  - Change Kind to Static (.a) or Framework (.framework/.xcframework) based upon the library kind and extension.
	//    - Dynamic (.dylib) is a third option but rarely if ever valid, and only on macOS and Mac Catalyst
	//  - If your library depends on other frameworks, add them inside <Frameworks></Frameworks>
	// Example:
	// <NativeReference Include="libs\MyTestFramework.xcframework">
	//   <Kind>Framework</Kind>
	//   <Frameworks>CoreLocation ModelIO</Frameworks>
	// </NativeReference>
	// 
	// Once you've done that, you're ready to move on to binding the API...
	//
	// Here is where you'd define your API definition for the native Objective-C library.
	//
	// For example, to bind the following Objective-C class:
	//
	//     @interface Widget : NSObject {
	//     }
	//
	// The C# binding would look like this:
	//
	//     [BaseType (typeof (NSObject))]
	//     interface Widget {
	//     }
	//
	// To bind Objective-C properties, such as:
	//
	//     @property (nonatomic, readwrite, assign) CGPoint center;
	//
	// You would add a property definition in the C# interface like so:
	//
	//     [Export ("center")]
	//     CGPoint Center { get; set; }
	//
	// To bind an Objective-C method, such as:
	//
	//     -(void) doSomething:(NSObject *)object atIndex:(NSInteger)index;
	//
	// You would add a method definition to the C# interface like so:
	//
	//     [Export ("doSomething:atIndex:")]
	//     void DoSomething (NSObject object, nint index);
	//
	// Objective-C "constructors" such as:
	//
	//     -(id)initWithElmo:(ElmoMuppet *)elmo;
	//
	// Can be bound as:
	//
	//     [Export ("initWithElmo:")]
	//     NativeHandle Constructor (ElmoMuppet elmo);
	//
	// For more information, see https://aka.ms/ios-binding
	//

	// @interface vDSPWrapper : NSObject
	[BaseType (typeof(NSObject))]
	interface vDSPWrapper
	{
		// +(void)vadd:(const float *)input1 :(const float *)input2 :(float *)output :(int)length;
		[Static]
		[Export ("vadd::::")]
		unsafe void VAdd (float* input1, float* input2, float* output, int length);

		// +(void)vsadd:(const float *)input1 :(float)input2 :(float *)output :(int)length;
		[Static]
		[Export ("vsadd::::")]
		unsafe void VSAdd (float* input1, float input2, float* output, int length);

		// +(void)svadd:(float)input1 :(const float *)input2 :(float *)output :(int)length;
		[Static]
		[Export ("svadd::::")]
		unsafe void SVAdd (float input1, float* input2, float* output, int length);

		// +(void)vsub:(const float *)input1 :(const float *)input2 :(float *)output :(int)length;
		[Static]
		[Export ("vsub::::")]
		unsafe void VSub (float* input1, float* input2, float* output, int length);

		// +(void)vssub:(const float *)input1 :(float)input2 :(float *)output :(int)length;
		[Static]
		[Export ("vssub::::")]
		unsafe void VSSub (float* input1, float input2, float* output, int length);

		// +(void)svsub:(float)input1 :(const float *)input2 :(float *)output :(int)length;
		[Static]
		[Export ("svsub::::")]
		unsafe void SVsub (float input1, float* input2, float* output, int length);

		// +(void)vmul:(const float *)input1 :(const float *)input2 :(float *)output :(int)length;
		[Static]
		[Export ("vmul::::")]
		unsafe void VMul (float* input1, float* input2, float* output, int length);

		// +(void)vsmul:(const float *)input1 :(float)input2 :(float *)output :(int)length;
		[Static]
		[Export ("vsmul::::")]
		unsafe void VSMul (float* input1, float input2, float* output, int length);

		// +(void)svmul:(float)input1 :(const float *)input2 :(float *)output :(int)length;
		[Static]
		[Export ("svmul::::")]
		unsafe void SVMul (float input1, float* input2, float* output, int length);

		// +(void)vdiv:(const float *)input1 :(const float *)input2 :(float *)output :(int)length;
		[Static]
		[Export ("vdiv::::")]
		unsafe void VDiv (float* input1, float* input2, float* output, int length);

		// +(void)vsdiv:(const float *)input1 :(float)input2 :(float *)output :(int)length;
		[Static]
		[Export ("vsdiv::::")]
		unsafe void VSDiv (float* input1, float input2, float* output, int length);

		// +(void)svdiv:(float)input1 :(const float *)input2 :(float *)output :(int)length;
		[Static]
		[Export ("svdiv::::")]
		unsafe void SVDiv (float input1, float* input2, float* output, int length);

		// +(void)vlog10:(const float *)input :(float *)output :(int)length;
		[Static]
		[Export ("vlog10:::")]
		unsafe void VLog10 (float* input, float* output, int length);

		// +(void)vsin:(const float *)input :(float *)output :(int)length;
		[Static]
		[Export ("vsin:::")]
		unsafe void VSin (float* input, float* output, int length);

		// +(void)vcos:(const float *)input :(float *)output :(int)length;
		[Static]
		[Export ("vcos:::")]
		unsafe void VCos (float* input, float* output, int length);

		// +(void)vfft:(const float *)inputReal :(const float *)inputImag :(float *)outputReal :(float *)outputImag :(int)length;
		[Static]
		[Export ("vfft:::::")]
		unsafe void VFFT (float* inputReal, float* inputImag, float* outputReal, float* outputImag, int length);

		// +(void)vabs:(const float *)inputReal :(const float *)inputImag :(float *)output :(int)length;
		[Static]
		[Export ("vabs::::")]
		unsafe void VAbs (float* inputReal, float* inputImag, float* output, int length);
	}
}
