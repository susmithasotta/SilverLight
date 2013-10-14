using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using TrafficServiceReference;
using System.Threading;

namespace TrafficMonitorService
{
    public partial class TrafficMonitorService : ServiceBase
    {
        private System.Timers.Timer MonitorIntervalTimer;
        private ApplicationSettingsManager m_ApplicationSettings = new ApplicationSettingsManager();
        private const string APPLICATION_SETTINGS_FILE = "ApplicationSettings.xml";
        private int ServiceConsecutiveFailCount;

        public TrafficMonitorService()
        {
            InitializeComponent();
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            m_ApplicationSettings.ApplicationSettingsFile = Environment.CurrentDirectory + "\\" + APPLICATION_SETTINGS_FILE;
            m_ApplicationSettings.LoadApplicationSettings();

        }

        protected override void OnStart(string[] args)
        {
            try
            {
                WriteToEventLog("Traffic Monitoring Service", "Application", "Traffic Monitoring Service Started", EventLogEntryType.Information);

                Thread thread = new Thread(() => CheckService());
                thread.Start();
                MonitorIntervalTimer = new System.Timers.Timer(m_ApplicationSettings.MonitorIntervalSeconds * 1000);
                //if (DateTime.Now.Hour == 4)
                //{

                    // Set to run more than once 

                    MonitorIntervalTimer.AutoReset = true;

                    // Delegate to handle the timer event 

                    MonitorIntervalTimer.Elapsed += new System.Timers.ElapsedEventHandler(MonitorIntervalTimer_Elapsed);

                    // Start the timer 

                    MonitorIntervalTimer.Start();
                    // If the timer is declared in XliteClient long-running method, use 
                    // KeepAlive to prevent garbage collection from occurring 
                    // before the method ends. 
                    GC.KeepAlive(MonitorIntervalTimer);
                //}
            }
            catch (Exception Ex)
            {
                WriteToEventLog("Traffic Monitoring Service", "Application", "Start Error : " + Ex.Message, EventLogEntryType.Error);
            }

        }

        void MonitorIntervalTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Thread thread = new Thread(() => CheckService());
                thread.Start();
                System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
                m_ApplicationSettings.ApplicationSettingsFile = Environment.CurrentDirectory + "\\" + APPLICATION_SETTINGS_FILE;
                m_ApplicationSettings.LoadApplicationSettings();

            }
            catch (Exception)
            {
                WriteToEventLog("Traffic Monitoring Service", "Application", "Traffic Monitoring Timer Exception", EventLogEntryType.Error);

            }
        }

        private void CheckService()
        {
            try
            {
                using (TrafficServiceClient trafficServiceClient = new TrafficServiceClient())
                {
                    WriteToEventLog("Traffic Monitoring Service", "Application", "Traffic Monitoring CheckService() called", EventLogEntryType.Information);
                    bool result = trafficServiceClient.RefreshFlightCubeData();
                    WriteToEventLog("Traffic Monitoring Service", "Application", "Traffic Monitoring CheckService()Completed", EventLogEntryType.Information);
                    if (!result)
                    {
                        ServiceConsecutiveFailCount++;

                        if (ServiceConsecutiveFailCount > 3)
                        {
                            WriteToEventLog("Traffic Monitoring Service", "Application", "Traffic Service is down", EventLogEntryType.Error);
                            ServiceConsecutiveFailCount = 0;
                        }

                    }
                    else
                    {
                        ServiceConsecutiveFailCount = 0;
                    }
                }
            }
            catch (Exception Ex)
            {
                WriteToEventLog("Traffic Monitoring Service", "Application", "Traffic Monitoring CheckService() Exception" + Ex.Message, EventLogEntryType.Error);
            }
        }

        protected override void OnStop()
        {
            try
            {
                WriteToEventLog("Traffic Monitoring Service", "Application", "Traffic Monitoring Service Stopped", EventLogEntryType.Information);

            }
            catch (Exception Ex)
            {

            }

        }
       
        protected bool WriteToEventLog(string StrSource, string StrLog, string StrEventMessage, EventLogEntryType EntryType)
        {
            try
            {

                if (!EventLog.SourceExists(StrSource))
                    EventLog.CreateEventSource(StrSource, StrLog);

                EventLog.WriteEntry(StrSource, StrEventMessage);
                EventLog.WriteEntry(StrSource, StrEventMessage, EntryType);
                return true;

            }
            catch
            {
                return false;

            }
        }
    }
}
