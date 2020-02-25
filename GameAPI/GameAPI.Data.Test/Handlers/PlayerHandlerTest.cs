using AutoMapper;
using GameAPI.Data.Context;
using GameAPI.Data.Handlers;
using GameAPI.Data.Mappers.Profiles;
using GameAPI.Data.Request.PlayerRequest;
using GameAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using Xunit;

namespace GameAPI.Data.Test.Handlers
{
    public class PlayerHandlerTest
    {
        [Fact]
        [Trait("Author", "Junior Macedo")]
        public async void CreatePlayer()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "CreatePlayer")
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
            }
        }

        [Fact]
        [Trait("Author", "Junior Macedo")]
        public async void QueryPlayer()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "QueryPlayer")
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

                PlayerHandler playerHandler = new PlayerHandler(context, mapper);

                CreatePlayer createPlayer = new CreatePlayer
                {
                    FirstName = "Dummy"
                };

                Player player = await playerHandler.Handle(createPlayer, cancellationToken);

                Assert.NotNull(player);
                Assert.Equal(createPlayer.FirstName, player.FirstName);

                QueryPlayerRequest queryPlayerRequest = new QueryPlayerRequest();

                var query = await playerHandler.Handle(queryPlayerRequest, cancellationToken);
                Assert.NotNull(query);
                Assert.Equal(createPlayer.FirstName, query.FirstOrDefault().FirstName);
            }
        }

        [Fact]
        [Trait("Author", "Junior Macedo")]
        public async void QuerySinglePlayer()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "QuerySinglePlayer")
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

                PlayerHandler playerHandler = new PlayerHandler(context, mapper);

                CreatePlayer createPlayer = new CreatePlayer
                {
                    FirstName = "Dummy"
                };

                Player player = await playerHandler.Handle(createPlayer, cancellationToken);

                Assert.NotNull(player);
                Assert.Equal(createPlayer.FirstName, player.FirstName);

                QuerySinglePlayerRequest querySinglePlayerRequest = new QuerySinglePlayerRequest();

                querySinglePlayerRequest.key = 1;

                var query = await playerHandler.Handle(querySinglePlayerRequest, cancellationToken);
                Assert.NotNull(query);
                Assert.Equal(createPlayer.FirstName, query.FirstOrDefault().FirstName);
            }
        }
    }
}
