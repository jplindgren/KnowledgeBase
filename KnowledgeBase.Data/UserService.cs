using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data {
    public class UserService {
        private IDatasource<CustomUser> datasource;
        public UserService(IDatasource<CustomUser> dataSource) {
            this.datasource = dataSource;
        }

        public CustomUser Login(string email, string password) {
            using (MD5 md5Hash = MD5.Create()) {
                var hashedPassword = GetMd5Hash(md5Hash, password);
                var users = this.datasource.Load();
                var user = users.Where(x => x.Email == email && x.Password == hashedPassword).FirstOrDefault();
                return user;
            }
        }

        public CustomUser Register(Guid userId, string name, string email, string password) {
            using(MD5 md5Hash = MD5.Create()){
                CustomUser user = new CustomUser() {
                    Id = userId,
                    Email = email,
                    Name = name,
                    Password = GetMd5Hash(md5Hash, password)
                };

                this.datasource.Save(user);
                return user;
            }            
        }

        public string GetMd5Hash(MD5 md5Hash, string input) {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++) {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public bool VerifyMd5Hash(MD5 md5Hash, string input, string hash) {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash)) {
                return true;
            } else {
                return false;
            }
        }
    } //class
}
