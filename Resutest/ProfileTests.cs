using Resunet.DAL.Models;
using Resutest.Helpers;
using System.Transactions;

namespace Resutest
{
    public class ProfileTests : BaseTest
    {
        [Test]
        public async Task AddTest()
        {
            using (var scope = Helper.CreateTransactionScope())
            {
                await _profile.AddOrUpdateAsync(
                    new ProfileModel
                    {
                        UserId = 2999,
                        FirstName = "Иван",
                        LastName = "Иванов",
                        ProfileName = "Тест"
                    });
                var results = await _profile.GetAsync(2999);
                Assert.That(results.Count(), Is.EqualTo(1));

                var result = results.First();
                Assert.That(result.FirstName, Is.EqualTo("Иван"));
                Assert.That(result.LastName, Is.EqualTo("Иванов"));
                Assert.That(result.ProfileName, Is.EqualTo("Тест"));
                Assert.That(result.UserId, Is.EqualTo(2999));
            }
        }

        [Test]
        public async Task UpdateTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                var profileModel = new ProfileModel()
                {
                    UserId = 19,
                    FirstName = "Иван",
                    LastName = "Иванов",
                    ProfileName = "Тест"
                };

                await _profile.AddOrUpdateAsync(profileModel);
                profileModel.FirstName = "Иван1";
                await _profile.AddOrUpdateAsync(profileModel);

                var results = await _profile.GetAsync(19);
                Assert.That(results.Count(), Is.EqualTo(1));

                var result = results.First();
                Assert.That(result.FirstName, Is.EqualTo("Иван1"));
                Assert.That(result.UserId, Is.EqualTo(19));
            }
        }
    }
}
