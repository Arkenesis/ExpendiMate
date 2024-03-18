//using ExpendiMate.Models;
//using ExpendiMate.ViewModels;
//using Firebase.Auth;
//using Firebase.Database;
//using Firebase.Database.Offline;
//using Firebase.Database.Query;

//namespace ExpendiMate.Services
//{
//    public static class FirebaseServices
//    {

        

//        private static RealtimeDatabase<ExpensesModel> _connection;

//        public static RealtimeDatabase<ExpensesModel> Connection
//        {
//            get
//            {
//                if (_connection == null)
//                {
//                    FirebaseOptions options = new FirebaseOptions()
//                    {
//                        OfflineDatabaseFactory = (t, s) => new OfflineDatabase(t, s),
//                        AuthTokenAsyncFactory = () => LoginAsync()
//                    };

//                    var client = new FirebaseClient("https://courseplanner-192be-default-rtdb.asia-southeast1.firebasedatabase.app/", options);
//                    _connection = client.Child("Expenses").AsRealtimeDatabase<ExpensesModel>("","", StreamingOptions.LatestOnly, InitialPullStrategy.MissingOnly, true);
//                }
                
//                return _connection;
//            }
//        }

//        public static async Task<string> LoginAsync()
//        {
//            var user = LoginViewModel.Current.User.User;
//            var token = await user.GetIdTokenAsync();
//            return token;
//        }

//        public static async Task<bool> AddItem(ExpensesModel item)
//        {
//            try
//            {
//                string key = _connection.Post(item);
//            }
//            catch (Exception ex)
//            {
//                return await Task.FromResult(false);
//            }

//            return await Task.FromResult(true);
//        }

//        public static async Task<bool> UpdateItem(string id, ExpensesModel item)
//        {
//            try
//            {
//                _connection.Put(id, item);
//            }
//            catch (Exception ex)
//            {
//                return await Task.FromResult(false);
//            }

//            return await Task.FromResult(true);
//        }

//        public static async Task<bool> DeleteItem(string id)
//        {
//            try
//            {
//                _connection.Delete(id);
//            }
//            catch (Exception ex)
//            {
//                return await Task.FromResult(false);
//            }

//            return await Task.FromResult(true);
//        }

//        public static async Task<ExpensesModel> GetItem(string id)
//        {
//            if (_connection.Database?.Count == 0)
//            {
//                try
//                {
//                    await _connection.PullAsync();
//                }
//                catch (Exception ex)
//                {
//                    return null;
//                }
//            }

//            bool success = _connection.Database.TryGetValue(id, out OfflineEntry offlineEntry);

//            var result = offlineEntry?.Deserialize<ExpensesModel>();

//            return await Task.FromResult(result);
//        }

//        public static async Task<IEnumerable<ExpensesModel>> GetAllItems(bool forceRefresh = false)
//        {
//            if (_connection.Database?.Count == 0)
//            {
//                try
//                {
//                    await _connection.PullAsync();
//                }
//                catch (Exception ex)
//                {
//                    return null;
//                }
//            }

//            var result = _connection
//                .Once()
//                .Select(x => x.Object);

//            return await Task.FromResult(result);
//        }


//    }
//}
