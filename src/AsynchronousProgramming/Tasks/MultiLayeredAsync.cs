using System.Text.Json;

namespace AsynchronousProgramming.Tasks
{
    /// <summary>
    /// Contains the multilayered async operation.
    /// </summary>
    internal class MultiLayeredAsync
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiLayeredAsync"/> class.
        /// </summary>
        /// <param name="client">Client object</param>
        public MultiLayeredAsync(HttpClient client)
        {
            this._httpClient = client;
        }

        /// <summary>
        /// Gets the length of the key value pair.
        /// </summary>
        /// <returns>A asynchronous task parse the response form the url.</returns>
        public async Task<int> MethodC()
        {
            string response = await this.MethodB();
            Console.WriteLine(response);
            using JsonDocument json = JsonDocument.Parse(response);
            JsonElement root = json.RootElement;
            return root.EnumerateObject().Count();
        }

        private async Task<string> MethodB()
        {
            long value = await Task.Run(() => this.MethodA());
            int todoId = (int)(value % 100) + 1;
            string url = $"https://jsonplaceholder.typicode.com/todos/{todoId}";
            return await this._httpClient.GetStringAsync(url);
        }

        private long MethodA()
        {
            long result = 0;
            for (int i = 0; i < 10_000; i++)
            {
                result += i;
            }

            return result;
        }
    }
}
