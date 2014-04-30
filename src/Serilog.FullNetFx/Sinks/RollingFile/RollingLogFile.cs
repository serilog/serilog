using System;

namespace Serilog.Sinks.RollingFile
{
    class RollingLogFile
    {
        readonly string _filename;
        readonly DateTime _date;
        readonly int _sequenceNumber;

        public RollingLogFile(string filename, DateTime date, int sequenceNumber)
        {
            _filename = filename;
            _date = date;
            _sequenceNumber = sequenceNumber;
        }

        public string Filename
        {
            get { return _filename; }
        }

        public DateTime Date
        {
            get { return _date; }
        }

        public int SequenceNumber
        {
            get { return _sequenceNumber; }
        }
    }
}