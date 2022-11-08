using RegistrationSystem.AccessData.Repositories;
using RegistrationSystem.Common.Interfaces.AccessData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystemTests.RegistrationSystem.AccessData
{
    public class UserInfoRepositoryTests
    {
        private readonly AppDbTestContext _Db;
        private readonly IAccountsRepository _sut;

        public UserInfoRepositoryTests ( )
        {
            _Db = new AppDbTestContext( );
            _sut = new AccountsRepository(_Db.Context);
        }
    }
}
