using System;
using System.Threading.Tasks;

namespace InstationFinalVersion
{
    public interface IAuth
    {
        Task<string> LoginWithEmailPassword(string email, string password);
        bool SignUpWithEmailPassword(string email, string password);
    }
}