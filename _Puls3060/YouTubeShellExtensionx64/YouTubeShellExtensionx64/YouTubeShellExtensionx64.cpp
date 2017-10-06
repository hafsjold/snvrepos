#include <Windows.h>
#include <string>
#include "YouTubeGUID.h" 
#include <Shlobj.h>
#include "YouTubeClassFactory.h"

HINSTANCE g_hInstance;
UINT g_cObjCount;

std::wstring dllName = L"YouTubeShellExtension64";

BOOL __stdcall DllMain(
	HINSTANCE hinstDLL,
	DWORD	  fdwReason,
	LPVOID	  lpReserved)
{
	switch (fdwReason)
	{
		case(DLL_PROCESS_ATTACH):
			g_hInstance = hinstDLL;
			return true;
		default:
			break;
	}

	return true;
}

std::wstring MyStringFromCLSID(IID iid)
{
	wchar_t* tempString;
	StringFromCLSID(iid, &tempString);
	return std::wstring(tempString);
}

std::wstring MyGetModuleFileName()
{
	wchar_t buffer[MAX_PATH];
	GetModuleFileName(g_hInstance, buffer, MAX_PATH);
	return std::wstring(buffer);
}

DWORD MySizeInBytes(std::wstring target)
{
	DWORD size = (target.size() + 1) * 2;
	return size;
}


HRESULT __stdcall DllCanUnloadNow()
{
	return{ g_cObjCount > 0 ? S_FALSE : S_OK };
}

HRESULT __stdcall DllGetClassObject(
	REFCLSID rclsid,
	REFIID   riid,
	LPVOID   *ppv)
{
	if (!IsEqualCLSID(rclsid, CLSID_YouTubeShellExtension))
		return CLASS_E_CLASSNOTAVAILABLE;

	if (!ppv)
		return E_INVALIDARG;
	*ppv = NULL;

	HRESULT hr = E_UNEXPECTED;
	YouTubeClassFactory *pYouTubeClassFactory = new YouTubeClassFactory();
	if (pYouTubeClassFactory != NULL)
	{
		hr = pYouTubeClassFactory->QueryInterface(riid, ppv);
		pYouTubeClassFactory->Release();
	}
	
	return hr;
}

HRESULT __stdcall DllRegisterServer()
{
	HKEY hKey;
	DWORD lpDisp;
	std::wstring;

	// Create GUID key
	std::wstring lpSubKey = L"SOFTWARE\\Classes\\CLSID\\" + MyStringFromCLSID(CLSID_YouTubeShellExtension);
	LONG result = RegCreateKeyEx(HKEY_LOCAL_MACHINE, lpSubKey.c_str(), 0, NULL, REG_OPTION_NON_VOLATILE, KEY_WRITE, NULL, &hKey, &lpDisp);
	if (result != ERROR_SUCCESS) { return E_UNEXPECTED; }

	// Create InprocServer32 key
	result = RegCreateKeyEx(hKey, L"InprocServer32", 0, NULL, REG_OPTION_NON_VOLATILE, KEY_WRITE, NULL, &hKey, &lpDisp);
	if (result != ERROR_SUCCESS) { return E_UNEXPECTED; }

	// Set (default) value
	std::wstring lpDllPath = MyGetModuleFileName();
	result = RegSetValueEx(hKey, NULL, 0, REG_SZ, (BYTE*)lpDllPath.c_str(), MySizeInBytes(lpDllPath));
	if (result != ERROR_SUCCESS) { return E_UNEXPECTED; }

	// Create ThreadingModel value and set to Apartment
	std::wstring appartment = L"Apartment";
	result = RegSetValueEx(hKey, L"ThreadingModel", 0, REG_SZ, (BYTE*)appartment.c_str(), MySizeInBytes(appartment));
	if (result != ERROR_SUCCESS) { return E_UNEXPECTED; }
	RegCloseKey(hKey);

	// Create handler key
	lpSubKey = L"SOFTWARE\\Classes\\txtfile\\ShellEx\\ContextMenuHandlers\\" + dllName;
	result = RegCreateKeyEx(HKEY_LOCAL_MACHINE, lpSubKey.c_str(), 0, NULL, REG_OPTION_NON_VOLATILE, KEY_WRITE, NULL, &hKey, &lpDisp);
	if (result != ERROR_SUCCESS) { return E_UNEXPECTED; }

	// Set handler key (default) value
	result = RegSetValueEx(hKey, NULL, 0, REG_SZ, (BYTE*)MyStringFromCLSID(CLSID_YouTubeShellExtension).c_str(), MySizeInBytes(MyStringFromCLSID(CLSID_YouTubeShellExtension)));
	if (result != ERROR_SUCCESS) { return E_UNEXPECTED; }
	RegCloseKey(hKey);

	// Put on approved list
	lpSubKey = L"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Approved";
	result = RegOpenKeyEx(HKEY_LOCAL_MACHINE, lpSubKey.c_str(), 0, KEY_ALL_ACCESS, &hKey);
	if (result == ERROR_SUCCESS)
	{
		result = RegSetValueEx(hKey, MyStringFromCLSID(CLSID_YouTubeShellExtension).c_str(), 0, REG_SZ, (BYTE*)dllName.c_str(), MySizeInBytes(dllName));
		if (result != ERROR_SUCCESS) { return E_UNEXPECTED; }
	}
	RegCloseKey(hKey);

	// Allert that there has been a change:
	SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, NULL, NULL);
	if (result != ERROR_SUCCESS) { return E_UNEXPECTED; }
	return S_OK;
}

