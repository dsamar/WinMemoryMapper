#include "StubVariable.h"

Variables::Variables()
{
	externPoint = POINT();
	externPoint.x = -1;
	externPoint.y = -1;
}

Singleton* Singleton::instance = 0;