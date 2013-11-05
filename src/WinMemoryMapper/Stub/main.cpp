#include "main.h"
#include "StubVariable.h"
#include <cstdio>

BOOL WINAPI GetCursorPosHook(
    _Out_ LPPOINT lpPoint)
{
	POINT eP = Singleton::getInstance()->vars.externPoint;
	if ( eP.x != -1 && eP.y != -1)
	{
		lpPoint->x = eP.x;
		lpPoint->y = eP.y;
	} else {
		return Singleton::getInstance()->vars.pfnOrigFn(lpPoint);
	}

	return true;
}

bool HotPatch(void *oldProc, void *newProc, void**ppOrigFn)
{
    bool bRet = false;
    DWORD    oldProtect = NULL;

    WORD* pJumpBack = (WORD*)oldProc;
    BYTE* pLongJump = ((BYTE*)oldProc-5);
    DWORD* pLongJumpAdr = ((DWORD*)oldProc-1);

    VirtualProtect(pLongJump, 7, PAGE_EXECUTE_WRITECOPY, &oldProtect);

    // don’t hook functions which have already been hooked
    if ((0xff8b == *pJumpBack) && 
        (0x90 == *pLongJump) &&
        (0x90909090 == *pLongJumpAdr))
    {
        *pLongJump = 0xE9;    // long jmp
        *pLongJumpAdr = ((DWORD)newProc)-((DWORD)oldProc);    // 
        *pJumpBack = 0xF9EB;        // short jump back -7 (back 5, plus two for this jump)

        if (ppOrigFn)
            *ppOrigFn = ((BYTE*)oldProc)+2;
        bRet = true;
    }
    VirtualProtect(pLongJump, 7, oldProtect, &oldProtect);
    return bRet;
}

bool HotUnpatch(void*oldProc)    // the original fn ptr, not "ppOrigFn" from HotPatch
{
    bool bRet = false;
    DWORD    oldProtect = NULL;

    WORD* pJumpBack = (WORD*)oldProc;

    VirtualProtect(pJumpBack, 2, PAGE_EXECUTE_WRITECOPY, &oldProtect);

    if (0xF9EB == *pJumpBack)
    {
        *pJumpBack = 0xff8b;        // mov edi, edi = nop
        bRet = true;
    }

    VirtualProtect(pJumpBack, 2, oldProtect, &oldProtect);
    return bRet;
}

extern "C" __declspec(dllexport) void ApplyHook() {
	HotPatch(GetCursorPos, (void*) GetCursorPosHook, (void**) &Singleton::getInstance()->vars.pfnOrigFn);
}

extern "C" __declspec(dllexport) void RemoveHook() {
	HotUnpatch(GetCursorPos);
}

extern "C" __declspec(dllexport) void SetPoint(PVOID message) {
	PINIT_STRUCT messageStruct = reinterpret_cast<PINIT_STRUCT>(message);
	Singleton::getInstance()->vars.externPoint.x = messageStruct->X;
	Singleton::getInstance()->vars.externPoint.y = messageStruct->Y;
}
