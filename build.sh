dir=$(cd $(dirname $0); pwd)

cd $dir
cd MyMathLib
sh build.sh

cd $dir
cd MyMathLibBinding
sh build.sh
