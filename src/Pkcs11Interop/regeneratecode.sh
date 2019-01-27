#!/bin/bash

set -e

SLNDIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

cd $SLNDIR/Pkcs11Interop
./regeneratecode.sh

cd $SLNDIR/Pkcs11Interop.Mock
./regeneratecode.sh

cd $SLNDIR/Pkcs11Interop.Tests
./regeneratecode.sh
