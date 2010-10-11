// Modified by Ian 2010-10-01
// tpforums.org, ianobermiller.com, opentibiatools.com
// Created by Stiju 2010-10-01
// www.stiju.com

#include <Windows.h>
#include <mscoree.h>
#include <metahost.h>
#include <cor.h>
#include <stdio.h>
#include <wchar.h>

#pragma comment(lib, "mscoree.lib")

HINSTANCE hMod;

wchar_t szDllPath[MAX_PATH];

class MException {
public:
    int ErrorCode;
    const wchar_t *ErrorMessage;
    MException(const wchar_t *Message, int Code = 0) : ErrorMessage(Message), ErrorCode(Code) {}
};

void __declspec(noreturn) UninjectSelf()
{       
    DWORD ExitCode=0;
    __asm
    {
        push hMod
        push ExitCode
        jmp dword ptr [FreeLibraryAndExitThread] 
    }
}

void StartUninjectSelf()
{
    try
    {
        CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)UninjectSelf, hMod, NULL, NULL);
    }
    catch (...)
    {
        #if _DEBUG
        MessageBoxA(0,"StartUninjectSelf -> Unable to uninject from process.", "Injected DLL - Fatal Error", MB_ICONERROR | MB_TOPMOST );
        #endif
    }
}

void ThrowHresultError(const wchar_t* message, HRESULT hr)
{
    wchar_t buffer[1024];

    LPVOID lpMsgBuf;
    FormatMessage(
        FORMAT_MESSAGE_ALLOCATE_BUFFER | 
        FORMAT_MESSAGE_FROM_SYSTEM |
        FORMAT_MESSAGE_IGNORE_INSERTS,
        NULL,
        hr,
        MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
        (LPTSTR) &lpMsgBuf,
        0, NULL
    );

    wsprintf(buffer, L"%s failed with HR:%x (%s)", message, hr, lpMsgBuf);
    if (FAILED(hr))
    {
        throw MException(buffer);
    }
}

void ThrowFalseError(const wchar_t* action, const wchar_t* message)
{
    wchar_t buffer[1024];
    wsprintf(buffer, L"%s was false, %s", action, message);
    throw MException(buffer);
}

#define THROW_IF_FAILED(action) { hr = action; if (FAILED(hr)) ThrowHresultError(L#action, hr); }
#define THROW_IF_FALSE(action, message) { BOOL result = action; if (result == FALSE) ThrowFalseError(L#action, message); }

void RunBooter()
{
    ICLRMetaHost *pMetaHost = NULL;
    ICLRRuntimeInfo *pRuntimeInfo = NULL;
    ICLRRuntimeHost *pRuntimeHost = NULL;

    try
    {
        HRESULT hr = NULL;

        THROW_IF_FAILED(CLRCreateInstance(CLSID_CLRMetaHost, IID_ICLRMetaHost, (LPVOID*)&pMetaHost));
        
        THROW_IF_FAILED(pMetaHost->GetRuntime(L"v4.0.30319", IID_ICLRRuntimeInfo, (LPVOID*)&pRuntimeInfo));

        BOOL loadable = FALSE;
        THROW_IF_FAILED(pRuntimeInfo->IsLoadable(&loadable));
		THROW_IF_FALSE(loadable, L".NET Runtime v4.0.30319 cannot be loaded")

        THROW_IF_FAILED(pRuntimeInfo->GetInterface(CLSID_CLRRuntimeHost, IID_ICLRRuntimeHost, (LPVOID*)&pRuntimeHost))
        
        BOOL isStarted;
        DWORD startupFlags;

        THROW_IF_FAILED(pRuntimeInfo->IsStarted(&isStarted, &startupFlags));

        if (!isStarted)
        {
            THROW_IF_FAILED(pRuntimeHost->Start());

            // Allow usage of CLR 2 assemblies
            THROW_IF_FAILED(pRuntimeInfo->BindAsLegacyV2Runtime());
        }

        DWORD ret;
        THROW_IF_FAILED(pRuntimeHost->ExecuteInDefaultAppDomain(szDllPath, L"Inject.InjectedMain", L"EntryPoint", L"Parameter", &ret));
    }
    catch(MException ex)
    {
        MessageBox(0, ex.ErrorMessage, L"Booter", MB_ICONERROR);
    }

    if(pRuntimeHost != 0)
    {
        pRuntimeHost->Release();
    }

    if(pRuntimeInfo != 0)
    {
        pRuntimeInfo->Release();
    }

    if(pMetaHost != 0)
    {
        pMetaHost->Release();
    }

    StartUninjectSelf();
}

DWORD WINAPI MyThread( LPVOID lpParam )
{
    RunBooter();
    return 0;
}

BOOL WINAPI DllMain(HINSTANCE hinstDLL, DWORD fdwReason, LPVOID lpvReserved)
{
	PWCH removeBooter = NULL;
    switch(fdwReason)
    {
    case DLL_PROCESS_ATTACH:
        hMod = hinstDLL;
		// Remove "Booter" from this dll name to get the managed injected dll name
		// Ex. Rename Booter.dll to InjectBooter.dll to inject the managed Inject.dll
        GetModuleFileName(hinstDLL, szDllPath, sizeof(szDllPath));
		removeBooter = wcsrchr(szDllPath, 'B');
        if (removeBooter != NULL)
		{
			wcscpy_s(removeBooter, removeBooter - szDllPath, L".dll");
		}
        CreateThread(0, 0, MyThread, 0, 0, 0);
        break;
    case DLL_PROCESS_DETACH:
        break;
    case DLL_THREAD_ATTACH:
        break;
    case DLL_THREAD_DETACH:
        break;
    }
    return TRUE;
}