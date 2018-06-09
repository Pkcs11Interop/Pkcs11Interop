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
	sed -i -e 's/LowLevelAPI41/'"$API"'/g' $file
	sed -i -e 's/Settings.InitArgs41/Settings.InitArgs40/' $file
done
sed -i -e 's/(Platform.NativeULongSize != 4 || Platform.StructPackingSize != 1)/(Platform.NativeULongSize != 4 || Platform.StructPackingSize != 0)/' ./$API/Helpers.cs

API=LowLevelAPI80
echo "Regenerating $API"
rm -rf $API || :
cp -r LowLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/Code in this file is maintained manually/Code in this file is generated automatically/' $file
	sed -i -e 's/LowLevelAPI41/'"$API"'/g' $file
	sed -i -e 's/using NativeULong = System.UInt32;/using NativeULong = System.UInt64;/' $file
	sed -i -e 's/ConvertUtils.UInt32/ConvertUtils.UInt64/g' $file
	sed -i -e 's/Settings.InitArgs41/Settings.InitArgs80/' $file
done
sed -i -e 's/(Platform.NativeULongSize != 4 || Platform.StructPackingSize != 1)/(Platform.NativeULongSize != 8 || Platform.StructPackingSize != 0)/' ./$API/Helpers.cs

API=LowLevelAPI81
echo "Regenerating $API"
rm -rf $API || :
cp -r LowLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/Code in this file is maintained manually/Code in this file is generated automatically/' $file
	sed -i -e 's/LowLevelAPI41/'"$API"'/g' $file
	sed -i -e 's/using NativeULong = System.UInt32;/using NativeULong = System.UInt64;/' $file
	sed -i -e 's/ConvertUtils.UInt32/ConvertUtils.UInt64/g' $file
	sed -i -e 's/Settings.InitArgs41/Settings.InitArgs81/' $file
done
sed -i -e 's/(Platform.NativeULongSize != 4 || Platform.StructPackingSize != 1)/(Platform.NativeULongSize != 8 || Platform.StructPackingSize != 1)/' ./$API/Helpers.cs
