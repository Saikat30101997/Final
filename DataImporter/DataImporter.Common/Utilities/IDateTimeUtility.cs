using System;

namespace DataImporter.Common.Utilities
{
    public interface IDateTimeUtility
    {
        DateTime Now();
        DateTime NowWithTime { get; }
    }
}