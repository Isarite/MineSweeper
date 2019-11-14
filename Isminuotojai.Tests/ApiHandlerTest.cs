using System.Threading.Tasks;
using Isminuotojai.Resources;
// <copyright file="ApiHandlerTest.cs">Copyright ©  2019</copyright>

using System;
using Isminuotojai.Classes;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;

namespace Isminuotojai.Classes.Tests
{
    [TestFixture]
    [PexClass(typeof(ApiHandler))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ApiHandlerTest
    {

        [PexMethod]
        public Task<MineResult> DoMoveAsync([PexAssumeUnderTest]ApiHandler target, Move move)
        {
            Task<MineResult> result = target.DoMoveAsync(move);
            return result;
            // TODO: add assertions to method ApiHandlerTest.DoMoveAsync(ApiHandler, Move)
        }
    }
}
