namespace RedBoarder.NetDataClient.Base
{
    public readonly struct HttpResponseResult<T>
    {
        public HttpResponseResult(T? responseObject, string responseText)
        {
            Object = responseObject;
            Text = responseText;
        }

        public T? Object { get; }

        public string Text { get; }
    }
}