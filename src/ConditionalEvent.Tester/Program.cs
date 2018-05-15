using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConditionalEvents.Lib;
using ConditionalEvents.UsageTest;

namespace ConditionalEvent.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            TestClient client = new TestClient();

            client.Test.SomeAmount = 3;
            client.Test.FireEvents();

            client.Test.SomeAmount = 7;
            client.Test.FireEvents();

            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();
        }
    }

    public class TestClient
    {
        public TestClient()
        {
            _Test = new TestClass();
            _Test.ReportValueEvent += OnReportValueEvent;
            _Test.NewReportValueEvent += OnNewReportValueEvent;
        }

        void OnReportValueEvent(object sender, ReportValueEventArgs e)
        {
            Console.WriteLine("ReportValueEvent event fired with {0}.", e.Value);
        }

        void OnNewReportValueEvent(object sender, ReportValueEventArgs e)
        {
            Console.WriteLine("NewReportValueEvent event fired with {0}.", e.Value);
        }

        private TestClass _Test = null;

        public TestClass Test
        {
            get { return _Test; }
            set
            {
                if (_Test == value)
                    return;
                _Test = value;
            }
        }
    }

}
