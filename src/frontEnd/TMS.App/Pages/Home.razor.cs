using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.Net.Http.Json;
using System.Text.Json;
using TMS.App.Domain;
using TMS.App.Services;

namespace TMS.App.Pages;

public partial class Home : ComponentBase
{
    private List<RailcarTripModel> trips = new();
    private RailcarTripModel selectedTrip;
    private bool isUploading = false;
    private string errorMessage;

    [Inject]
    public IEventAPIService _EventApiService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private async Task OnFileUpload(InputFileChangeEventArgs e)
    {
        isUploading = true; // Show loading indicator

        try
        {
            var file = e.File;

            // Convert IBrowserFile to IFormFile
            var formFile = await ConvertToIFormFileAsync(file);

            await _EventApiService.UploadEventsAsync(formFile);

            // Refresh the trips list
            trips = (await _EventApiService.GetAllTripsAsync()).ToList();
        }
        catch (Exception ex)
        {
            // Handle any errors (e.g., show an error message)
            Console.Error.WriteLine($"Error uploading file: {ex.Message}");
            errorMessage = ex.Message;
        }
        finally
        {
            isUploading = false; // Hide loading indicator
        }
    }

    private async Task<IFormFile> ConvertToIFormFileAsync(IBrowserFile browserFile)
    {
        var buffer = new byte[browserFile.Size];
        await browserFile.OpenReadStream().ReadAsync(buffer);

        return new FormFile(new MemoryStream(buffer), 0, buffer.Length, browserFile.Name, browserFile.Name)
        {
            Headers = new HeaderDictionary(),
            ContentType = browserFile.ContentType
        };
    }

    private void ViewTripEvents(RailcarTripModel trip)
    {
        var tripJson = JsonSerializer.Serialize(trip);

        NavigationManager.NavigateTo($"/trip-events/{trip.TripId}", new NavigationOptions
        {
            HistoryEntryState = tripJson
        });
    }



    private async Task UploadFile(InputFileChangeEventArgs e)
    {
        var file = ((InputFileChangeEventArgs)e).File;
        if (file is not null)
        {
            isUploading = true;
            var content = new MultipartFormDataContent
                {
                    { new StreamContent(file.OpenReadStream()), "file", file.Name }
                };
            var response = await Http.PostAsync("/api/trips/upload", content);
            if (response.IsSuccessStatusCode)
            {
                trips = await response.Content.ReadFromJsonAsync<List<RailcarTripModel>>();
            }
            isUploading = false;
        }
    }

    private void ViewTripDetails(RailcarTripModel trip)
    {
        selectedTrip = trip;
    }

  
}
