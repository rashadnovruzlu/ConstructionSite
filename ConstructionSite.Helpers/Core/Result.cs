namespace ConstructionSite.Helpers.Core
{
    public class Result<T>
    {
        public T Data { get; set; }
        public bool IsResult { get; set; }
    }
}