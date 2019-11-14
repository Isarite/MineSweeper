using Isminuotojai.Resources;
// <copyright file="PlayerDataBuilderTest.cs">Copyright ©  2019</copyright>

using System;
using Isminuotojai.Classes;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;

namespace Isminuotojai.Classes.Tests
{
    [TestFixture]
    [PexClass(typeof(PlayerDataBuilder))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class PlayerDataBuilderTest
    {

        [PexMethod]
        public PlayerData GetResult([PexAssumeUnderTest]PlayerDataBuilder target)
        {
            PlayerData result = target.GetResult();
            return result;
            // TODO: add assertions to method PlayerDataBuilderTest.GetResult(PlayerDataBuilder)
        }
    }
}
