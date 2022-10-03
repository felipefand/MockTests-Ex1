using FluentAssertions;
using MockingUnitTestsDemoApp.Impl.Models;
using MockingUnitTestsDemoApp.Impl.Repositories.Interfaces;
using MockingUnitTestsDemoApp.Impl.Services;
using NSubstitute;

namespace MockingUnitTestsDemoApp.Tests.Services
{
    public class PlayerServiceTest
    {

        private readonly PlayerService _subject;
        private readonly IPlayerRepository _mockPlayerRepository;
        private readonly ITeamRepository _mockTeamRepository;
        private readonly ILeagueRepository _mockLeagueRepository;

        public PlayerServiceTest()
        {
            _mockPlayerRepository = Substitute.For<IPlayerRepository>();
            _mockTeamRepository = Substitute.For<ITeamRepository>();
            _mockLeagueRepository = Substitute.For<ILeagueRepository>();
            _subject = new PlayerService(_mockPlayerRepository, _mockTeamRepository, _mockLeagueRepository);
        }

        [Fact]
        public void GetForLeague_ValidId_PlayerList()
        {
            //Arrange
            var leagueId = 3;
            _mockLeagueRepository.IsValid(Arg.Any<int>()).Returns(true);
            _mockTeamRepository.GetForLeague(Arg.Any<int>()).Returns(GetTeamList());
            _mockPlayerRepository.GetForTeam(Arg.Any<int>()).Returns(GetPlayerList());

            //Act
            var teams = _subject.GetForLeague(leagueId);

            //Assert
            teams.Should()
                .NotBeEmpty();
        }

        [Fact]
        public void GetForLeague_InvalidId_EmptyPlayerList()
        {
            //Arrange
            var leagueId = 3;
            _mockLeagueRepository.IsValid(Arg.Any<int>()).Returns(false);

            //Act
            var teams = _subject.GetForLeague(leagueId);

            //Assert
            teams.Should()
                .BeEmpty();
        }

        private List<Team> GetTeamList()
        {
            var teams = new List<Team> { new Team(), new Team() };
            return teams;
        }

        private List<Player> GetPlayerList()
        {
            var players = new List<Player> { new Player(), new Player() };
            return players;
        }
    }
}
