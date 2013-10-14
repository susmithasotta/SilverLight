using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Xml;

namespace TrafficMonitorService
{
    public class ApplicationSettingsManager
    {
        private const string SMTP_SERVER = "SMTPServer";
        private const string SMTP_PORT = "SMTPPort";
        private const string FROM_MAIL = "FromMail";
        private const string TO_MAIL = "ToMail";
        private const string CC_MAIL = "CCMail";
        private const string NETWORK_USERNAME = "NetworkUserName";
        private const string NETWORK_PASSWORD = "NetworkPassword";
        private const string MONITOR_INTERVAL_SECONDS = "MonitorIntervalSeconds";
        private const string AUTO_RESOLVE = "AutoResolve";

        string m_ApplicationSettingsFile = "";

        string m_DBConnectionString = "";

        string m_NetworkUserName = "";

        string m_NetworkPassword = "";

        string m_SMTPServer = "";

        int m_SMTPPort = 25;

        string m_FromMail = "";

        string m_ToMail = "";
        string m_CCMail = "";

        //Seconds
        int m_MonitorIntervalSeconds = 3;

        bool m_AutoResolve = false;

        public string ApplicationSettingsFile
        {
            get { return m_ApplicationSettingsFile; }

            set { m_ApplicationSettingsFile = value; }
        }


        public string SMTPServer
        {
            get { return m_SMTPServer; }

            set { m_SMTPServer = value; }
        }

        public int SMTPPort
        {
            get { return m_SMTPPort; }

            set { m_SMTPPort = value; }
        }

        public string FromMail
        {
            get { return m_FromMail; }

            set { m_FromMail = value; }
        }

        public string ToMail
        {
            get { return m_ToMail; }

            set { m_ToMail = value; }
        }

        public string CCMail
        {
            get { return m_CCMail; }

            set { m_CCMail = value; }
        }



        public string NetworkUserName
        {
            get { return m_NetworkUserName; }

            set { m_NetworkUserName = value; }
        }

        public string NetworkPassword
        {
            get { return m_NetworkPassword; }
            set { m_NetworkPassword = value; }
        }

        public int MonitorIntervalSeconds
        {
            get { return m_MonitorIntervalSeconds; }

            set { m_MonitorIntervalSeconds = value; }
        }

        public bool AutoResolve
        {
            get { return m_AutoResolve; }
            set { m_AutoResolve = value; }
        }



        public void LoadApplicationSettings()
        {

            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(m_ApplicationSettingsFile);
                XmlNodeList nodeList = null;
                XmlNode Appnode = document.GetElementsByTagName("ApplicationSettings").Item(0);
                nodeList = Appnode.ChildNodes;

                foreach (XmlNode node in nodeList)
                {
                    switch (node.Name)
                    {

                        case SMTP_SERVER:
                            m_SMTPServer = node.FirstChild.Value.ToString();
                            break;

                        case SMTP_PORT:
                            m_SMTPPort = Convert.ToInt32(node.FirstChild.Value);
                            break;

                        case FROM_MAIL:
                            m_FromMail = node.FirstChild.Value.ToString();
                            break;

                        case TO_MAIL:
                            m_ToMail = node.FirstChild.Value.ToString();
                            break;

                        case CC_MAIL:
                            m_CCMail = node.FirstChild.Value.ToString();
                            break;

                        case NETWORK_USERNAME:
                            m_NetworkUserName = node.FirstChild.Value.ToString();
                            break;


                        case NETWORK_PASSWORD:
                            m_NetworkPassword = node.FirstChild.Value.ToString();
                            break;

                        case MONITOR_INTERVAL_SECONDS:
                            m_MonitorIntervalSeconds = Convert.ToInt32(node.FirstChild.Value);
                            break;

                        case AUTO_RESOLVE:
                            m_AutoResolve = Convert.ToBoolean(node.FirstChild.Value);
                            break;
                    }
                }


            }
            catch
            {

            }
        }



    }
}
