using Microsoft.Pex.Framework.Generated;
using NUnit.Framework;
using Microsoft.Pex.Framework;
using Isminuotojai.Resources;
using Isminuotojai.Classes;
// <copyright file="PlayerDataBuilderTest.GetResult.g.cs">Copyright ©  2019</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace Isminuotojai.Classes.Tests
{
    public partial class PlayerDataBuilderTest
    {

[Test]
[PexGeneratedBy(typeof(PlayerDataBuilderTest))]
public void GetResult877()
{
    PlayerDataBuilder playerDataBuilder;
    PlayerData playerData;
    playerDataBuilder = PlayerDataBuilderFactory.Create();
    playerData = this.GetResult(playerDataBuilder);
    PexAssert.IsNotNull((object)playerData);
    PexAssert.AreEqual<string>((string)null, playerData.userName);
    PexAssert.AreEqual<string>((string)null, playerData.password);
    PexAssert.IsNotNull((object)playerDataBuilder);
}
    }
}
