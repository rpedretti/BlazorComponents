﻿namespace RPedretti.Blazor.Components.Sample.Domain
{
    public class MovieSearchResult
    {
        #region Properties

        public bool Response { get; set; }
        public Movie[] Search { get; set; }
        public int totalResults { get; set; }

        #endregion Properties
    }
}
