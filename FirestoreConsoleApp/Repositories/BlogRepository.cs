using Firestore.Interfaces;
using FireStoreConsoleApp.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirestoreConsoleApp.Repositories
{
    public class BlogRepository
    {
        private readonly FirestoreBaseRepository<Blog> _repository;
        public BlogRepository()
        {
            _repository = new FirestoreBaseRepository<Blog>(Collection.Blogs);
        }

        public async Task<List<Blog>> GetAllAsync() => await _repository.GetAllAsync<Blog>();

        public async Task<Blog?> GetAsync(Blog entity) => (Blog?)await _repository.GetAsync(entity);

        public async Task<Blog> AddAsync(Blog entity) => await _repository.AddAsync(entity);

        public async Task<Blog> UpdateAsync(Blog entity) => await _repository.UpdateAsync(entity);

        public async Task DeleteAsync(Blog entity) => await _repository.DeleteAsync(entity);

        public async Task<List<Blog>> QueryRecordsAsync(Query query) => await _repository.QueryRecordsAsync<Blog>(query);
    }
}
