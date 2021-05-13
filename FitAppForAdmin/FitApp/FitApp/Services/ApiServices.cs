using FitApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnixTimeStamp;
using Xamarin.Essentials;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;

namespace FitApp.Services
{
    class ApiServices
    {

        //Register user
        public static async Task<bool> RegisterUser(string name, string email, string password, double weight, double height)
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Password = password,
                Weight = weight,
                Height = height
            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(register);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/users/register", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Login user
        public static async Task<bool> Login(string email, string password)
        {
            var login = new Login()
            {
                Email = email,
                Password = password
            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/users/login", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Token>(jsonResult);
                Preferences.Set("accessToken", result.access_token);
                Preferences.Set("userId", result.user_id);
                Preferences.Set("userName", result.user_name);
                Preferences.Set("userEmail", result.user_email);
                Preferences.Set("userWeight", result.user_weight);
                Preferences.Set("userHeight", result.user_height);
                Preferences.Set("tokenExpirationTime", result.expiration_Time);
                Preferences.Set("currentTime", UnixTime.GetCurrentTime());
                return true;
            }

        }

        public static async Task<List<Exercise>> GetAllExercises(int pageNumber, int PageSize)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + String.Format("api/exercises?pageNumber={0}&PageSize={1}", pageNumber, PageSize));
            return JsonConvert.DeserializeObject<List<Exercise>>(response);
        }

        public static async Task<List<Users>> GetAllUsers(int pageNumber, int PageSize)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + String.Format("api/users/GetAllUsers?pageNumber={0}&PageSize={1}", pageNumber, PageSize));
            return JsonConvert.DeserializeObject<List<Users>>(response);
        }

        public static async Task<bool> DeleteUsers(int userId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.DeleteAsync(AppSettings.ApiUrl + String.Format("api/users/DeleteUser/" + userId));
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<bool> DeleteExercise(int exerciseId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.DeleteAsync(AppSettings.ApiUrl + String.Format("api/exercises/" + exerciseId));
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<bool> AddExercise(MediaFile mediaFile, ExerciseDetail exercise)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var content = new MultipartFormDataContent
            {
                {new StringContent(exercise.ExerciseName),"ExerciseName" },
                {new StringContent(exercise.Description),"Description" },
                {new StringContent(exercise.Difficulty),"Difficulty" },
                {new StringContent(exercise.TrailorUrl),"TrailorUrl" },

            };

            content.Add(new StreamContent(new MemoryStream(exercise.ImageArray)), "Image", mediaFile.Path);

            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/exercises", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }

    public static class TokenValidator
    {
        public static async Task CheckTokenValidity()
        {
            var expirationTime = Preferences.Get("tokenExpiraationTime", 0);
            Preferences.Set("currentTime", UnixTime.GetCurrentTime());
            var currentTime = Preferences.Get("currentTime", 0);
            if (expirationTime < currentTime)
            {
                var email = Preferences.Get("email", string.Empty);
                var password = Preferences.Get("password", string.Empty);
                await ApiServices.Login(email, password);
            }
        }
    }
}
