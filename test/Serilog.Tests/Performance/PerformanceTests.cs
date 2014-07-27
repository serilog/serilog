using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Serilog;

[TestFixture]
public class PerformanceTests
{
    [Test]
    [Explicit]
    public void RealWorld()
    {
        var persons = new List<Person>();
        for (var i = 0; i < 100000; i++)
        {

            var person = new Person
            {
                FirstName = "John",
                LastName = "smith",
                Age = 25,
                Address = new Address
                {
                    StreetAddress = "21 2nd Street",
                    PostalCode = 10021,
                    State = "NY",
                    City = "New York"
                },
                PhoneNumbers = new List<PhoneNumber>
                {
                    new PhoneNumber
                    {
                        Type = PhoneNumberType.Home,
                        Number = "232 423 234"
                    },
                    new PhoneNumber
                    {
                        Type = PhoneNumberType.Fax,
                        Number = "534 234 423"
                    }
                }

            };
            persons.Add(person);
        }

        var tempFileName = Path.GetTempFileName();
        ILogger logger = null;
        try
        {
            logger = new LoggerConfiguration()
                .WriteTo.File(tempFileName)
                .CreateLogger();
            foreach (var person in persons)
            {
                logger.Information("Foo from {@Person}", person);
            }
        }
        finally
        {
            var disposable = (IDisposable) logger;
            if (disposable != null)
            {
                disposable.Dispose();
            }
            File.Delete(tempFileName);
        }
    }

    public enum PhoneNumberType
    {
        Fax,
        Home
    }


    public class Address
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
    }

    public class PhoneNumber
    {
        public PhoneNumberType Type { get; set; }
        public string Number { get; set; }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
    }

}
