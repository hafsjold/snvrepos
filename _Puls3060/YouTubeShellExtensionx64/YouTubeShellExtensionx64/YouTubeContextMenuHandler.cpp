#include "YouTubeContextMenuHandler.h"

YouTubeContextMenuHandler::~YouTubeContextMenuHandler()
{
	InterlockedDecrement(&g_cObjCount);
}

YouTubeContextMenuHandler::YouTubeContextMenuHandler() : m_objRefCount(1)
{
	InterlockedIncrement(&g_cObjCount);
}

ULONG YouTubeContextMenuHandler::AddRef()
{
	return InterlockedIncrement(&m_objRefCount);
}

ULONG YouTubeContextMenuHandler::Release()
{
	ULONG returnValue = InterlockedDecrement(&m_objRefCount);
	if (returnValue < 1)
	{
		delete this;
	}
	return returnValue;
}

HRESULT YouTubeContextMenuHandler::QueryInterface(REFIID riid, void **ppvObject)
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
	else if (IsEqualIID(riid, IID_IContextMenu))
	{
		*ppvObject = (IContextMenu*) this;
		this->AddRef();
		return S_OK;
	}
	else if (IsEqualIID(riid, IID_IShellExtInit))
	{
		*ppvObject = (IShellExtInit*) this;
		this->AddRef();
		return S_OK;
	}
	else
	{
		return E_NOINTERFACE;
	}
}

HRESULT YouTubeContextMenuHandler::Initialize(PCIDLIST_ABSOLUTE pidlFolder, IDataObject * pdtobj, HKEY hkeyProgID)
{
	return S_OK;
}

HRESULT YouTubeContextMenuHandler::GetCommandString(UINT_PTR idCmd, UINT uFlags, UINT * pwReserved, LPSTR pszName, UINT cchMax)
{
	return E_NOTIMPL;
}

HRESULT YouTubeContextMenuHandler::InvokeCommand(LPCMINVOKECOMMANDINFO pici)
{
	system("mspaint.exe");
	return S_OK;
}

HRESULT YouTubeContextMenuHandler::QueryContextMenu(HMENU hmenu, UINT indexMenu, UINT idCmdFirst, UINT idCmdLast, UINT uFlags)
{
	if (uFlags & CMF_DEFAULTONLY)
		return MAKE_HRESULT(SEVERITY_SUCCESS, FACILITY_NULL, 0);

	MENUITEMINFO myItem = {};
	myItem.cbSize = sizeof(MENUITEMINFO);
	myItem.fMask = MIIM_STRING | MIIM_ID;
	myItem.wID = idCmdFirst + 1;
	myItem.dwTypeData = L"YouTube Test Item";
	
	if (!InsertMenuItem(hmenu, 0, TRUE, &myItem))
	{
		return HRESULT_FROM_WIN32(GetLastError());
	}

	return MAKE_HRESULT(SEVERITY_SUCCESS, 0, myItem.wID - idCmdFirst + 1);
}
