PKCS11-MOCK is minimalistic C library that implements PKCS#11 v3.1 API. 
It is not a real cryptographic module but just a dummy mock object designed 
specifically for unit testing of Pkcs11Interop library.

The Wikipedia article on mock objects states:

  In object-oriented programming, mock objects are simulated objects 
  that mimic the behavior of real objects in controlled ways. A programmer 
  typically creates a mock object to test the behavior of some other object, 
  in much the same way that a car designer uses a crash test dummy 
  to simulate the dynamic behavior of a human in vehicle impacts.

Following these simple principles PKCS11-MOCK does not depend on any hardware 
nor configuration and can be easily modified to return any response or data. 

For more info visit https://github.com/Pkcs11Interop/pkcs11-mock