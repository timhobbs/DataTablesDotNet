using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace DataTablesDotNet.Mvc.Data {

    public class UserRepository : IRepository<User> {
        private List<User> data;

        public UserRepository(string dataPath) {
            data = new List<User>();

            LoadData(dataPath);
        }

        public User Get(int id) {
            return GetAll().AsQueryable()
                           .Where(d => d.Id == id)
                           .First();
        }

        public List<User> GetAll() {
            return data;
        }

        public List<User> GetAllWhere(Expression<Func<User, bool>> predicate) {
            return GetAll().AsQueryable()
                           .Where(predicate)
                           .ToList();
        }

        private void LoadData(string dataPath) {
            var file = new FileInfo(dataPath);
            string json = null;

            using (var reader = file.OpenText()) {
                json = reader.ReadToEnd();
            }

            data = JsonConvert.DeserializeObject<List<User>>(json);
        }
    }
}