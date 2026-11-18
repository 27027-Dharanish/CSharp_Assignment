namespace AsynchronousProgramming.Tasks
{
    /// <summary>
    /// Contains the implementation of downloading from http client.
    /// </summary>
    internal class AsyncAwait
    {
        private readonly HttpClient _httpsClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncAwait"/> class.
        /// </summary>
        /// <param name="client">Client object</param>
        public AsyncAwait(HttpClient client)
        {
            this._httpsClient = client;
        }

        /// <summary>
        /// Fetches the content from the url.
        /// </summary>
        /// <returns>A asynchronous operation, that download content from the url.</returns>
        public async Task<string> DownloadContentAsync()
        {
            string content = await this._httpsClient.GetStringAsync("https://www.example.com");
            return content;
        }
    }
}
