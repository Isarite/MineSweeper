// <copyright file="MineResultTest.cs">Copyright ©  2019</copyright>

using System;
using Isminuotojai.Resources;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;

namespace Isminuotojai.Resources.Tests
{
    [TestFixture]
    [PexClass(typeof(MineResult))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class MineResultTest
    {

        [PexMethod]
        public MineResult Constructor()
        {
            MineResult target = new MineResult();
            return target;
            // TODO: add assertions to method MineResultTest.Constructor()
        }
    }
}
