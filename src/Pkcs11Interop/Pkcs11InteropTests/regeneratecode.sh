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
	sed -i -e 's/Net.Pkcs11Interop.Tests.LowLevelAPI41/Net.Pkcs11Interop.Tests.'"$API"'/' $file
	sed -i -e 's/Settings.InitArgs41/Settings.InitArgs40/' $file
done
sed -i -e 's/(Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 1)/(Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 0)/' ./$API/Helpers.cs

API=LowLevelAPI80
echo "Regenerating $API"
rm -rf $API || :
cp -r LowLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/Net.Pkcs11Interop.LowLevelAPI41/Net.Pkcs11Interop.'"$API"'/' $file
	sed -i -e 's/Net.Pkcs11Interop.Tests.LowLevelAPI41/Net.Pkcs11Interop.Tests.'"$API"'/' $file
	sed -i -e 's/Settings.InitArgs41/Settings.InitArgs80/' $file
	sed -i -e 's/using NativeULong = System.UInt32;/using NativeULong = System.UInt64;/' $file
done
sed -i -e 's/(Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 1)/(Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 0)/' ./$API/Helpers.cs

API=LowLevelAPI81
echo "Regenerating $API"
rm -rf $API || :
cp -r LowLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/Net.Pkcs11Interop.LowLevelAPI41/Net.Pkcs11Interop.'"$API"'/' $file
	sed -i -e 's/Net.Pkcs11Interop.Tests.LowLevelAPI41/Net.Pkcs11Interop.Tests.'"$API"'/' $file
	sed -i -e 's/Settings.InitArgs41/Settings.InitArgs81/' $file
	sed -i -e 's/using NativeULong = System.UInt32;/using NativeULong = System.UInt64;/' $file
done
sed -i -e 's/(Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 1)/(Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)/' ./$API/Helpers.cs

API=HighLevelAPI40
echo "Regenerating $API"
rm -rf $API || :
cp -r HighLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/HighLevelAPI41/'"$API"'/' $file
	sed -i -e 's/LowLevelAPI41/LowLevelAPI40/' $file
done
sed -i -e 's/(Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 1)/(Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 0)/' ./$API/Helpers.cs

API=HighLevelAPI80
echo "Regenerating $API"
rm -rf $API || :
cp -r HighLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/HighLevelAPI41/'"$API"'/' $file
	sed -i -e 's/LowLevelAPI41/LowLevelAPI80/' $file
	sed -i -e 's/using NativeULong = System.UInt32;/using NativeULong = System.UInt64;/' $file
done
sed -i -e 's/(Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 1)/(Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 0)/' ./$API/Helpers.cs

API=HighLevelAPI81
echo "Regenerating $API"
rm -rf $API || :
cp -r HighLevelAPI41 $API
files=`find ./$API -type f`
for file in $files; do
	echo "  Processing $file"
	sed -i -e 's/HighLevelAPI41/'"$API"'/' $file
	sed -i -e 's/LowLevelAPI41/LowLevelAPI81/' $file
	sed -i -e 's/using NativeULong = System.UInt32;/using NativeULong = System.UInt64;/' $file
done
sed -i -e 's/(Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 1)/(Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)/' ./$API/Helpers.cs
