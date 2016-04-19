﻿using System;
using Warden.Web.Core.Domain;

namespace Warden.Web.Core.Queries
{
    public class BrowseWardenIterations : PagedQueryBase
    {
        public string WatcherName { get; set; }
        public WatcherType? WatcherTypeName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public ResultType ResultType { get; set; }
    }

    public enum ResultType
    {
        All = 0,
        Valid = 1,
        Invalid = 2
    }
}