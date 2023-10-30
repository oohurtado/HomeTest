namespace Home.Source.Common
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Errors = new List<string>();
        }

        public bool Succeeded => Errors?.Count == 0;
        public List<string>? Errors { get; set; }
    }

    public class BaseResponse<T>
    {
        public BaseResponse()
        {
            Errors = new List<string>();
        }

        public T? Data { get; set; }
        public bool Succeeded => Data != null && Errors?.Count == 0;
        public List<string>? Errors { get; set; }
    }
}
