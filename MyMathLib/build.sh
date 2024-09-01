rm -rf build
mkdir build

xcodebuild \
 -target MyMathLib \
 -project MyMathLib.xcodeproj \
 -configuration Release \
 -sdk iphoneos \
 build
xcodebuild \
 -target MyMathLib \
 -project MyMathLib.xcodeproj \
 -configuration Release \
 -sdk iphonesimulator \
 build

rm -rf Products
mkdir Products
xcodebuild -create-xcframework -framework build/Release-iphoneos/MyMathLib.framework -framework build/Release-iphonesimulator/MyMathLib.framework -output Products/MyMathLib.xcframework

sharpie bind --sdk=`sharpie xcode -sdks | grep -o -E 'iphoneos[0-9]+\.[0-9]+'` --output="Products/ApiDef" --namespace="Binding" --scope="build/Release-iphoneos/MyMathLib.framework/Headers/" build/Release-iphoneos/MyMathLib.framework/Headers/*.h
