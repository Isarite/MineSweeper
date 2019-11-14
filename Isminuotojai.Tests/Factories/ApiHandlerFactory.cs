using Isminuotojai.Classes;
// <copyright file="ApiHandlerFactory.cs">Copyright ©  2019</copyright>

using System;
using Microsoft.Pex.Framework;

namespace Isminuotojai.Classes
{
    /// <summary>A factory for Isminuotojai.Classes.ApiHandler instances</summary>
    public static partial class ApiHandlerFactory
    {
        /// <summary>A factory for Isminuotojai.Classes.ApiHandler instances</summary>
        [PexFactoryMethod(typeof(ApiHandler))]
        public static object Create()
        {
            return ApiHandler.Instance;
            // TODO: Edit factory method of ApiHandler
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
