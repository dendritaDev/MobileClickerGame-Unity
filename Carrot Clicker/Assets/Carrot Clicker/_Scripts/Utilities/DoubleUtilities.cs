using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;



public enum IdleAbbreviation
{
    k,
    M,
    B,
    T,
    q,
    Q,
    s,
    S,
    o,
    N,
    d,
    U,
    D,
    Td
}//separacion de 1000 entre cada uno
 

public static class DoubleUtilities  
{
    

    public static string ToScientificNotation(double value)
    {
        int exponent = 0;

        double tValue = value;

        if (value < 10)
            return value.ToString("F0");

        while(tValue > 10)
        {
            tValue /= 10;
            exponent++;
        }

        return tValue.ToString("F2") + "e" + exponent;
    }

    public static string ToCustomScientificNotation(double value)
    {
        if (value < Mathf.Pow(10, 12)) ///si es menor a un trillon:
            return ToSepareatedThousands(value);
        else
            return ToScientificNotation(value);
    }

    public static string ToSepareatedThousands(double value)
    {
        NumberFormatInfo nfi = new NumberFormatInfo();

        nfi.NumberGroupSeparator = " ";
        nfi.NumberDecimalSeparator = ".";

        return value.ToString("N", nfi);
    }
}
