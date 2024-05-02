namespace Abstiegsrechner.Resources
{
    public static class Constants
    {
        public static string FIRST_TEAM_LEAGUE_URL = "https://www.fussball.de/spieltagsuebersicht/landesklasse-nord-sachsen-landesklasse-herren-saison2324-sachsen/-/staffel/02M76H3DU800000BVS5489B4VSAUO6GA-G#!/";
        public static string FIRST_TEAM_NAME = "Roter Stern Leipzig";

        public static string SECOND_TEAM_LEAGUE_URL = "https://www.fussball.de/spieltagsuebersicht/herren-stadtklasse-kreis-leipzig-kreisliga-a-herren-saison2324-sachsen/-/staffel/02MAPI1ACO000004VS5489B3VVRQ15EP-G#!/";
        public static string SECOND_TEAM_NAME = "Roter Stern Leipzig 99 II";

        public static string THIRD_TEAM_LEAGUE_URL = "https://www.fussball.de/spieltagsuebersicht/1kreisklasse-st-1-kreis-leipzig-1kreisklasse-herren-saison2324-sachsen/-/staffel/02MAPT224G000004VS5489B3VVRQ15EP-G#!/";
        public static string THIRD_TEAM_NAME = "Roter Stern Leipzig 99 III";

        public static List<string> POSSIBLE_STADTLIGA_TEAMS = new List<string>()
        {
            FIRST_TEAM_NAME,
            "SG Rotation Leipzig",
            "SV Lindenau 1848",
            "SV Liebertwolkwitz",
            "FC Blau-Weiß Leipzig"
        };
    }
}
