using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConditionalEvents.Lib;

namespace ConditionalEvents.UsageTest
{
    public class TestClass
    {
        public TestClass()
        {
            _NewReportValueEvent = EventManager.RegisterEvent<ReportValueEventArgs>().WithCondition(() =>
            {
                return (SomeAmount > 5);
            });
        }
        
        private int _SomeAmount = 0;

        #region conventional

        private event EventHandler<ReportValueEventArgs> _ReportValueEvent;

        private List<EventHandler<ReportValueEventArgs>> _ReportValueEventSubscribers =
            new List<EventHandler<ReportValueEventArgs>>();

        public event EventHandler<ReportValueEventArgs> ReportValueEvent
        {
            add
            {
                if (!_ReportValueEventSubscribers.Contains(value))
                {
                    _ReportValueEvent += value;
                    _ReportValueEventSubscribers.Add(value);
                }
            }
            remove
            {
                _ReportValueEvent -= value;
                _ReportValueEventSubscribers.Remove(value);
            }
        }

        #endregion
        
        private ConditionalEvent<ReportValueEventArgs> _NewReportValueEvent = null;

        public event EventHandler<ReportValueEventArgs> NewReportValueEvent
        {
            add { EventManager.AttachHandler(_NewReportValueEvent, value); }
            remove { EventManager.DetachHandler(_NewReportValueEvent, value); }
        }

        protected virtual void OnReportValueEvent(ReportValueEventArgs e)
        {
            if (this._ReportValueEvent != null)
                this._ReportValueEvent(this, e);
        }

        protected virtual void OnNewReportValueEvent(ReportValueEventArgs e)
        {
            EventManager.RaiseEvent(_NewReportValueEvent, this, e);
        }

        public void FireEvents()
        {
            OnReportValueEvent(new ReportValueEventArgs(SomeAmount));
            OnNewReportValueEvent(new ReportValueEventArgs(SomeAmount));
        }

        public int SomeAmount
        {
            get { return _SomeAmount; }
            set
            {
                if (_SomeAmount == value)
                    return;

                _SomeAmount = value;
            }
        }
    }
}
