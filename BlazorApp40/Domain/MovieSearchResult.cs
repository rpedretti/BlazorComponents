namespace BlazorApp40.Domain
{
    public class MovieSearchResult
    {
        public Movie[] Search { get; set; }
        public int totalResults { get; set; }
        public bool Response { get; set; }
    }
}
