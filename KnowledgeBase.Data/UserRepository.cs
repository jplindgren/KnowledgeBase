using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data {
    public class UserService {
        private IDatasource<CustomUser> datasource;
        public UserService(IDatasource<CustomUser> dataSource) {
            this.datasource = dataSource;
        }

        public CustomUser Login(string email, string password) {
            var users = this.datasource.Load();
            var user = users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
            return user;
        }

        public CustomUser Register(Guid userId, string name, string email, string password) {
            CustomUser user = new CustomUser() {
                Id = userId,
                Email = email,
                Name = name,
                Password = password
            };

            this.datasource.Save(user);
            return user;
        }
    } //class
}
