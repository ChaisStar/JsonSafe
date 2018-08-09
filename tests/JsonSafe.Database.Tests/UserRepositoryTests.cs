namespace JsonSafe.Database.Tests
{
    using System;
    using AutoFixture.NUnit3;
    using DbModels;
    using NSubstitute;
    using NUnit.Framework;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using AutoMapper;
    using Models;
    using Neleus.LambdaCompare;

    [TestFixture]
    public class UserRepositoryTests
    {
        private IMongoCollectionClient<DbUser> _mongoCollectionClient;
        private IMapper _mapper;
        private UserRepository _userRepositoryInstance;
        private const string CollectionName = nameof(DbUser);

        [SetUp]
        public void Setup()
        {
            _mongoCollectionClient = Substitute.For<IMongoCollectionClient<DbUser>>();
            _mapper = Substitute.For<IMapper>();
            _userRepositoryInstance = new UserRepository(_mongoCollectionClient, _mapper);
        }

        [Test, AutoData]
        public async Task AddUserAsync_Should_Add_UserAsync(DbUser dbUser, UserModel userModel)
        {
            _mapper.Map<DbUser>(userModel).Returns(dbUser);
            await _userRepositoryInstance.AddUserAsync(userModel);
            await _mongoCollectionClient.Received(1).InsertOneAsync(CollectionName, dbUser);
        }

        [Test, AutoData]
        public async Task AddUserAsync_Should_Add_Create_Collection_With_UsernameAsync(DbUser dbUser, UserModel userModel)
        {
            _mapper.Map<DbUser>(userModel).Returns(dbUser);
            await _userRepositoryInstance.AddUserAsync(userModel);
            await _mongoCollectionClient.Received(1).CreateCollectionAsync(userModel.Username);
        }

        [Test, AutoData]
        public async Task AddUserAsync_Should_Return_Created_UserAsync(DbUser dbUser, UserModel userModel)
        {
            _mapper.Map<DbUser>(userModel).Returns(dbUser);
            _mapper.Map<UserModel>(dbUser).Returns(userModel);
            Expression<Func<DbUser, bool>> expression = null;
            _mongoCollectionClient
                .When(x => x.FindOneAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>()))
                .Do(info => { expression = (Expression<Func<DbUser, bool>>) info[1]; });
            _mongoCollectionClient
                .FindOneAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>())
                .Returns(dbUser);

            var result = await _userRepositoryInstance.AddUserAsync(userModel);
            Expression<Func<DbUser, bool>> expectedExpression = x => x.Username == userModel.Username;
            Assert.AreEqual(userModel, result);
            Assert.IsTrue(Lambda.Eq(expectedExpression, expression));
        }

        [Test, AutoData]
        public async Task DeleteUserAsync_Should_Delete_UserAsync(DbUser dbUser, UserModel userModel)
        {
            _mapper.Map<DbUser>(userModel).Returns(dbUser);
            _mapper.Map<UserModel>(dbUser).Returns(userModel);

            Expression<Func<DbUser, bool>> getExpression = null;
            Expression<Func<DbUser, bool>> deleteExpression = null;
            _mongoCollectionClient
                .When(x => x.DeleteOneAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>()))
                .Do(info => { getExpression = (Expression<Func<DbUser, bool>>)info[1]; });
            _mongoCollectionClient
                .When(x => x.FindOneAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>()))
                .Do(info => { deleteExpression = (Expression<Func<DbUser, bool>>)info[1]; });
            _mongoCollectionClient
                .FindOneAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>())
                .Returns(dbUser);

            await _userRepositoryInstance.DeleteUserAsync(dbUser.Id);
            Expression<Func<DbUser, bool>> expectedExpression = x => x.Id == dbUser.Id;

            await _mongoCollectionClient
                .Received(1)
                .DeleteOneAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>());
            Assert.IsTrue(Lambda.Eq(expectedExpression, getExpression));
            Assert.IsTrue(Lambda.Eq(expectedExpression, deleteExpression));
        }

        [Test, AutoData]
        public async Task DeleteUserAsync_Should_Drop_Collection_With_UsernameAsync(DbUser dbUser, UserModel userModel)
        {
            _mapper.Map<DbUser>(userModel).Returns(dbUser);
            _mapper.Map<UserModel>(dbUser).Returns(userModel);

            _mongoCollectionClient
                .FindOneAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>())
                .Returns(dbUser);

            await _userRepositoryInstance.DeleteUserAsync(dbUser.Id);
            await _mongoCollectionClient.Received(1).DropCollectionAsync(userModel.Username);
        }

        [Test, AutoData]
        public async Task GetByIdAsync_Should_Return_User_With_Same_IdAsync(DbUser dbUser, UserModel userModel)
        {
            _mapper.Map<DbUser>(userModel).Returns(dbUser);
            _mapper.Map<UserModel>(dbUser).Returns(userModel);

            Expression<Func<DbUser, bool>> expression = null;
            _mongoCollectionClient
                .When(x => x.FindOneAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>()))
                .Do(info => { expression = (Expression<Func<DbUser, bool>>)info[1]; });
            _mongoCollectionClient
                .FindOneAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>())
                .Returns(dbUser);

            var result = await _userRepositoryInstance.GetByIdAsync(dbUser.Id);
            Expression<Func<DbUser, bool>> expectedExpression = x => x.Id == dbUser.Id;
            Assert.AreEqual(userModel, result);
            Assert.IsTrue(Lambda.Eq(expectedExpression, expression));
        }

        [Test, AutoData]
        public async Task GetByUsernameAsync_Should_Return_User_With_Same_UsernameAsync(DbUser dbUser, UserModel userModel)
        {
            _mapper.Map<DbUser>(userModel).Returns(dbUser);
            _mapper.Map<UserModel>(dbUser).Returns(userModel);

            Expression<Func<DbUser, bool>> expression = null;
            _mongoCollectionClient
                .When(x => x.FindOneAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>()))
                .Do(info => { expression = (Expression<Func<DbUser, bool>>)info[1]; });
            _mongoCollectionClient
                .FindOneAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>())
                .Returns(dbUser);

            var result = await _userRepositoryInstance.GetByUsernameAsync(dbUser.Username);
            Expression<Func<DbUser, bool>> expectedExpression = x => x.Username == dbUser.Username;
            Assert.AreEqual(userModel, result);
            Assert.IsTrue(Lambda.Eq(expectedExpression, expression));
        }

        [Test, AutoData]
        public async Task GetByEmailAsync_Should_Return_User_With_Same_EmailAsync(DbUser dbUser, UserModel userModel)
        {
            _mapper.Map<DbUser>(userModel).Returns(dbUser);
            _mapper.Map<UserModel>(dbUser).Returns(userModel);

            Expression<Func<DbUser, bool>> expression = null;
            _mongoCollectionClient
                .When(x => x.FindOneAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>()))
                .Do(info => { expression = (Expression<Func<DbUser, bool>>)info[1]; });
            _mongoCollectionClient
                .FindOneAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>())
                .Returns(dbUser);

            var result = await _userRepositoryInstance.GetByEmailAsync(dbUser.Email);
            Expression<Func<DbUser, bool>> expectedExpression = x => x.Email == dbUser.Email;
            Assert.AreEqual(userModel, result);
            Assert.IsTrue(Lambda.Eq(expectedExpression, expression));
        }

        [Test, AutoData]
        public async Task IsEmailExistAsync_Should_Return_Search_User_With_Same_EmailAsync(string email, bool exist)
        {
            Expression<Func<DbUser, bool>> expression = null;
            _mongoCollectionClient
                .When(x => x.ExistAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>()))
                .Do(info => { expression = (Expression<Func<DbUser, bool>>)info[1]; });
            _mongoCollectionClient
                .ExistAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>())
                .Returns(exist);

            var result = await _userRepositoryInstance.IsEmailExistAsync(email);
            Expression<Func<DbUser, bool>> expectedExpression = x => x.Email == email;
            Assert.AreEqual(exist, result);
            Assert.IsTrue(Lambda.Eq(expectedExpression, expression));
        }

        [Test, AutoData]
        public async Task IsUsernameExistAsync_Should_Return_Search_User_With_Same_UsernameAsync(string username, bool exist)
        {
            Expression<Func<DbUser, bool>> expression = null;
            _mongoCollectionClient
                .When(x => x.ExistAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>()))
                .Do(info => { expression = (Expression<Func<DbUser, bool>>)info[1]; });
            _mongoCollectionClient
                .ExistAsync(CollectionName, Arg.Any<Expression<Func<DbUser, bool>>>())
                .Returns(exist);

            var result = await _userRepositoryInstance.IsUsernameExistAsync(username);
            Expression<Func<DbUser, bool>> expectedExpression = x => x.Username == username;
            Assert.AreEqual(exist, result);
            Assert.IsTrue(Lambda.Eq(expectedExpression, expression));
        }
    }
}