HRESULT __stdcall 	DllUnregisterServer()
{
	HKEY hKey;
	std::wstring lpSubKey;
	LONG result;

	// Delete InprocServer32 key	
	lpSubKey = L"SOFTWARE\\Classes\\CLSID\\" + MyStringFromCLSID(CLSID_YouTubeShellExtension) + L"\\InprocServer32";
	result = RegOpenKeyEx(HKEY_LOCAL_MACHINE, lpSubKey.c_str(), 0, KEY_ALL_ACCESS, &hKey);
	if (result == ERROR_SUCCESS)
	{
		result = RegDeleteKey(HKEY_LOCAL_MACHINE, lpSubKey.c_str());
		if (result != ERROR_SUCCESS) { return E_UNEXPECTED; }
	}

	// Check if GUID key exists and then delete it if so.
	lpSubKey = L"SOFTWARE\\Classes\\CLSID\\" + MyStringFromCLSID(CLSID_YouTubeShellExtension);
	result = RegOpenKeyEx(HKEY_LOCAL_MACHINE, lpSubKey.c_str(), 0, KEY_ALL_ACCESS, &hKey);
	if (result == ERROR_SUCCESS)
	{
		result = RegDeleteKey(HKEY_LOCAL_MACHINE, lpSubKey.c_str());
		if (result != ERROR_SUCCESS) { return E_UNEXPECTED; }
	}
	RegCloseKey(hKey);

	// Delete handler key
	lpSubKey = L"SOFTWARE\\Classes\\txtfile\\ShellEx\\ContextMenuHandlers\\" + dllName;
	result = RegOpenKeyEx(HKEY_LOCAL_MACHINE, lpSubKey.c_str(), 0, KEY_ALL_ACCESS, &hKey);
	if (result == ERROR_SUCCESS)
	{
		result = RegDeleteKey(HKEY_LOCAL_MACHINE, lpSubKey.c_str());
		if (result != ERROR_SUCCESS) { return E_UNEXPECTED; }
	}
	RegCloseKey(hKey);

	// Delete value from the approved list
	lpSubKey = L"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Approved";
	result = RegOpenKeyEx(HKEY_LOCAL_MACHINE, lpSubKey.c_str(), 0, KEY_ALL_ACCESS, &hKey);
	if (result == ERROR_SUCCESS)
	{
		result = RegDeleteValue(hKey, MyStringFromCLSID(CLSID_YouTubeShellExtension).c_str());
		if (result != ERROR_SUCCESS) { return E_UNEXPECTED; }
	}
	RegCloseKey(hKey);

	// Allert that there has been a change:
	SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, NULL, NULL);
	if (result != ERROR_SUCCESS) { return E_UNEXPECTED; }

	return S_OK;
}