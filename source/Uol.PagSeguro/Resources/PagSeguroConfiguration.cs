// Copyright [2011] [PagSeguro Internet Ltda.]
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System;
using System.Configuration;
using System.IO;
using System.Xml;
using Uol.PagSeguro.Configuration;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.XmlParse;
using System.Reflection;
using System.Diagnostics;

namespace Uol.PagSeguro.Resources
{
    /// <summary>
    /// 
    /// </summary>
    public static class PagSeguroConfiguration
    {

        private static string _urlXmlConfiguration = ".../.../Configuration/PagSeguroConfig.xml";

        /// <summary>
        /// Xml Configuration File Path
        /// </summary>
        public static string XmlConfigurationPath
        {
            get { return _urlXmlConfiguration; }
            set { _urlXmlConfiguration = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static AccountCredentials Credentials
        {
            get { return new AccountCredentials(_configurationSection.Credentials.Email, _configurationSection.Credentials.Token); }
            set
            {
                if (null == value) return;
                _configurationSection.Credentials.Email = value.Email;
                _configurationSection.Credentials.Token = value.Token;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ModuleVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string CmsVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string LanguageEngineDescription
        {
            get { return Environment.Version.ToString(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri NotificationUri
        {
            get { return new Uri(ConfigurationSection.Urls.Notification); }
            set { ConfigurationSection.Urls.Notification = value.ToString(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri PaymentUri
        {
            get { return new Uri(ConfigurationSection.Urls.Payment); }
            set { ConfigurationSection.Urls.Payment = value.ToString(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri PaymentRedirectUri
        {
            get { return new Uri(ConfigurationSection.Urls.PaymentRedirect); }
            set { ConfigurationSection.Urls.PaymentRedirect = value.ToString(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri SearchUri
        {
            get { return new Uri(ConfigurationSection.Urls.Search); }
            set { ConfigurationSection.Urls.Search = value.ToString(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int RequestTimeout
        {
            get { return ConfigurationSection.RequestTimeout; }
            set { ConfigurationSection.RequestTimeout = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string FormUrlEncoded
        {
            get { return ConfigurationSection.FormUrlEncoded; }
            set { ConfigurationSection.FormUrlEncoded = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string Encoding
        {
            get { return ConfigurationSection.Encoding; }
            set { ConfigurationSection.Encoding = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string LibVersion
        {
            get { return ConfigurationSection.LibVersion; }
            set { ConfigurationSection.LibVersion = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static XmlDocument LoadXmlConfig()
        {
            XmlDocument xml = new XmlDocument();
            using (xml as IDisposable)
            {
                xml.Load(XmlConfigurationPath);
            }
            return xml;
        }

        private static PagSeguroConfigurationSection _configurationSection;
        private static PagSeguroConfigurationSection ConfigurationSection
        {
            get
            {
                if (null != _configurationSection)
                {
                    return _configurationSection;
                }

                _configurationSection = ConfigurationManager.GetSection("pagSeguro") as PagSeguroConfigurationSection;
                if (null != _configurationSection)
                {
                    return _configurationSection;
                }

                if (File.Exists(XmlConfigurationPath))
                {
                    _configurationSection = new PagSeguroConfigurationSection(LoadXmlConfig());
                    return _configurationSection;
                }

                _configurationSection = new PagSeguroConfigurationSection();
                return _configurationSection;
            }
        }
    }
}
