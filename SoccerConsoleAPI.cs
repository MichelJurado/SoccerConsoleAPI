using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.IO;
using NestedJSON2.Models;

//For the api key make an account here https://www.api-football.com/pricing and use your api-key it is free up to 100 requests a day


namespace NestedJSON2
{
    public class SoccerConsoleAPI
    {
        static async Task Main(string[] args)
        {
            string cont = "y";
            
            


            do
            {
                Console.WriteLine("Welcome to the SOCCER API PROJECT");
                Console.WriteLine("1. League Standings");
                Console.WriteLine("2. Team Squads");
                Console.WriteLine("3. Player Trophies");

                string opt = Console.ReadLine();

                if (opt == "1")
                {
                    await GetLeagueStandings();
                }
                else if (opt == "2")
                {
                    await GetSquads();
                }
                else if (opt == "3")
                {
                    await GetTrophies();
                }
                else
                {
                    Console.Write("NOT VALID OPTION!!!");
                }

                Console.WriteLine("Do you want to continue (y/n)");
                cont = Console.ReadLine();


            } while (cont != "n");
            

            //await GetLeagueStandings(); // need the await to print the result to console
        }

        

        //functions --------------------------------------------------------


        static int tableWidth = 100;

        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }


        //async task to get league standings
        static async Task GetLeagueStandings()
        {



            Console.WriteLine("Enter a season:");
            string submitseason = Console.ReadLine();
            Console.WriteLine("Choose a league: \n Premier League \n La liga \n Serie A \n Ligue1");
            string submitleague = Console.ReadLine();

            if (submitleague == "Premier League")
            {
                submitleague = "39";
            }
            else if (submitleague == "La liga")
            {
                submitleague = "140";
            }
            else if (submitleague == "Serie A")
            {
                submitleague = "135";
            }
            else if (submitleague == "Ligue1")
            {
                submitleague = "61";
            }
            else
            {
                Console.WriteLine("Please type leagues exactly how they are!");
            }

            Rootobject rootobject = new Rootobject();

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Format("https://v3.football.api-sports.io/standings?season={0}&league={1}", submitseason, submitleague)),
                Headers =
                    {
                        { "X-RapidAPI-Key", "" },
                        { "X-RapidAPI-Host", "v3.football.api-sports.io" },
                    },
                        };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(body);

                rootobject = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(body);

                //Console.WriteLine(rootobject.parameters.league);

                PrintLine();
                PrintRow("Team Name", "Points", "Wins", "Losses", "Draws");
                PrintLine();

                foreach (var responseArray in rootobject.response) // iterating and accessing the array for response
                {
                    foreach (var standingsArray in responseArray.league.standings) //iterating through the standings array in the response array
                    {
                        foreach (var standingsArrayArray in standingsArray) //iterating through the standings subarray within standings array, at this point you can access the nested values
                        {
                            //Console.WriteLine(standingsArrayArray.points);
                            //Console.WriteLine(string.Format("Team: {0} ", standingsArrayArray.team.name));
                            //Console.WriteLine(string.Format("Points: {0}", standingsArrayArray.points));
                            //Console.WriteLine($"Team: {standingsArrayArray.team.name}  \t\t Points: {standingsArrayArray.points} \t\t W:{standingsArrayArray.all.win} \t\t L:{standingsArrayArray.all.lose} \t\t D:{standingsArrayArray.all.draw}");
                            PrintRow($"{standingsArrayArray.team.name}", $"{standingsArrayArray.points}", $"{standingsArrayArray.all.win}", $"{standingsArrayArray.all.lose}", $"{standingsArrayArray.all.draw}");

                        }
                    }
                }
                PrintLine();
            
            }



        }

        // async task to get squads 

        static async Task GetSquads()
        {

           

            Console.WriteLine("Enter a squad: \n Barcelona \n Real Madrid \n Manchester United \n Manchester City \n PSG \n Bayern Munich");
            string squad = Console.ReadLine();

            if (squad == "Barcelona")
            {
                squad = "529";
            }
            else if (squad == "Real Madrid")
            {
                squad = "541";
            }
            else if (squad == "Manchester United")
            {
                squad = "33";
            }
            else if (squad == "Manchester City")
            {
                squad = "50";
            }
            else if (squad == "PSG")
            {
                squad = "85";
            }
            else if (squad == "Bayern Munich")
            {
                squad = "157";
            }
            else
            {
                Console.WriteLine("Please type leagues exactly how they are!");
            }


            Rootobject1 rootobject1 = new Rootobject1();

            var client1 = new HttpClient();
            var request1 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Format("https://v3.football.api-sports.io/players/squads?team={0}", squad)),
                Headers =
            {
                { "X-RapidAPI-Key", "" },
                { "X-RapidAPI-Host", "v3.football.api-sports.io" },
            },
            };
            using (var response = await client1.SendAsync(request1))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(body);

                rootobject1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject1>(body);

                PrintLine();
                PrintRow("Player Name", "Position", "Jersey");
                PrintLine();

               


                foreach (var responseArray in rootobject1.response) // iterating and accessing the array for response
                {
                    foreach (var standingsArray in responseArray.players) //iterating through the standings array in the response array
                    {
                        //Console.WriteLine($"{standingsArray.name}\t{standingsArray.position}\t{standingsArray.number}");
                        PrintRow($"{standingsArray.name}", $"{standingsArray.position}", $"{standingsArray.number}");
                    }
                }

                PrintLine();

            }


        }

        static async Task GetTrophies()
        {
            Console.WriteLine("Enter a player: \n Leo Messi \n Cristiano Ronaldo \n Neymar \n Sergio Ramos \n Sergio Busquets \n Pique");
            string player = Console.ReadLine();


            if (player == "Pique")
            {
                player = "136";
            }
            else if (player == "Sergio Busquets")
            {
                player = "144";
            }
            else if (player == "Cristiano Ronaldo")
            {
                player = "874";
            }
            else if (player == "Sergio Ramos")
            {
                player = "738";
            }
            else if (player == "Neymar")
            {
                player = "276";
            }
            else if (player == "Leo Messi")
            {
                player = "154";
            }
            else
            {
                Console.WriteLine("Please type players exactly how they are!");
            }


            Rootobject2 rootobject2 = new Rootobject2();

            var client2 = new HttpClient();
            var request2 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Format("https://v3.football.api-sports.io/trophies?player={0}", player)),
                Headers =
            {
                { "X-RapidAPI-Key", "" },
                { "X-RapidAPI-Host", "v3.football.api-sports.io" },
            },
            };
            using (var response = await client2.SendAsync(request2))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                

                rootobject2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject2>(body);

                PrintLine();
                PrintRow("League", "Country", "Season","Place");
                PrintLine();




                foreach (var responseArray in rootobject2.response) // iterating and accessing the array for response
                {
                    
                        
                        PrintRow($"{responseArray.league}", $"{responseArray.country}", $"{responseArray.season}", $"{responseArray.place}");
                    
                }

                PrintLine();

            }

        }

    }
            
}
