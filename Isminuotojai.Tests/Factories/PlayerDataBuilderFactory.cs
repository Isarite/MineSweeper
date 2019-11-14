using Isminuotojai;
using Isminuotojai.Classes;
// <copyright file="PlayerDataBuilderFactory.cs">Copyright ©  2019</copyright>

using System;
using Microsoft.Pex.Framework;

namespace Isminuotojai.Classes
{
    /// <summary>A factory for Isminuotojai.Classes.PlayerDataBuilder instances</summary>
    public static partial class PlayerDataBuilderFactory
    {
        /// <summary>A factory for Isminuotojai.Classes.PlayerDataBuilder instances</summary>
        [PexFactoryMethod(typeof(App), "Isminuotojai.Classes.PlayerDataBuilder")]
        public static PlayerDataBuilder Create()
        {
            PlayerDataBuilder playerDataBuilder = new PlayerDataBuilder();
            return playerDataBuilder;

            // TODO: Edit factory method of PlayerDataBuilder
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
