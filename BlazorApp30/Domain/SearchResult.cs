namespace BlazorApp30.Domain
{
    public class SearchResult
    {
        public Movie[] Search { get; set; }
        public int totalResults { get; set; }
        public bool Response { get; set; }
    }
}
