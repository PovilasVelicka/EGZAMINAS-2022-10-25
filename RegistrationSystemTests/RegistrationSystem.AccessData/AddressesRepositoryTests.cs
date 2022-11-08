using AutoFixture.Xunit2;
using Common.DTOs;
using RegistrationSystem.AccessData.Repositories;
using RegistrationSystem.Common.Interfaces.AccessData;
using RegistrationSystem.Entities.Enums;
using RegistrationSystem.Entities.Models;
using RegistrationSystem.Entities.Models.AccountProperties;
using System.Runtime.Versioning;

namespace RegistrationSystemTests.RegistrationSystem.AccessData
{
    [SupportedOSPlatform("windows")]
    public class AddressesRepositoryTests
    {
        private readonly AppDbTestContext _Db;

        private readonly IAddressesRepository _sut;
        public AddressesRepositoryTests ( )
        {
            _Db = new AppDbTestContext("AddressesRepositoryTestsDb");

            _sut = new AddressesRepository(_Db.Context);
        }


        [Fact]
        public async Task GetAddressAsync_WhenAddressExists_ReturnAddress ( )
        {
            var address = CreateDbAccount( ).UserInfo.Address;

            var returnedAddress = await _sut.GetAddressAsync(
                address.City.Value, address.Street.Value,
                address.HouseNumber.Value, address.AppartmentNumber.Value);
            Assert.NotNull(returnedAddress);
            Assert.Equal(address.City.Value, returnedAddress.City.Value);
            Assert.Equal(address.Street.Value, returnedAddress.Street.Value);
            Assert.Equal(address.HouseNumber.Value, returnedAddress.HouseNumber.Value);
            Assert.Equal(address.AppartmentNumber.Value, returnedAddress.AppartmentNumber.Value);
        }

        [Theory, AutoData]
        public async Task GetAddressAsync_WhenAddressNotEsists_ReturnNull (string city, string street, string houseNumber, string appartmentNumber)
        {
            var returnedAddress = await _sut.GetAddressAsync(
               city, street, houseNumber, appartmentNumber);
            Assert.Null(returnedAddress);
        }

        [Fact]
        public async Task GetAppartmentNumberAsync_WhenNumberExists_ReturnAaaprtmentNumber ( )
        {
            var expected = CreateDbAccount( ).UserInfo.Address;

            var actual = await _sut.GetAppartmentNumberAsync(expected.AppartmentNumber.Value);

            Assert.NotNull(actual);
            Assert.Equal(expected.AppartmentNumber.Value, actual.Value);
        }
        [Theory,AutoData]
        public async Task GetAppartmentNumberAsync_WhenNumberNotExists_ReturnNull ( string appartmentNumber)
        {    

            var actual = await _sut.GetAppartmentNumberAsync(appartmentNumber);

            Assert.Null(actual);
       
        }
        [Fact]
        public async Task GetCityAsync_WhenCityExists_ReturnCity ( )
        {
            var expected = CreateDbAccount( ).UserInfo.Address;

            var actual = await _sut.GetCityAsync(expected.City.Value);

            Assert.NotNull(actual);
            Assert.Equal(expected.City.Value, actual.Value);
        }
        [Theory,AutoData]
        public async Task GetCityAsync_WhenCityNotExists_ReturnNull (string city )
        {
            var actual = await _sut.GetCityAsync(city);

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetHouseNumberAsync_WhenNumberExists_HouseNumber ( )
        {
            var expected = CreateDbAccount( ).UserInfo.Address;

            var actual = await _sut.GetHouseNumberAsync(expected.HouseNumber.Value);

            Assert.NotNull(actual);
            Assert.Equal(expected.HouseNumber.Value, actual.Value);
        }
        [Theory,AutoData]
        public async Task GetHouseNumberAsyncGetCityAsync_WhenNumberNotExists_ReturnNull ( string houseNumber)
        {
            var actual = await _sut.GetHouseNumberAsync(houseNumber);

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetStreetAsync_WhenStreetExits_ReturnStreet ( )
        {
            var expected = CreateDbAccount( ).UserInfo.Address;

            var actual = await _sut.GetStreetAsync(expected.Street.Value);

            Assert.NotNull(actual);
            Assert.Equal(expected.Street.Value, actual.Value);
        }

        [Theory,AutoData]
        public async Task GetStreetAsync_WhenStreetNotExists_ReturnNull (string street )
        {
            var actual = await _sut.GetStreetAsync(street);

            Assert.Null(actual);
        }
        private Account CreateDbAccount ( )
        {
            var account = new TestAccount(UserRole.User, generateGuid: true);
            _Db.Context.Accounts.Add(account);
            _Db.Context.SaveChanges( );
            return account;
        }
    }

}
