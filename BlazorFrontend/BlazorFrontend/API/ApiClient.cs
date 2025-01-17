﻿using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorFrontend.API;

public class ApiClient
{
    private readonly HttpClient httpClient;
    public const string USER_API_ADRESS = "api/user";
    public const string TICKET_API_ADRESS = "api/ticket";


    public ApiClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        this.httpClient.BaseAddress = new Uri("https://localhost:7257"); 
    }



    public async Task GetQueryAsync<T>(string baseUrl, string endpoint, Func<T, Task> onSuccess, Action<string> onError)
    {
        try
        {
            var url = $"{httpClient.BaseAddress}{baseUrl}/{endpoint}";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<T>();

                if (data is null)
                {
                    throw new ArgumentException("Die angeforderten Daten konnten nicht empfangen werden.");
                }

                await onSuccess(data);
            }
            else
            {
                onError($"Fehler: {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            onError($"Exception: {ex.Message}");
        }
    }

    public async Task GetListQueryAsync<T>(string baseUrl, string endpoint, Func<List<T>, Task> onSuccess, Action<string> onError)
    {
        try
        {
            var url = $"{httpClient.BaseAddress}{baseUrl}/{endpoint}";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<T>>();

                if (data is null || data.Count == 0)
                {
                    throw new ArgumentException("Die angeforderten Daten konnten nicht empfangen werden oder die Liste ist leer.");
                }

                await onSuccess(data);
            }
            else
            {
                onError($"Fehler: {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            onError($"Exception: {ex.Message}");
        }
    }

}
