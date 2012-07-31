#region (c) 2010-2012 Lokad - CQRS Sample for Windows Azure - New BSD License 

// Copyright (c) Lokad 2010-2012, http://www.lokad.com
// This code is released as Open Source under the terms of the New BSD Licence

#endregion

namespace NAsana.API.Tests.SimpleTests
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using PowerAssert;
    using Simple.Testing.Framework;

    [TestFixture]
    public abstract class specs
    {
        protected TestCaseData[] GetSpecifications()
        {
            var type = GetType();
            var runs = TypeReader.GetSpecificationsIn(type);
            return runs
                .Select(s => new TestCaseData(s).SetName(Name(s)))
                .ToArray();
        }

        static string Name(SpecificationToRun r)
        {
            return (r.Specification.GetName() ?? r.FoundOn.Name).CleanupName() + " ";
        }

        // helpers
        protected static DateTime DateUtc(int year, int month, int day)
        {
            return new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Unspecified);
        }

        [TestCaseSource("GetSpecifications")]
        public void _(SpecificationToRun spec)
        {
            var result = new SpecificationRunner().RunSpecifciation(spec);
            //todo: print detailed output
            //spec.Specification.Document(result);
            if (!result.Passed)
            {
                if (result.Thrown != null)
                {
                    throw result.Thrown;
                }
                Assert.Fail(result.Message ?? "<null>");
            }
        }

        protected static DateTime Date(int year, int month = 1, int day = 1, int hour = 0)
        {
            return new DateTime(year, month, day, hour, 0, 0, DateTimeKind.Unspecified);
        }

        protected static DateTime Time(int hour, int minute = 0, int second = 0)
        {
            return new DateTime(2011, 1, 1, hour, minute, second, DateTimeKind.Unspecified);
        }
    }
}