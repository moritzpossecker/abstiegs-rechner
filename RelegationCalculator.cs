using Abstiegsrechner.Resources;
using HtmlAgilityPack;

namespace Abstiegsrechner
{
    public class RelegationCalculator
    {
        public bool FirstTeamRelegated { get; private set; }

        public bool SecondTeamRelegated { get; private set; }

        public bool SecondTeamDependsOnPromotion { get; private set; }

        public bool ThirdTeamRelegated { get; private set; }

        public bool ThirdTeamRelegatedWithoutSecondTeam { get; private set; }

        public bool ThirdTeamDependsOnPromotion { get; private set; }


        private List<string> _firstTeamLeagueList = null!;
        private List<string> _secondTeamLeagueList = null!;
        private List<string> _thirdTeamLeagueList = null!;

        private int _numberOfAdditionalRelegations;

        public void Calculate()
        {
            _firstTeamLeagueList = GetLeagueList(Constants.FIRST_TEAM_LEAGUE_URL);
            _secondTeamLeagueList = GetLeagueList(Constants.SECOND_TEAM_LEAGUE_URL);
            _thirdTeamLeagueList = GetLeagueList(Constants.THIRD_TEAM_LEAGUE_URL);

            _numberOfAdditionalRelegations = GetNumberOfAdditionalRelegations();

            CalculateFirstTeam();
            CalculateSecondTeam();
            CalculateThirdTeam();
        }

        private List<string> GetLeagueList(string url)
        {
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var tableElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='table-container fixtures-league-table']");

            List<string> leagueList = tableElement.InnerText.Split('.').ToList();
            leagueList.RemoveRange(0, 5);
            return leagueList;
        }

        private int GetTeamPosition(List<string> leagueList, string teamName)
        {
            for (int i = 0; i < leagueList.Count; i++)
            {
                if (leagueList[i].Contains(teamName))
                {
                    return i + 1;
                }
            }

            return 0;
        }

        private void CalculateFirstTeam()
        {
            int position = GetTeamPosition(_firstTeamLeagueList, Constants.FIRST_TEAM_NAME);

            FirstTeamRelegated = CheckIfTeamRelegates(position, 11);
        }

        private void CalculateSecondTeam()
        {
            int position = GetTeamPosition(_secondTeamLeagueList, Constants.SECOND_TEAM_NAME);

            SecondTeamRelegated = CheckIfTeamRelegates(position, 13 - _numberOfAdditionalRelegations);

            if (!SecondTeamRelegated)
            {
                SecondTeamDependsOnPromotion = false;
                return;
            }


            SecondTeamDependsOnPromotion = !CheckIfTeamRelegates(position, 13 - _numberOfAdditionalRelegations + 1);
        }

        private int GetNumberOfAdditionalRelegations()
        {
            int number = 0;

            foreach(string teamName in Constants.POSSIBLE_STADTLIGA_TEAMS)
            {
                int position = GetTeamPosition(_firstTeamLeagueList, teamName);
                if(position >= 11)
                {
                    number++;
                }
            }

            return number;
        }

        private void CalculateThirdTeam()
        {
            if (SecondTeamRelegated)
            {
                ThirdTeamRelegated = true;
            }

            int position = GetTeamPosition(_thirdTeamLeagueList, Constants.THIRD_TEAM_NAME);
            int numberOfAdditionalRelegations = (_numberOfAdditionalRelegations + 1) / 2;

            ThirdTeamRelegatedWithoutSecondTeam = CheckIfTeamRelegates(position, 13 - numberOfAdditionalRelegations);

            if (!ThirdTeamRelegatedWithoutSecondTeam)
            {
                ThirdTeamRelegated = false;
                ThirdTeamDependsOnPromotion = false;
                return;
            }

            ThirdTeamDependsOnPromotion = !CheckIfTeamRelegates(position, 13 - numberOfAdditionalRelegations + 1);
        }

        private bool CheckIfTeamRelegates(int position, int relegationPlace)
        {
            if(position >= relegationPlace)
            {
                return true;
            }

            return false;
        }
    }
}
