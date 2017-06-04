using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NekoUchi.DAL;
using NekoUchi.DAL.Helpers;
using NekoUchi.BLL.Helpers;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace NekoUchi.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SubscribeUserToCourse()
        {
            string email = "yetione.snowboard@yahoo.com";
            string courseID = "59148b099284641eacffcc7f";

            IDataProvider data = new MongoDataProvider();

        }

        [TestMethod]
        public void GetUsersCourses()
        {
            string email = "iandri25@hotmail.com";
            IDataProvider data = new MongoDataProvider();
            var course = BLL.Course.GetUsersCourses(email);
            foreach (var enumCourse in course.ModelCourses)
            {
                Assert.AreEqual(enumCourse.Author, email);
            }
        }

        [TestMethod]
        public void GetAllCourses()
        {
            IDataProvider data = new MongoDataProvider();
            var enumerableCourses = BLL.Course.GetAllCourses();
            Assert.AreEqual(enumerableCourses.ModelCourses.Count, 1);
        }

        [TestMethod]
        public void CheckAny()
        {
            IDataProvider data = new MongoDataProvider();
            var course = new BLL.Course();
            var enumerableCourses = data.GetMultipleUsingAny<DAL.Course>("Subscribed", "jadesmadness@yahoo.com");
            course.ModelCourses = new List<Model.Course>();
            foreach (var enumerableCourse in enumerableCourses)
            {
                course.ModelCourses.Add(enumerableCourse);
            }
        }

        [TestMethod]
        public void GetBllUser()
        {
            var bllUser = BLL.User.Get("iandri25@hotmail.com");
            //var user = Mongo.Get<BLL.User>(u => u.Email == "iandri25@hotmail.com");
            Assert.AreEqual(bllUser.ModelUser.Email, "iandri25@hotmail.com");
        }

        [TestMethod]
        public void GetDALUser()
        {
            IDataProvider data = new MongoDataProvider();
            Model.User ModelUser = data.Get<DAL.User>("Email", "iandri25@hotmail.com");
            Assert.AreEqual(ModelUser.Email, "iandri25@hotmail.com");
        }

        [TestMethod]
        public void CreateSession()
        {
            var session = new Session();
            session.Email = "Something@something.com";
            session.Role = "User";
            session.TTE = DateTime.Now + TimeSpan.FromDays(1);
            session.Create();
        }

        [TestMethod]
        public void ComparePasswordHashes()
        {
            string pass1 = "Password";
            string pass2 = "OtherPassword123";

            string salt = Randoms.GenerateSalt(8);

            string hash1 = Hasher.GetHash(pass1, salt);
            string hash2 = Hasher.GetHash(pass2, salt);

            string ctrlHash1 = Hasher.GetHash(pass1, salt);
            string ctrlHash2 = Hasher.GetHash(pass2, salt);

            Assert.AreEqual(hash1, ctrlHash1);
            Assert.AreEqual(hash2, ctrlHash2);
        }

        [TestMethod]
        public void CheckPass()
        {
            // "Save to DB", basically generating the string which will go to DB
            string password = "P@$$w0rd";
            //string fakePassword = "p@$$w0rd";
            string salt = Randoms.GenerateSalt(8);
            string hash = Hasher.GetHash(password, salt);
            //string saltString = Encoding.UTF8.GetString(salt);
            string saltLength = salt.Length.ToString();
            string passwordSaved = saltLength + ':' + salt + hash;

            // Now retrieveing it from DB and checking
            string[] passwordFromDB = passwordSaved.Split(new char[] { ':' }, 2);
            int DBSaltLength = Convert.ToInt32(passwordFromDB[0]);
            string DBSalt = passwordFromDB[1].Substring(0, DBSaltLength);
            byte[] saltFromDB = Encoding.UTF8.GetBytes(DBSalt);
            string hashFromDB = passwordFromDB[1].Substring(DBSaltLength);
            //string hashOfFake = Hasher.PBKDF2(fakePassword, saltFromDB);

            //Assert.AreNotEqual(hashFromDB, hashOfFake);
            Assert.AreEqual(hashFromDB, hash);
        }

        [TestMethod]
        public void GetSalt()
        {
            var rndNumGen = RandomNumberGenerator.Create();
            byte[] salt = new byte[8];
            rndNumGen.GetBytes(salt);
            string stringySalt = Convert.ToBase64String(salt);

            //var user = new User();
            //user.Email = "nijeBitno";
            //user.RegistrationDate = DateTime.Now;
            //user.Basil = stringySalt;
            //user.Password = Hasher.GetHash("pass", stringySalt);
            //user.Create();

            //var sameUser = new User();
            //sameUser.Email = "nijeBitno";
            //sameUser.Get();
            //string pass = Hasher.GetHash("pass", sameUser.Basil);
            //Assert.AreEqual(sameUser.Password, pass);
        }
    }
}
