using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CustomHeaderSignature.Standard;
using CustomHeaderSignature.Standard.Http.Client;
using CustomHeaderSignature.Tests.Helpers;
using CustomHeaderSignature.Standard.Models;

namespace CustomHeaderSignature.Tests
{
    [TestFixture]
    public class ControllerTestBase
    {
        //Test setup
        public const int REQUEST_TIMEOUT = 60;
        protected const double ASSERT_PRECISION = 0.1;
        public TimeSpan globalTimeout = TimeSpan.FromSeconds(REQUEST_TIMEOUT);

        protected Configuration config;

        protected HttpCallBack httpCallBackHandler;
        
        [OneTimeSetUp]
        public void SetUp()
        {
            httpCallBackHandler = new HttpCallBack();
            // Set Configuration parameters for test execution
            var builder = new Configuration.Builder();
            config = builder
                .WithEnvironment(Configuration.Environments.TESTING)
                .WithToken("Qaws2W233WedeRe4T56G6Vref2")
                .Build();
        }
    }
}