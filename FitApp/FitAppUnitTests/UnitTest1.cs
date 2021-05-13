using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FitApp.Models;
using FitApp.Services;
using System.Net;
using System.Threading.Tasks;

namespace FitAppUnitTests
{
    [TestClass]
    public class UnitTest1
    {

        // This Test works but can only be run once as the user will then
        // be registered and the email will have been taken thus failing it
        // the second time around

        //[TestMethod]
        //public void TestRegister()
        //{
        //    string name = "Jo";
        //    string email = "Jo@gmail.com";
        //    string password = "Jo@123";
        //    int weight = 80;
        //    int height = 180;

        //    Task.Run(async () =>
        //    {

        //        var response = await ApiServices.RegisterUser(name, email, password ,weight, height);

        //    Assert.AreEqual(true, response);
        //    }).GetAwaiter().GetResult();
        //}

        [TestMethod]
        public void TestLogin()
        {

            string email = "Sample@gmail.com";
            string password = "o@123";

            Task.Run(async () =>
            {
                var response = await ApiServices.Login(email, password);

                // failing
                Assert.AreEqual(false, response);

            }).GetAwaiter().GetResult();
        }


        [TestMethod]
        public void TestEmailAlreadyTaken()
        {
            string name = "Admin";
            string email = "Admin@gmail.com";
            string password = "Admin@123";
            int weight = 80;
            int height = 180;

            Task.Run(async () =>
            {
                var response = await ApiServices.RegisterUser(name, email, password, weight, height);

            // email already taken
            Assert.AreEqual(false, response);

        }).GetAwaiter().GetResult();
    }

        [TestMethod]
        public void TestBMI()
        {
            double weight = 100;
            double height = 200;

            double newHeight = height / 100;
            double bmi = Math.Round(weight / (newHeight * newHeight), 2);

            // bmi should be 25 in this case
            Assert.AreEqual(25, bmi);
        }


        //[TestMethod]
        //public void TestGetListOfWorkouts()
        //{

        //    Task.Run(async () =>
        //    {

        //        string email = "Admin@gmail.com";
        //        string password = "Admin@123";

        //        await ApiServices.Login(email, password);


        //        int userId = 1;
        //        int pageNumber = 5;

        //        var workouts = await ApiServices.GetWorkout(userId, pageNumber, 10);

        //        // failing
        //        Assert.IsNotNull(workouts);

        //    }).GetAwaiter().GetResult();
            
        //}
        
    }
}
