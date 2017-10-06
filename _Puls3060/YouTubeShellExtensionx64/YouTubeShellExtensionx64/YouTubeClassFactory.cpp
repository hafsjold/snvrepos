#include "YouTubeClassFactory.h"
#include "YouTubeContextMenuHandler.h"

YouTubeClassFactory::~YouTubeClassFactory()
{
	InterlockedDecrement(&g_cObjCount);
}

YouTubeClassFactory::YouTubeClassFactory() : m_ObjRefCount(1)
{
	InterlockedIncrement(&g_cObjCount);
}

ULONG YouTubeClassFactory::AddRef()
{
	return InterlockedIncrement(&m_ObjRefCount);
}

ULONG YouTubeClassFactory::Release()
{
	ULONG returnValue = InterlockedDecrement(&m_ObjRefCount);
	if (returnValue < 1)
	{
		delete this;
	}
	return returnValue;
}

HRESULT YouTubeClassFactory::QueryInterface(REFIID riid, void **ppvObject)
{
	if (!ppvObject)
		return E_POINTER;
	*ppvObject = NULL;

	if (IsEqualIID(riid, IID_IUnknown))
	{
		*ppvObject = this;
		this->AddRef();
		return S_OK;
	}
	else if (IsEqualIID(riid, IID_IClassFactory))
	{
		*ppvObject = (IClassFactory*) this;
		this->AddRef();
		return S_OK;
	}
	else
	{
		return E_NOINTERFACE;
	}
}

HRESULT YouTubeClassFactory::CreateInstance(IUnknown * pUnkOuter, REFIID riid, void ** ppvObject)
{
	if (!ppvObject)
		return E_INVALIDARG;

	if (pUnkOuter != NULL)
		return CLASS_E_NOAGGREGATION;

	HRESULT hr;
	if (IsEqualIID(riid, IID_IShellExtInit) || IsEqualIID(riid, IID_IContextMenu))
	{
		YouTubeContextMenuHandler *pYouTubeContextMenuHandler = new YouTubeContextMenuHandler();
		if (pYouTubeContextMenuHandler == NULL)
			return E_OUTOFMEMORY;

		hr = pYouTubeContextMenuHandler->QueryInterface(riid, ppvObject);
		pYouTubeContextMenuHandler->Release();
	}
	else
	{
		hr = E_NOINTERFACE;
	}
	
	return hr;
}

HRESULT YouTubeClassFactory::LockServer(BOOL fLock)
{
	return S_OK;
}
