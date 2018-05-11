#!/bin/bash

set -e

API=LowLevelAPI40
echo "Regenerating $API"
rm -rf $API || :
cp -r LowLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/Net.Pkcs11Interop.LowLevelAPI41/Net.Pkcs11Interop.'"$API"'/' $file
	sed -i -e 's/Pack = 1/Pack = 0/' $file
done

API=LowLevelAPI80
echo "Regenerating $API"
rm -rf $API || :
cp -r LowLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/Net.Pkcs11Interop.LowLevelAPI41/Net.Pkcs11Interop.'"$API"'/' $file
	sed -i -e 's/using NativeULong = System.UInt32;/using NativeULong = System.UInt64;/' $file
	sed -i -e 's/Pack = 1/Pack = 0/' $file
done
sed -i -e 's/#if true \/\/ DO NOT REMOVE - UInt32/#if false \/\/ DO NOT REMOVE - UInt32/' ./$API/NativeLongUtils.cs
sed -i -e 's/#if false \/\/ DO NOT REMOVE - UInt64/#if true \/\/ DO NOT REMOVE - UInt64/' ./$API/NativeLongUtils.cs

API=LowLevelAPI81
echo "Regenerating $API"
rm -rf $API || :
cp -r LowLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/Net.Pkcs11Interop.LowLevelAPI41/Net.Pkcs11Interop.'"$API"'/' $file
	sed -i -e 's/using NativeULong = System.UInt32;/using NativeULong = System.UInt64;/' $file
done
sed -i -e 's/#if true \/\/ DO NOT REMOVE - UInt32/#if false \/\/ DO NOT REMOVE - UInt32/' ./$API/NativeLongUtils.cs
sed -i -e 's/#if false \/\/ DO NOT REMOVE - UInt64/#if true \/\/ DO NOT REMOVE - UInt64/' ./$API/NativeLongUtils.cs

API=HighLevelAPI40
echo "Regenerating $API"
rm -rf $API || :
cp -r HighLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/HighLevelAPI41/'"$API"'/g' $file
	sed -i -e 's/LowLevelAPI41/LowLevelAPI40/g' $file
done

API=HighLevelAPI80
echo "Regenerating $API"
rm -rf $API || :
cp -r HighLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/HighLevelAPI41/'"$API"'/g' $file
	sed -i -e 's/LowLevelAPI41/LowLevelAPI80/g' $file
	sed -i -e 's/using NativeULong = System.UInt32;/using NativeULong = System.UInt64;/' $file
done

API=HighLevelAPI81
echo "Regenerating $API"
rm -rf $API || :
cp -r HighLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/HighLevelAPI41/'"$API"'/g' $file
	sed -i -e 's/LowLevelAPI41/LowLevelAPI81/g' $file
	sed -i -e 's/using NativeULong = System.UInt32;/using NativeULong = System.UInt64;/' $file
done
