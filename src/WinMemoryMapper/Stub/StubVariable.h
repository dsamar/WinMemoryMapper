#ifndef _STUBVARIABLE_H
#define	_STUBVARIABLE_H

#include <stdio.h>
#include <stdlib.h>
#include <Windows.h>

typedef BOOL (WINAPI GETCURSORPOSFN)( _Out_ LPPOINT lpPoint);

class Variables {
public:
	Variables();
	POINT externPoint;	
	GETCURSORPOSFN* pfnOrigFn;
};

class Singleton{ 
public:
	Variables vars;

	static Singleton* getInstance(){ 
		if (instance == 0){ 
			instance = new Singleton;
		} // end if
		return instance;
	}
private:
	Singleton(){ 
		void init();
	}
	static Singleton* instance;
};

#endif	/* _SINGLETON_H */