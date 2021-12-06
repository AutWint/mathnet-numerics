﻿// <copyright file="UseLinearAlgebraProvider.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
//
// Copyright (c) 2009-2018 Math.NET
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

#if MKL
using MathNet.Numerics.Providers.MKL;
#elif CUDA
using MathNet.Numerics.Providers.CUDA;
#elif OPENBLAS
using MathNet.Numerics.Providers.OpenBLAS;
#endif

namespace MathNet.Numerics.UnitTests
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class UseLinearAlgebraProvider : Attribute, ITestAction
    {
        public void BeforeTest(ITest testDetails)
        {
            string outDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"../../../../../out/"));
#if MKL
            Control.NativeProviderPath = Path.GetFullPath(Path.Combine(outDir, @"MKL/Windows/"));
            Control.UseNativeMKL();
            //MklControl.UseNativeMKL(MklConsistency.AVX, MklPrecision.Double, MklAccuracy.High);
#elif CUDA
            Control.UseNativeCUDA();
            //CudaControl.UseNativeCUDA();
#elif OPENBLAS
            Control.UseNativeOpenBLAS();
            //OpenBlasControl.UseNativeOpenBLAS();
#else
            Control.UseManaged();
#endif

            // ReSharper disable LocalizableElement
            Console.WriteLine();
            Console.WriteLine(Control.Describe());
            // ReSharper restore LocalizableElement
        }

        public void AfterTest(ITest details)
        {
        }

        public ActionTargets Targets => ActionTargets.Suite;
    }
}
