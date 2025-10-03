namespace BlazorClient.Client
{
    using System.Net.Http.Json;

    public class ApiClient
    {
        private readonly HttpClient _http;

        public ApiClient(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("api");
        }

        public Task<string[]?> GetFilesAsync(CancellationToken cancellation = default)
            => _http.GetFromJsonAsync<string[]>("api/PairOfEmployees/files", cancellation);

        public async Task<int> UploadAsync(string fileName, CancellationToken cancellation = default)
        {
            using var content = new StringContent("");
            var resp = await _http.PostAsync($"api/PairOfEmployees/upload?file={Uri.EscapeDataString(fileName)}", content, cancellation);
            resp.EnsureSuccessStatusCode();
            var s = await resp.Content.ReadAsStringAsync(cancellation);
            return int.TryParse(s, out var n) ? n : 0;
        }

        public Task<List<PairOfEmployeesRequest>?> GetPreviewAsync(CancellationToken cancellation = default)
            => _http.GetFromJsonAsync<List<PairOfEmployeesRequest>>("api/PairOfEmployees/preview", cancellation);

        public Task<LongestPairResult?> GetTopPairAsync(CancellationToken cancellation = default)
            => _http.GetFromJsonAsync<LongestPairResult>("api/PairOfEmployees/longest-pair", cancellation);
    }

    // DTOs to map WebAPI JSON
    public sealed class PairOfEmployeesRequest
    {
        public int EmpId { get; set; }
        public int ProjectId { get; set; }
        public string DateFrom { get; set; } = "";
        public string DateTo { get; set; } = "";
    }

    public sealed class LongestPairResult
    {
        public int EmpId1 { get; set; }
        public int EmpId2 { get; set; }
        public int ProjectId { get; set; }
        public int TimeCoop { get; set; }
    }

}
