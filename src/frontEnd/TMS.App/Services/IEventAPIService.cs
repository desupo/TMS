using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TMS.App.Domain;

namespace TMS.App.Services;

public interface IEventAPIService
{
    Task UploadEventsAsync(IFormFile file);
    Task<IEnumerable<RailcarTripModel>> GetAllTripsAsync();
    Task<RailcarTripModel> GetTripAsync(long tripId);
    Task<IEnumerable<RailcarTripModel>> GetTripsByEquipmentIdAsync(string equipmentId);
}

public class EventApiService(HttpClient httpClient) : IEventAPIService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task UploadEventsAsync(IFormFile file)
    {
        using var content = new MultipartFormDataContent();
        var fileContent = new StreamContent(file.OpenReadStream());
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
        content.Add(fileContent, "file", file.FileName);

        var response = await _httpClient.PostAsync("/api/events/upload", content);
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<RailcarTripModel>> GetAllTripsAsync()
    {
        var response = await _httpClient.GetAsync("/api/trips");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<RailcarTripModel>>();
    }

    public async Task<RailcarTripModel> GetTripAsync(long tripId)
    {
        var response = await _httpClient.GetAsync($"/api/trips/{tripId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<RailcarTripModel>();
    }

    public async Task<IEnumerable<RailcarTripModel>> GetTripsByEquipmentIdAsync(string equipmentId)
    {
        var response = await _httpClient.GetAsync($"/api/trips/equipment/{equipmentId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<RailcarTripModel>>();
    }
}