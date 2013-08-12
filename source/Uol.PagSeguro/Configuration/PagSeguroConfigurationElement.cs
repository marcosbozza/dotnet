using System;
using System.Configuration;
using System.Xml;
using Uol.PagSeguro.XmlParse;

namespace Uol.PagSeguro.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class PagSeguroConfigurationSection : ConfigurationSection
    {
        public PagSeguroConfigurationSection()
        {
            Credentials = new CredentialsElement();
            Urls = new UrlsElement();
        }

        public PagSeguroConfigurationSection(XmlDocument xml)
        {
            var credentials = PagSeguroConfigSerializer.GetAccountCredentials(xml);
            Credentials = new CredentialsElement
                {
                    Email = credentials.Email,
                    Token = credentials.Token
                };


            Urls = new UrlsElement
                {
                    Payment = PagSeguroConfigSerializer.GetWebserviceUrl(xml, PagSeguroConfigSerializer.Payment),
                    Notification = PagSeguroConfigSerializer.GetWebserviceUrl(xml, PagSeguroConfigSerializer.Notification),
                    Search = PagSeguroConfigSerializer.GetWebserviceUrl(xml, PagSeguroConfigSerializer.Search),
                    PaymentRedirect = PagSeguroConfigSerializer.GetWebserviceUrl(xml, PagSeguroConfigSerializer.PaymentRedirect)
                };

            RequestTimeout = Convert.ToInt32(PagSeguroConfigSerializer.GetDataConfiguration(xml, PagSeguroConfigSerializer.RequestTimeout));
            FormUrlEncoded = PagSeguroConfigSerializer.GetDataConfiguration(xml, PagSeguroConfigSerializer.FormUrlEncoded);
            Encoding = PagSeguroConfigSerializer.GetDataConfiguration(xml, PagSeguroConfigSerializer.Encoding);
        }

        [ConfigurationProperty("credentials", IsRequired = true)]
        public CredentialsElement Credentials
        {
            get { return (CredentialsElement)this["credentials"]; }
            set { this["credentials"] = value; }
        }

        [ConfigurationProperty("urls", IsRequired = true)]
        public UrlsElement Urls
        {
            get { return (UrlsElement)this["urls"]; }
            set { this["urls"] = value; }
        }

        [ConfigurationProperty("formUrlEncoded", IsRequired = true)]
        public String FormUrlEncoded
        {
            get { return (String)this["formUrlEncoded"]; }
            set { this["formUrlEncoded"] = value; }
        }

        [ConfigurationProperty("encoding", IsRequired = true)]
        public String Encoding
        {
            get { return (String)this["encoding"]; }
            set { this["encoding"] = value; }
        }

        [ConfigurationProperty("requestTimeout", IsRequired = true)]
        public int RequestTimeout
        {
            get { return (int)this["requestTimeout"]; }
            set { this["requestTimeout"] = value; }
        }

        [ConfigurationProperty("libVersion", IsRequired = true)]
        public string LibVersion
        {
            get { return (string)this["libVersion"]; }
            set { this["libVersion"] = value; }
        }
    }

    public class CredentialsElement : ConfigurationElement
    {
        [ConfigurationProperty("email", IsRequired = true)]
        public String Email
        {
            get { return (String)this["email"]; }
            set { this["email"] = value; }
        }

        [ConfigurationProperty("token", IsRequired = true)]
        public String Token
        {
            get { return (String)this["token"]; }
            set { this["token"] = value; }
        }
    }

    public class UrlsElement : ConfigurationElement
    {
        [ConfigurationProperty("payment", IsRequired = true)]
        public String Payment
        {
            get { return (String)this["payment"]; }
            set { this["payment"] = value; }
        }

        [ConfigurationProperty("paymentRedirect", IsRequired = true)]
        public String PaymentRedirect
        {
            get { return (String)this["paymentRedirect"]; }
            set { this["paymentRedirect"] = value; }
        }

        [ConfigurationProperty("notification", IsRequired = true)]
        public String Notification
        {
            get { return (String)this["notification"]; }
            set { this["notification"] = value; }
        }

        [ConfigurationProperty("search", IsRequired = true)]
        public String Search
        {
            get { return (String)this["search"]; }
            set { this["search"] = value; }
        }
    }
}