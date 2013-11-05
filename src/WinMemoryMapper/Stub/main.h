#pragma once

#ifndef _MAIN_H
#define _MAIN_H

#include <Windows.h>

typedef struct _INIT_STRUCT {
	int X;
	int Y;
} INIT_STRUCT, *PINIT_STRUCT;

DWORD WINAPI DllMain( HMODULE, DWORD_PTR, LPVOID );
extern "C" __declspec(dllexport) void ApplyHook( void );
extern "C" __declspec(dllexport) void RemoveHook( void );
extern "C" __declspec(dllexport) void SetPoint( PVOID );

#endif