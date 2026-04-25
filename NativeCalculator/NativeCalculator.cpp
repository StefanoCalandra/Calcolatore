#include "NativeCalculator.h"

#include <limits>

double Add(double left, double right)
{
    return left + right;
}

double Subtract(double left, double right)
{
    return left - right;
}

double Multiply(double left, double right)
{
    return left * right;
}

double Divide(double left, double right)
{
    if (right == 0.0)
    {
        return std::numeric_limits<double>::quiet_NaN();
    }

    return left / right;
}
