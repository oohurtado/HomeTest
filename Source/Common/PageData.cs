namespace Home.Source.Common
{
    public class PageData<T>
    {
        public List<T> Data { get; set; }
        public int GrandTotal { get; set; }
        public string Message { get; set; }


        public PageData(string message)
        {

            Message = message;
            Data = null!;
            GrandTotal = 0;

        }

        public PageData(List<T> data, int grandTotal)
        {
            Data = data;
            GrandTotal = grandTotal;
            Message = null!;
        }
    }
}
