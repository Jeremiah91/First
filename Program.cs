using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Solution
{
    static void input()
    {
        Console.Write("Please enter an input: ");
        string name = Console.ReadLine();

        Console.WriteLine("Input: " + name);
        Console.ReadLine();
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Hello World\n");

        input();
    }
}
