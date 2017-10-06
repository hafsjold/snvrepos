#pragma once
#include <Windows.h>

extern UINT g_cObjCount;

class YouTubeClassFactory : public IClassFactory, IUnknown
{
protected:
	DWORD m_ObjRefCount;
	~YouTubeClassFactory();
public:
	YouTubeClassFactory();

	// IUnknown methode
	ULONG __stdcall AddRef();
	ULONG __stdcall Release();
	HRESULT __stdcall QueryInterface(REFIID riid, void **ppvObject);

	// IClassFactory methede
	HRESULT __stdcall CreateInstance(IUnknown *pUnkOuter, REFIID riid, void **ppvObject);
	HRESULT __stdcall LockServer(BOOL fLock);

};
