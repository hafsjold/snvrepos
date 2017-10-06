#pragma once
#include <ShlObj.h>

extern UINT g_cObjCount;

class YouTubeContextMenuHandler : public IShellExtInit, IContextMenu, IUnknown
{
protected:
	DWORD m_objRefCount;
	~YouTubeContextMenuHandler();
public:
	// Constructor
	YouTubeContextMenuHandler();
	
	
	// IUnknown methode
	ULONG __stdcall AddRef();
	ULONG __stdcall Release();
	HRESULT __stdcall QueryInterface(REFIID riid, void **ppvObject);
	
	// IShellExtInit methode
	HRESULT Initialize(
		PCIDLIST_ABSOLUTE pidlFolder,
		IDataObject *pdtobj,
		HKEY hkeyProgID
	);
	
	// IContextMenu methode
	HRESULT GetCommandString(
		UINT_PTR idCmd,
		UINT uFlags,
		UINT *pwReserved,
		LPSTR pszName,
		UINT cchMax
	);
	HRESULT InvokeCommand(
		LPCMINVOKECOMMANDINFO pici
	);
	HRESULT QueryContextMenu(
		HMENU hmenu,
		UINT indexMenu,
		UINT idCmdFirst,
		UINT idCmdLast,
		UINT uFlags
	);
};
