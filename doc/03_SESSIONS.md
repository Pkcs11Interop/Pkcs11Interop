# Quick Introduction to PKCS#11 Sessions

Session represents a logical connection between an application and a token.

## Session Handles

Every session is identified by its session handle, a `ulong` identifier stored inside `ISession` in Pkcs11Interop. The session handle is passed to PKCS#11 functions as a parameter to specify which session the function should act on. While a session can theoretically be used by multiple application threads with proper locking mechanisms, it is better to avoid this. The safest approach is to create a new session for each cryptographic operation, regardless of the thread.

## Session Types

There are two basic session types:

- **Read-Only**  
  Provides read-only access to token objects and read-write access to session objects.

- **Read-Write**  
  Provides read-write access to token objects and read-write access to session objects.

## Session Login

By default, a session provides access only to public objects. Access to private objects is allowed only after a session login is performed.

There are two basic user types that can perform a session login:

- **Normal User**  
  Normal user - `CKU_USER` - is allowed to access to private objects.

- **Security Officer**  
  Security officer - `CKU_SO` - is not allowed to access private objects but can perform token initialization and reset the PIN of Normal User.

It is important to note that all sessions that an application has open with a token share the same login status. When an application's session logs into a token, all of that application's sessions with that token become logged in. Conversely, when an application's session logs out of a token, all of that application's sessions with that token become logged out.

## Session States

Session can be in one of the five states, depending on the session type and whether a login has been performed:

- **CKS_RO_PUBLIC_SESSION**  
  The session is read-only and no user is logged in.  
  The application has read-only access to public token objects and read-write access to public session objects.

- **CKS_RO_USER_FUNCTIONS**  
  The session is read-only and a normal user is logged in.  
  The application has read-only access to all token objects and read-write access to all session objects.

- **CKS_RW_PUBLIC_SESSION**  
  The session is read-write and no user is logged in.  
  The application has read-write access to public token objects and read-write access to public session objects.

- **CKS_RW_USER_FUNCTIONS**  
  The session is read-write and a normal user is logged in.  
  The application has read-write access to all token objects and read-write access to all session objects.

- **CKS_RW_SO_FUNCTIONS**  
  The session is read-write and a security officer is logged in.  
  The application has read-write access to public token objects and read-write access to public session objects.

Each session state provides different access levels to both session and token objects.

The following table summarizes session states and their characteristics:

  | Session State           | Session Type | Authenticated User | Public Session Objects | Private Session Objects | Public Token Objects | Private Token Objects |
  | :---------------------- | :----------- | :----------------- | -----------------------| ------------------------|----------------------|-----------------------|
  | `CKS_RO_PUBLIC_SESSION` | Read-Only    | None               | Read, Write            | None                    | Read                 | None                  |
  | `CKS_RO_USER_FUNCTIONS` | Read-Only    | Normal User        | Read, Write            | Read, Write             | Read                 | Read                  |
  | `CKS_RW_PUBLIC_SESSION` | Read-Write   | None               | Read, Write            | None                    | Read, Write          | None                  |
  | `CKS_RW_USER_FUNCTIONS` | Read-Write   | Normal User        | Read, Write            | Read, Write             | Read, Write          | Read, Write           |
  | `CKS_RW_SO_FUNCTIONS`   | Read-Write   | Security Officer   | Read, Write            | None                    | Read, Write          | None                  |

## Session Lifetime and Visibility

All threads of a given application have access to the same sessions and session objects.

When a session is closed, any session objects created in that session are destroyed. This is true even for session objects being used by other sessions. For example, if an application has multiple sessions open with a token and creates a session object using one of them, that session object is visible through any of the application's sessions. However, as soon as the session that created the object is closed, the object is destroyed.

[Next page >](04_ARCHITECTURE.md)