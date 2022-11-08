using AutoFixture.Xunit2;
using RegistrationSystem.AccessData.Repositories;
using RegistrationSystem.Common.Interfaces.AccessData;

namespace RegistrationSystemTests.RegistrationSystem.AccessData
{
    public class AddressesRepositoryTests
    {
        private readonly AppDbTestContext _Db;
        private readonly IAccountsRepository _sut;
        public AddressesRepositoryTests ( )
        {
            _Db = new AppDbTestContext( );
            _sut = new AccountsRepository(_Db.Context);
        }

        [Theory,AutoData]
        public async Task GetAddressAsync_WhenAddressExists_ReturnAddress ( )
        {

        }
        [Fact]
        public async Task GetAddressAsync_WhenAddressNotEsists_ReturnNull ( )
        {

        }
        [Fact]
        public async Task GetAppartmentNumberAsync_WhenNumberExists_ReturnAaaprtmentNumber ( )
        {

        }
        [Fact]
        public async Task GetAppartmentNumberAsync_WhenNumberNotExists_ReturnNull ( )
        {

        }
        [Fact]
        public async Task GetCityAsync_WhenCityExists_ReturnCity ( )
        {

        }
        [Fact]
        public async Task GetCityAsync_WhenCityNotExists_ReturnNull ( )
        {

        }

        [Fact]
        public async Task GetHouseNumberAsync_WhenNumberExists_HouseNumber ( )
        {

        }
        [Fact]
        public async Task GetHouseNumberAsyncGetCityAsync_WhenNumberNotExists_ReturnNull ( )
        {

        }

        [Fact]
        public async Task GetStreetAsync_WhenStreetExits_ReturnStreet ( )
        {

        }

        [Fact]
        public async Task GetStreetAsync_WhenStreetNotExists_ReturnNull ( )
        {

        }
    }

}
