using System;
using System.Collections.Generic;
using System.Text;
using CustomHeaderSignature.Standard.Utilities;
using CustomHeaderSignature.Standard.Http.Client;
using CustomHeaderSignature.Standard.Models;

namespace CustomHeaderSignature.Standard
{
    public sealed class Configuration
    {
        /// <summary>
        /// Available environments
        /// </summary>
        public enum Environments
        {
            PRODUCTION,
            TESTING,
        }

        /// <summary>
        /// Available servers
        /// </summary>
        public enum Servers
        {
            ENUM_DEFAULT,
            AUTH_SERVER,
        }

        internal IHttpClient HttpClient { get; private set; }

        /// <summary>
        /// Http client timeout
        /// </summary>
        public TimeSpan Timeout { get; }

        public string Token { get; }

        /// <summary>
        /// Current API environment
        /// </summary>
        public Environments Environment { get; }

        /// <summary>
        /// Port value
        /// </summary>
        public string Port { get; }

        /// <summary>
        /// Suites value
        /// </summary>
        public Models.SuiteCodeEnum Suites { get; }

        //A map of environments and their corresponding servers/baseurls
        private static readonly Dictionary<Environments, Dictionary<Servers, string>> EnvironmentsMap =
            new Dictionary<Environments, Dictionary<Servers, string>>
        {
            {
                Environments.PRODUCTION, new Dictionary<Servers, string>
                {
                    { Servers.ENUM_DEFAULT, "http://apimatic.hopto.org:{suites}" },
                    { Servers.AUTH_SERVER, "http://apimaticauth.hopto.org:3000" },
                }
            },
            {
                Environments.TESTING, new Dictionary<Servers, string>
                {
                    { Servers.ENUM_DEFAULT, "http://localhost:3000" },
                    { Servers.AUTH_SERVER, "http://apimaticauth.xhopto.org:3000" },
                }
            },
        };

        /// <summary>
        /// Default constructor
        /// </summary>
        public Configuration()
        {
            HttpClient = new HttpClientWrapper();
            Timeout = TimeSpan.FromSeconds(0);
            Token = "TODO: Replace";
            Environment = Environments.TESTING;
            Port = "80";
            Suites = Models.SuiteCodeEnum.HEARTS;
        }

        /// <summary>
        /// Private constructor used to build an immutable Configuration object
        /// </summary>
        private Configuration(IHttpClient httpClient, TimeSpan timeout, string token, Environments environment, string port,
                Models.SuiteCodeEnum suites)
        { 
            this.HttpClient = httpClient;
            this.Timeout = timeout;
            this.Token = token;
            this.Environment = environment;
            this.Port = port;
            this.Suites = suites;
        }

        /// <summary>
        /// Makes a list of the BaseURL parameters 
        /// </summary>
        /// <return>Returns the parameters list</return>
        private List<KeyValuePair<string, object>> GetBaseURIParameters()
        {
            List<KeyValuePair<string, object>> kvpList = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("port", Port),
                new KeyValuePair<string, object>("suites", (int)Suites),
            };
            return kvpList; 
        }

        /// <summary>
        /// Gets the URL for a particular alias in the current environment and appends it with template parameters
        /// </summary>
        /// <param name="alias">Default value:DEFAULT</param>
        /// <return>Returns the baseurl</return>
        internal string GetBaseURI(Servers alias = Servers.ENUM_DEFAULT)
        {
            StringBuilder Url =  new StringBuilder(EnvironmentsMap[Environment][alias]);
            APIHelper.AppendUrlWithTemplateParameters(Url, GetBaseURIParameters());
            return Url.ToString();
        }

        public Builder ToBuilder() 
        {
            Builder builder = new Builder()
                .WithHttpClient(HttpClient)
                .WithTimeout(Timeout)
                .WithToken(Token)
                .WithEnvironment(Environment)
                .WithPort(Port)
                .WithSuites(Suites);
            return builder;
        }

        public class Builder
        {
            internal IHttpClient HttpClient { get; private set; }
            public TimeSpan Timeout { get; private set; } = TimeSpan.FromSeconds(0);
            public string Token { get; private set; } = "TODO: Replace";
            public Environments Environment { get; private set; } = Environments.TESTING;
            public string Port { get; private set; } = "80";
            public Models.SuiteCodeEnum Suites { get; private set; } = Models.SuiteCodeEnum.HEARTS;
            private bool createCustomHttpClient = false;

            // Setter for HttpClient
            public Builder WithHttpClient(IHttpClient httpClient)
            {
                this.HttpClient = httpClient;
                return this;
            }

            // Setter for timeout. Determines whether a new HttpClient is needed
            public Builder WithTimeout(TimeSpan timeout)
            { 
                if (this.Timeout != timeout && timeout.TotalSeconds > 0)
                {
                    this.Timeout = timeout;
                    this.createCustomHttpClient = true;
                }
                return this;
            }

            // Setter for Token
            public Builder WithToken(string token)
            {
                this.Token = token;
                return this;
            }

            // Setter for Environment
            public Builder WithEnvironment(Environments environment)
            {
                this.Environment = environment;
                return this;
            }

            // Setter for Port
            public Builder WithPort(string port)
            {
                this.Port = port;
                return this;
            }

            // Setter for Suites
            public Builder WithSuites(Models.SuiteCodeEnum suites)
            {
                this.Suites = suites;
                return this;
            }

            public Configuration Build()
            {
                if (createCustomHttpClient) 
                {
                    HttpClient = new HttpClientWrapper(Timeout);
                } 
                else 
                {
                    HttpClient = new HttpClientWrapper();
                }

                return new Configuration(HttpClient, Timeout, Token, Environment, Port, Suites);
            }
        }
    }
}