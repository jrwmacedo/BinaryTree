using AutoMapper;
using GameAPI.Data.Context;
using GameAPI.Data.Handlers;
using GameAPI.Data.Mappers.Profiles;
using GameAPI.Data.Request.PlayerRequest;
using GameAPI.Data.Request.QuestionRequest;
using GameAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace GameAPI.Data.Test.Handlers
{
    public class QuestionHandlerTest
    {
        [Fact]
        [Trait("Author", "Junior Macedo")]
        public async void CreatePlayerQuestion()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "CreatePlayerQuestion")
                .Options;

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                CancellationToken cancellationToken = new CancellationToken();

                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new GameProfile());
                });

                var mapper = mockMapper.CreateMapper();

                PlayerHandler playerHandler = new PlayerHandler(context, mapper);

                CreatePlayer createPlayer = new CreatePlayer
                {
                    FirstName = "Dummy"
                };

                Player player = await playerHandler.Handle(createPlayer, cancellationToken);
                Assert.NotNull(player);
                Assert.Equal(createPlayer.FirstName, player.FirstName);

                CreatePlayerQuestion createPlayerQuestion = new CreatePlayerQuestion
                {
                    Id = 1,
                    Answers = new int[] { 1, 2, 4 }
                };

                QuestionHandler questionHandler = new QuestionHandler(context, mapper);
                IList<PlayerQuestion> playerQuestions = await questionHandler.Handle(createPlayerQuestion, cancellationToken);
                Assert.NotNull(playerQuestions);
                Assert.Collection(playerQuestions,
                    item =>
                    {
                        Assert.Equal(1, item.PlayerId);
                        Assert.Equal(1, item.QuestionId);
                    },
                    item =>
                    {
                        Assert.Equal(1, item.PlayerId);
                        Assert.Equal(2, item.QuestionId);
                    },
                    item =>
                    {
                        Assert.Equal(1, item.PlayerId);
                        Assert.Equal(4, item.QuestionId);
                    });
            }
        }

        [Fact]
        [Trait("Author", "Junior Macedo")]
        public async void QueryQuestion()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "QueryQuestion")
                .Options;

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                CancellationToken cancellationToken = new CancellationToken();
                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new GameProfile());
                });

                var mapper = mockMapper.CreateMapper();

                QuestionHandler questionHandler = new QuestionHandler(context, mapper);

                QueryQuestionRequest queryQuestionRequest = new QueryQuestionRequest();

                var query = await questionHandler.Handle(queryQuestionRequest, cancellationToken);
                Assert.NotNull(query);
                Assert.Equal(1, query.FirstOrDefault().Id);
                Assert.True(query.Count() > 1);
            }
        }

        [Fact]
        [Trait("Author", "Junior Macedo")]
        public async void QuerySingleQuestion()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "QuerySingleQuestion")
                .Options;

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                CancellationToken cancellationToken = new CancellationToken();
                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new GameProfile());
                });

                var mapper = mockMapper.CreateMapper();

                QuestionHandler questionHandler = new QuestionHandler(context, mapper);

                QueryQuestionRequest queryQuestionRequest = new QueryQuestionRequest();

                var query = await questionHandler.Handle(queryQuestionRequest, cancellationToken);
                Assert.NotNull(query);
                Assert.Equal(1, query.FirstOrDefault().Id);
            }
        }
    }
}
