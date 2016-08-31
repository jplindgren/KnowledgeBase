using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace MyKnowledge.Authentication {
    internal interface IAuthorizationManager {
        Task<IUser> FindAsync(object email, object password);        
    }
}