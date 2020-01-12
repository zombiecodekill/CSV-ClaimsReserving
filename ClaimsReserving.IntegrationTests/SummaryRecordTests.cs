using System.Collections.Generic;
using System.IO;
using ClaimsReserving.Models;
using ClaimsReserving.Services.Writer;
using CsvHelper;
using NUnit.Framework;

namespace ClaimsReserving.IntegrationTests
{
    [TestFixture]
    public class SummaryRecordTests
    {
        private SummaryRecord _target;

        [SetUp]
        public void Setup()
        {
            _target = new SummaryRecord();
        }

        [Test]
        public void WritesToFile()
        {
            CsvWriter csv = new CsvWriter(new StreamWriter("D:\\WTWDIP\\ClaimsReserving\\ClaimsReserving\\OutputFiles\\IntegrationTests-SummaryRecord.csv"));
            _target.Write(csv, new List<YearlyData>());
        }


    }
}
