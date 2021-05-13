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

namespace FitApp.Services
{
    public class ApiServices
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

        public static async Task<List<Workouts>> GetWorkout(int Id,int pageNumber, int PageSize)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + String.Format("api/workouts/FindWorkouts?UserId={0}&pageNumber={1}&PageSize={2}", Id, pageNumber, PageSize));
            return JsonConvert.DeserializeObject<List<Workouts>>(response);
        }

        public static async Task<WorkoutDetail> GetWorkoutDetail(int workoutId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + String.Format("api/workouts/" + workoutId));
            return JsonConvert.DeserializeObject<WorkoutDetail>(response);
        }

        public static async Task<bool> AddWorkout(WorkoutDetail workout)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(workout);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/workouts/", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<bool> DeleteWorkout(int workoutId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.DeleteAsync(AppSettings.ApiUrl + String.Format("api/workouts/" + workoutId));
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
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
        public static async Task<ExerciseDetail> GetExerciseDetail(int exerciseId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + String.Format("api/exercises/" + exerciseId));
            return JsonConvert.DeserializeObject<ExerciseDetail>(response);
        }

        public static async Task<User> GetUserDetail(int userId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + String.Format("api/users/GetUser/" + userId));
            return JsonConvert.DeserializeObject<User>(response);
        }

        public static async Task<bool> UpdateUser(int userId, User user)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.PutAsync(AppSettings.ApiUrl + "api/users/updateuser/" + userId, content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<List<Nutrient>> GetNutrients(string query)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(query);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Add("x-app-id", "b84864bd");
            httpClient.DefaultRequestHeaders.Add("x-app-key", "83eca09271c6a0d9b61c0fbefc1ad9c2");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.PostAsync("https://trackapi.nutritionix.com/v2/natural/nutrients", content);
            string s = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Nutrient>>(s);
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
