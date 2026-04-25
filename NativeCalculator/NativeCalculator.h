#pragma once

#ifdef _WIN32
#define API __declspec(dllexport)
#else
#define API
#endif

extern "C"
{
    API double Add(double left, double right);
    API double Subtract(double left, double right);
    API double Multiply(double left, double right);
    API double Divide(double left, double right);
}
