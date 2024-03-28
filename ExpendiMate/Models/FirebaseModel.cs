using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth.Providers;
using Firebase.Auth;
using Microsoft.Maui.Graphics;

namespace ExpendiMate.Models
{
    public class FirebaseModel
    {

        public static FirebaseAuthConfig config = new FirebaseAuthConfig
        {
            ApiKey = "AIzaSyCdW9pq99-7wEpyI-MKjbDnkVxyjq9LX3U",
            AuthDomain = "courseplanner-192be.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
            {
                new EmailProvider()
            }
        };
    }
}