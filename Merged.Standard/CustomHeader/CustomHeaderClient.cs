/*
 * CustomHeaderSignature.Standard
 *
 * This file was automatically generated by APIMATIC v2.0 ( https://apimatic.io ).
 */
using System;
using CustomHeaderSignature.Standard.Controllers;
using Merged.Standard;
using Merged.Standard.Batester;

namespace CustomHeaderSignature.Standard.CustomHeader
{
    public sealed class CustomHeaderClient
    {
        /// <summary>
        /// Configuration instance
        /// </summary>
        private Configuration Config { get; }

        private readonly object syncObject = new Object();

        private CustomHeaderSignatureController customHeaderSignature;


        /// <summary>
        /// Provides access to CustomHeaderSignatureController controller
        /// </summary>
        public CustomHeaderSignatureController CustomHeaderSignature
        {
            get
            {
                if (customHeaderSignature == null)
                {
                    lock (syncObject)
                    {
                        if (customHeaderSignature == null)
                        {
                            customHeaderSignature = new CustomHeaderSignatureController(Config);
                        }
                    }
                }
                return customHeaderSignature;
            }
        }

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomHeaderClient(Configuration config)
        {
            this.Config = config;
        }
        #endregion
    }
}