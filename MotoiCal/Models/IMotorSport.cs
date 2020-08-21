﻿using System.Collections.Generic;

namespace MotoiCal.Models
{
    public interface IMotorSport
    {
        MotorSportID SportIdentifier { get; }
        string FilePath { get; }
        string Url { get; }
        string UrlPartial { get; }
        string UrlPath { get; }
        string UrlAttribute { get; }
        string EventTablePath { get; }
        string SeriesNamePath { get; }
        string SessionNamePath { get; }
        string GrandPrixNamePath { get; }
        string SponserNamePath { get; }
        string LocationNamePath { get; }
        string StartDatePath { get; }
        string StartDateAttribute { get; }
        string EndDatePath { get; }
        string EndDateAttribute { get; }
        string GMTOffset { get; }

        List<string> EventUrlList { get; set; }
        string[] ExcludedUrls { get; }
        string[] ExcludedClasses { get; }
        List<string> ExcludedEvents { get; set; }
        string[] ExcludedWords { get; }

        string CheckForExcludedWords(string stringToCheck);
    }
}