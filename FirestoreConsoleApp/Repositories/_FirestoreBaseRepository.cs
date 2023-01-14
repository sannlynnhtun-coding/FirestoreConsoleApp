using Firestore.Interfaces;
using FirestoreConsoleApp.Models;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirestoreConsoleApp.Repositories
{
    public class FirestoreBaseRepository<T> : IBaseRepository<T>
    {
        private readonly Collection _collection;
        public FirestoreDb _firestoreDb;

        public FirestoreBaseRepository(Collection collection)
        {
            // This should live in the appsetting file and injected - This is just an example.
            _collection = collection;
            var filepath = @"csharp-firestore-2022-firebase-adminsdk-exxt6-d7e4fb29a6.json";
            string jsonStr = File.ReadAllText(filepath);
            FirebaseProjectSetting setting = JsonConvert.DeserializeObject<FirebaseProjectSetting>(jsonStr);

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            _firestoreDb = FirestoreDb.Create(setting.project_id);
        }

        /// <inheritdoc />
        public async Task<List<T>> GetAllAsync<T>() where T : IBaseFirestoreEntity
        {
            Query query = _firestoreDb.Collection(_collection.ToString());
            var querySnapshot = await query.GetSnapshotAsync();
            var list = new List<T>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (!documentSnapshot.Exists) continue;
                var data = documentSnapshot.ConvertTo<T>();
                if (data == null) continue;
                data.Id = documentSnapshot.Id;
                list.Add(data);
            }

            return list;
        }

        /// <inheritdoc />
        public async Task<object> GetAsync<T>(T entity) where T : IBaseFirestoreEntity
        {
            var docRef = _firestoreDb.Collection(_collection.ToString()).Document(entity.Id);
            var snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                var usr = snapshot.ConvertTo<T>();
                usr.Id = snapshot.Id;
                return usr;
            }

            return null;
        }

        /// <inheritdoc />
        public async Task<T> AddAsync<T>(T entity) where T : IBaseFirestoreEntity
        {
            var colRef = _firestoreDb.Collection(_collection.ToString());
            var doc = await colRef.AddAsync(entity);
            // GO GET RECORD FROM DATABASE:
            // return (T) await GetAsync(entity); 
            return entity;
        }

        /// <inheritdoc />
        public async Task<T> UpdateAsync<T>(T entity) where T : IBaseFirestoreEntity
        {
            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(entity));
            var recordRef = _firestoreDb.Collection(_collection.ToString()).Document(entity.Id);
            await recordRef.SetAsync(values);
            // GO GET RECORD FROM DATABASE:
            // return (T)await GetAsync(entity);
            return entity;
        }

        /// <inheritdoc />
        public async Task DeleteAsync<T>(T entity) where T : IBaseFirestoreEntity
        {
            var recordRef = _firestoreDb.Collection(_collection.ToString()).Document(entity.Id);
            await recordRef.DeleteAsync();
        }

        /// <inheritdoc />
        public async Task<List<T>> QueryRecordsAsync<T>(Query query) where T : IBaseFirestoreEntity
        {
            var querySnapshot = await query.GetSnapshotAsync();
            var list = new List<T>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (!documentSnapshot.Exists) continue;
                var data = documentSnapshot.ConvertTo<T>();
                if (data == null) continue;
                data.Id = documentSnapshot.Id;
                list.Add(data);
            }

            return list;
        }
    }

    public enum Collection
    {
        Blogs = 1
    }
}
