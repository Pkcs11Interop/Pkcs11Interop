#!/bin/bash

set -e

API=LowLevelAPI40
echo "Regenerating $API"
rm -rf $API || :
cp -r LowLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/Code in this file is maintained manually/Code in this file is generated automatically/' $file
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
	sed -i -e 's/Code in this file is maintained manually/Code in this file is generated automatically/' $file
	sed -i -e 's/Net.Pkcs11Interop.LowLevelAPI41/Net.Pkcs11Interop.'"$API"'/' $file
	sed -i -e 's/using NativeULong = System.UInt32;/using NativeULong = System.UInt64;/' $file
	sed -i -e 's/Pack = 1/Pack = 0/' $file
	sed -i -e 's/NativeULongUtils.ConvertUInt32/NativeULongUtils.ConvertUInt64/g' $file
done

API=LowLevelAPI81
echo "Regenerating $API"
rm -rf $API || :
cp -r LowLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/Code in this file is maintained manually/Code in this file is generated automatically/' $file
	sed -i -e 's/Net.Pkcs11Interop.LowLevelAPI41/Net.Pkcs11Interop.'"$API"'/' $file
	sed -i -e 's/using NativeULong = System.UInt32;/using NativeULong = System.UInt64;/' $file
	sed -i -e 's/NativeULongUtils.ConvertUInt32/NativeULongUtils.ConvertUInt64/g' $file
done

API=HighLevelAPI40
echo "Regenerating $API"
rm -rf $API || :
cp -r HighLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/Code in this file is maintained manually/Code in this file is generated automatically/' $file
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
	sed -i -e 's/Code in this file is maintained manually/Code in this file is generated automatically/' $file
	sed -i -e 's/HighLevelAPI41/'"$API"'/g' $file
	sed -i -e 's/LowLevelAPI41/LowLevelAPI80/g' $file
	sed -i -e 's/using NativeULong = System.UInt32;/using NativeULong = System.UInt64;/' $file
	sed -i -e 's/NativeULongUtils.ConvertUInt32/NativeULongUtils.ConvertUInt64/g' $file
done

API=HighLevelAPI81
echo "Regenerating $API"
rm -rf $API || :
cp -r HighLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/Code in this file is maintained manually/Code in this file is generated automatically/' $file
	sed -i -e 's/HighLevelAPI41/'"$API"'/g' $file
	sed -i -e 's/LowLevelAPI41/LowLevelAPI81/g' $file
	sed -i -e 's/using NativeULong = System.UInt32;/using NativeULong = System.UInt64;/' $file
	sed -i -e 's/NativeULongUtils.ConvertUInt32/NativeULongUtils.ConvertUInt64/g' $file
done
