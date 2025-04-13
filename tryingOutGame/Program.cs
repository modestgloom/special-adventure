// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Formats.Asn1;
using System.Security.Cryptography.X509Certificates;

Console.WriteLine("Hello, World!");


string userImput = "";
Random random = new Random();
String[] playerTeams = {"Terrorist", "Counter-Terrorist"};
string currentPlayerTeam = playerTeams[1];
String[] ttDefaultInventory = {"Bomb", "Pistol"};
String[] ctDefaultInventory = {"Defuse Kit", "Pistol"};
String[] mapA =  {"A", "B", "C", "D"};
String[] mapB =  {"1", "2", "3", "4"};
String[] enemyDirections = {"North", "East", "South", "West"};
String[] playerDirections = {"North", "East", "South", "West"};
string enemyDirection = enemyDirections[0];
string playerDirection = playerDirections[0];
String[] enemyEntryPoints = {"North", "East", "South", "West", "none"};
String[] playerEntryPoints = {"North", "East", "South", "West", "none"};
string enemyEntryPoint = enemyEntryPoints[4];
string playerEntryPoint = playerEntryPoints[4];

string bombsiteA = $"{mapA.Last()}{mapB.First()}";
string bombsiteB = $"{mapA.First()}{mapB.Last()}";
string enemySpawn = $"{mapA.First()}{mapB.First()}";
string playerSpawn = $"{mapA.Last()}{mapB.Last()}";
string enemyLocationA = mapA.First();
string playerLocationA = mapA.Last();
string enemyLocationB = mapB.First();
string playerLocationB = mapB.Last();
int enemyLocationAindex = 255;
int playerLocationAindex = 255;
int enemyLocationBindex = 255;
int playerLocationBindex = 255;
double enemyHealth = 100;
double playerHealth = 100;
bool bombPlantedA = false;
bool bombPlantedB = false;
bool fightMode = false;
bool gameOver1 = false;
double playerAdvantagePoints = 0;



void playerTurn()
{
    Console.WriteLine("You are at: " + playerLocationA + playerLocationB);

    bool validWaypointPlayer = false;

    do
    {

        resetImput();
        Console.WriteLine("Choose action:");
        Console.Write("Go North(N), Go East(E), Go South(S), Go West(W), stay(H): ");
        userImput = Console.ReadLine().ToUpper();

        if (playerLocationA == mapA.First() && userImput == "W")
        {
            Console.WriteLine("You can't go West! ");
        }
        else if (playerLocationA == mapA.Last() && userImput == "E")
        {
            Console.WriteLine("You can't go East! ");
        }
        else if (playerLocationB == mapB.First() && userImput == "N")
        {
            Console.WriteLine("You can't go North! ");
        }
        else if (playerLocationB == mapB.Last() && userImput == "S")
        {
            Console.WriteLine("You can't go East! ");
        }
        else if (userImput == "W" || userImput == "E" || userImput == "N" || userImput == "S" || userImput == "H")
        {
            validWaypointPlayer = true;
        }
        else
        {
            Console.WriteLine("Please enter a valid value!");
        }

    } while (validWaypointPlayer == false);


    switch (userImput)
    {
        case "N":
            playerLocationBindex = Array.IndexOf(mapB, playerLocationB);
            playerLocationB = mapB[playerLocationBindex - 1];
            playerEntryPoint = playerEntryPoints[2];
            Console.WriteLine("You went: North");
            break;
        case "E":
            playerLocationAindex = Array.IndexOf(mapA, playerLocationA);
            playerLocationA = mapA[playerLocationAindex + 1];
            playerEntryPoint = playerEntryPoints[3];
            Console.WriteLine("You went: East");
            break;
        case "S":
            playerLocationBindex = Array.IndexOf(mapB, playerLocationB);
            playerLocationB = mapB[playerLocationBindex + 1];
            playerEntryPoint = playerEntryPoints[0];
            Console.WriteLine("You went: South");
            break;
        case "W":
            playerLocationAindex = Array.IndexOf(mapA, playerLocationA);
            playerLocationA = mapA[playerLocationAindex - 1];
            playerEntryPoint = playerEntryPoints[1];
            Console.WriteLine("You went: West");
            break;
        case "H":
            playerEntryPoint = playerEntryPoints[4];
            Console.WriteLine("You went: none");
            break;
    }
    Console.WriteLine("You are now at: " + playerLocationA + playerLocationB);
    playerLocationAindex = 255;
    playerLocationBindex = 255;
    resetImput();
    


}   



void enemyTurn()
{
    /*
    enemyLocationA = mapA[random.Next(0, mapA.Length)];
    enemyLocationB = mapB[random.Next(0, mapB.Length)];
    */
    Console.WriteLine("enemy first location: " + enemyLocationA + enemyLocationB);

    //top left
    if (enemyLocationA == mapA.First() && enemyLocationB == mapB.First())
    {
        enemyEntryPoint = enemyEntryPoints[random.Next(1, 3)];
    }
    //top right
    else if (enemyLocationA == mapA.Last() && enemyLocationB == mapB.First())
    {
        enemyEntryPoint = enemyEntryPoints[random.Next(2, 4)];
    }
    //bottom right
    else if (enemyLocationA == mapA.Last() && enemyLocationB == mapB.Last())
    {
        do
        {
            enemyEntryPoint = enemyEntryPoints[random.Next(0, 4)];
        } while (enemyEntryPoint != enemyEntryPoints[3] && enemyEntryPoint != enemyEntryPoints[0]);
    }
    //bottom left
    else if (enemyLocationA == mapA.First() && enemyLocationB == mapB.First())
    {
        enemyEntryPoint = enemyEntryPoints[random.Next(0, 2)];
    }
    //top
    else if (enemyLocationA != mapA.First() && enemyLocationA != mapA.Last() && enemyLocationB == mapB.First())
    {
        enemyEntryPoint = enemyEntryPoints[random.Next(1, 4)];
    }
    //PROBLEM HERE V (fixed)
    //right
    else if (enemyLocationA == mapA.Last() && enemyLocationB != mapB.First() && enemyLocationB != mapB.Last())
    {
        do
        {
            enemyEntryPoint = enemyEntryPoints[random.Next(0, 4)];
        } while (enemyEntryPoint != enemyEntryPoints[0] && enemyEntryPoint != enemyEntryPoints[2] && enemyEntryPoint != enemyEntryPoints[3]);
    }
    //bottom
    else if (enemyLocationB == mapB.Last() && enemyLocationA != mapA.First() && enemyLocationA != mapA.Last())
    {
        do
        {
            enemyEntryPoint = enemyEntryPoints[random.Next(0, 4)];
        } while (enemyEntryPoint != enemyEntryPoints[0] && enemyEntryPoint != enemyEntryPoints[1] && enemyEntryPoint != enemyEntryPoints[3]);
    }
    //left
    else if (enemyLocationA == mapA.First() && enemyLocationB != mapB.First() && enemyLocationB != mapB.Last())
    {
        enemyEntryPoint = enemyEntryPoints[random.Next(0, 3)];
    }
    //middle
    else if (enemyLocationA != mapA.First() && enemyLocationA != mapA.Last() && enemyLocationB != mapB.First() && enemyLocationB != mapB.Last())
    {
        enemyEntryPoint = enemyEntryPoints[random.Next(0, 4)];
    }
    
    //MAKE HIM MOVE HERE JUST LIKE BELOW: (done?)
    //String[] enemyEntryPoints = {"North", "East", "South", "West", "none"};
    /*
            case "N":
                playerLocationBindex = Array.IndexOf(mapB, playerLocationB);
                playerLocationB = mapB[playerLocationBindex - 1];
                playerEntryPoint = playerEntryPoints[2];
                Console.WriteLine("You went: North");
                break;
    */
    switch (enemyEntryPoint)
    {
        case "North":
            enemyLocationBindex = Array.IndexOf(mapB, enemyLocationB);
            enemyLocationB = mapB[enemyLocationBindex - 1];
            break;
        case "South":
            enemyLocationBindex = Array.IndexOf(mapB, enemyLocationB);
            enemyLocationB = mapB[enemyLocationBindex + 1];
            break;
        case "West":
            enemyLocationAindex = Array.IndexOf(mapA, enemyLocationA);
            enemyLocationA = mapA[enemyLocationAindex - 1];
            break;
        case "East":
            enemyLocationAindex = Array.IndexOf(mapA, enemyLocationA);
            enemyLocationA = mapA[enemyLocationAindex + 1];
            break;
    }

    
    Console.WriteLine("enemy goes: " + enemyEntryPoint);
    Console.WriteLine("enemy is now at: " + enemyLocationA + enemyLocationB);

    if (bombPlantedA == false && bombPlantedB == false)
        if (enemyLocationA == mapA.Last() && enemyLocationB == mapB.First())
        {
            Console.WriteLine("Bomb has been planted at: " + bombsiteA);
            bombPlantedA = true;
        }
        else if (enemyLocationA == mapA.First() && enemyLocationB == mapB.Last())
        {
            Console.WriteLine("Bomb has been planted at: " + bombsiteB);
            bombPlantedB = true;
        }


}




void askContinue()
{
    userImput = "255";
    while (userImput != "")
    {
        Console.Write("Press Enter to continue (type \"INFO\" for more commands): ");
        userImput = Console.ReadLine().ToUpper();

        if (userImput == "INFO")
        {
            Console.WriteLine("STATS, POSITION, QUIT");
        }
        else if (userImput == "POSITION")
        {
            Console.WriteLine("You are at: " + playerLocationA + playerLocationB);
        }
        else if (userImput == "STATS")
        {
            playerStats();
        }
        else if (userImput == "QUIT")
        {
            break;
        }
    }

}

void resetImput()
{
    userImput = "";

}

void playerStats()
{
    if (currentPlayerTeam == playerTeams[1])
    {
        if (userImput == "STATS")
        {
            Console.WriteLine("Your health: " + playerHealth);
            //player location displayed here again
            Console.WriteLine("Your location: " + playerLocationA + playerLocationB);
            Console.Write("Your inventory: " );
            foreach (string item in ctDefaultInventory)
            {
                Console.Write(item);

                if (item != ctDefaultInventory.Last())
                {
                    Console.Write(", ");
                }
                else if (item == ctDefaultInventory.Last())
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine("");
        }
    }
    else if (currentPlayerTeam == playerTeams[0])
    {
        if (userImput == "STATS")
        {
            Console.WriteLine("Your health: " + playerHealth);
            //player location displayed here again
            Console.WriteLine("Your location: " + playerLocationA + playerLocationB);
            Console.Write("Your inventory: " );
            foreach (string item in ttDefaultInventory)
            {
                Console.Write(item);

                if (item != ttDefaultInventory.Last())
                {
                    Console.Write(", ");
                }
                else if (item == ttDefaultInventory.Last())
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine("");
        }
    }


}

//combatP is used only for checking enemy presence after the player makes a move
void combatP()
{
    if (playerLocationA == enemyLocationA && playerLocationB == enemyLocationB)
    {
        Console.WriteLine("CONTACT! ENEMY SPOTTED!");
        //player north
        if (playerEntryPoint == playerEntryPoints[0])
        {
            //enemy north
            if (enemyEntryPoint == enemyEntryPoints[0])
            {
                playerAdvantagePoints = playerAdvantagePoints + 3;
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've gained 3 advantage points!");
            }
            //enemy east, south, west
            else if (enemyEntryPoint != enemyEntryPoints[0] && enemyEntryPoint != enemyEntryPoints[4])
            {
                playerAdvantagePoints = playerAdvantagePoints - 1;
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've lost 1 advantage points!");
            }
            //enemy stay
            else if (enemyEntryPoint == enemyEntryPoints[4])
            {
                playerAdvantagePoints = playerAdvantagePoints - 2;
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've lost 2 advantage points!");
            }
        }
        //player east
        else if (playerEntryPoint == playerEntryPoints[1])
        {
            //enemy east
            if (enemyEntryPoint == enemyEntryPoints[1])
            {
                playerAdvantagePoints = playerAdvantagePoints + 3;
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've gained 3 advantage points!");
            }
            //enemy north, south, west
            else if (enemyEntryPoint != enemyEntryPoints[1] && enemyEntryPoint != enemyEntryPoints[4])
            {
                playerAdvantagePoints = playerAdvantagePoints - 1;
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've lost 1 advantage points!");
            }
            //enemy stay
            else if (enemyEntryPoint == enemyEntryPoints[4])
            {
                playerAdvantagePoints = playerAdvantagePoints - 2;
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've lost 2 advantage points!");
            }
        }
        //player south
        else if (playerEntryPoint == playerEntryPoints[2])
        {
            //enemy south
            if (enemyEntryPoint == enemyEntryPoints[2])
            {
                playerAdvantagePoints = playerAdvantagePoints + 3;
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've gained 3 advantage points!");
            }
            //enemy north, east, west
            else if (enemyEntryPoint != enemyEntryPoints[2] && enemyEntryPoint != enemyEntryPoints[4])
            {
                playerAdvantagePoints = playerAdvantagePoints - 1;
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've lost 1 advantage points!");
            }
            //enemy stay
            else if (enemyEntryPoint == enemyEntryPoints[4])
            {
                playerAdvantagePoints = playerAdvantagePoints - 2;
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've lost 2 advantage points!");
            }
        }
        //player west
        else if (playerEntryPoint == playerEntryPoints[3])
        {
            //enemy west
            if (enemyEntryPoint == enemyEntryPoints[3])
            {
                playerAdvantagePoints = playerAdvantagePoints + 3;
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've gained 3 advantage points!");
            }
            //enemy north, east, south
            else if (enemyEntryPoint != enemyEntryPoints[3] && enemyEntryPoint != enemyEntryPoints[4])
            {
                playerAdvantagePoints = playerAdvantagePoints - 1;
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've lost 1 advantage points!");
            }
            //enemy stay
            else if (enemyEntryPoint == enemyEntryPoints[4])
            {
                playerAdvantagePoints = playerAdvantagePoints - 2;
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've lost 2 advantage points!");
            }
        }

        fightMode = true;
        

    }
    else if (playerLocationA != enemyLocationA && playerLocationB != enemyLocationB)
    {
        Console.WriteLine("Sector clear. No enemy detected!");
    }
}

void combatE()
{
    if (playerLocationA == enemyLocationA && playerLocationB == enemyLocationB)
    {
        Console.WriteLine("CONTACT! ENEMY SPOTTED!");
        //enemy north
        if (enemyEntryPoint == enemyEntryPoints[0])
        {
            //player north
            if (playerEntryPoint == playerEntryPoints[0])
            {
                playerAdvantagePoints = playerAdvantagePoints - 3;
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("You've lost 3 advantage points!");
            }
            //player east, south, west
            else if (playerEntryPoint != playerEntryPoints[0] && playerEntryPoint != playerEntryPoints[4])
            {
                playerAdvantagePoints = playerAdvantagePoints + 1;
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("You've gained 1 advantage points!");
            }
            //player stay
            else if (playerEntryPoint == playerEntryPoints[4])
            {
                playerAdvantagePoints = playerAdvantagePoints + 2;
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("You've gained 2 advantage points!");
            }
        }
        //enemy east
        else if (enemyEntryPoint == enemyEntryPoints[1])
        {
            //player east
            if (playerEntryPoint == playerEntryPoints[1])
            {
                playerAdvantagePoints = playerAdvantagePoints - 3;
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("You've lost 3 advantage points!");
            }
            //player north, south, west
            else if (playerEntryPoint != playerEntryPoints[1] && playerEntryPoint != playerEntryPoints[4])
            {
                playerAdvantagePoints = playerAdvantagePoints + 1;
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("You've gained 1 advantage points!");
            }
            //player stay
            else if (playerEntryPoint == playerEntryPoints[4])
            {
                playerAdvantagePoints = playerAdvantagePoints + 2;
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("You've gained 2 advantage points!");
            }
        }
        //enemy south
        else if (enemyEntryPoint == enemyEntryPoints[2])
        {
            //player south
            if (playerEntryPoint == playerEntryPoints[2])
            {
                playerAdvantagePoints = playerAdvantagePoints - 3;
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("You've lost 3 advantage points!");
            }
            //player north, east, west
            else if (playerEntryPoint != playerEntryPoints[2] && playerEntryPoint != playerEntryPoints[4])
            {
                playerAdvantagePoints = playerAdvantagePoints + 1;
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("You've gained 1 advantage points!");
            }
            //player stay
            else if (playerEntryPoint == playerEntryPoints[4])
            {
                playerAdvantagePoints = playerAdvantagePoints + 2;
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("You've gained 2 advantage points!");
            }
        }
        //enemy west
        else if (enemyEntryPoint == enemyEntryPoints[3])
        {
            //player west
            if (playerEntryPoint == playerEntryPoints[3])
            {
                playerAdvantagePoints = playerAdvantagePoints - 3;
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("You've lost 3 advantage points!");
            }
            //player north, east, south
            else if (playerEntryPoint != playerEntryPoints[3] && playerEntryPoint != playerEntryPoints[4])
            {
                playerAdvantagePoints = playerAdvantagePoints + 1;
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("You've gained 1 advantage points!");
            }
            //player stay
            else if (enemyEntryPoint == enemyEntryPoints[4])
            {
                playerAdvantagePoints = playerAdvantagePoints + 2;
                Console.WriteLine("Enemy has entered from " + enemyEntryPoint);
                Console.WriteLine("You've entered from: " + playerEntryPoint);
                Console.WriteLine("You've gained 2 advantage points!");
            }
        }

        fightMode = true;
        
    }
    else if (enemyLocationA != playerLocationA && playerLocationB != enemyLocationB)
    {
        Console.WriteLine("Sector clear. No enemy detected!");
    }
}


void fightMain()
{
    if (fightMode == false)
    {
        Console.WriteLine("No fight detected!");
    }
    else if (fightMode == true)
    {
        double shotFired = 255;
        Console.WriteLine("Combat mode started!");
        Thread.Sleep(2000);
        Console.WriteLine("You have " + playerAdvantagePoints + " advantage pionts!");
        Thread.Sleep(2000);
        playerHealth = 100;
        enemyHealth = 100;

        while (playerHealth > 0 && enemyHealth > 0)
        {
            if (playerAdvantagePoints > 0)
            {
                Console.WriteLine("You take a shot!");
                Thread.Sleep(2000);
                shotFired = random.Next(1, 11);
                if (shotFired >= 1 && shotFired <= 5)
                {
                    Console.WriteLine("You got a hit!");
                    Thread.Sleep(2000);
                    enemyHealth = enemyHealth - 25;
                    Console.WriteLine("Enemy HP: " + enemyHealth);
                    Thread.Sleep(2000);
                    playerAdvantagePoints = playerAdvantagePoints - 1;
                }
                else if (shotFired >= 6 && shotFired <= 9)
                {
                    Console.WriteLine("You missed");
                    Thread.Sleep(2000);
                    Console.WriteLine("Enemy HP: " + enemyHealth);
                    Thread.Sleep(2000);
                    playerAdvantagePoints = playerAdvantagePoints - 1;
                }
                else if (shotFired == 10)
                {
                    Console.WriteLine("Headshot!");
                    Thread.Sleep(2000);
                    enemyHealth = enemyHealth - 100;
                    Console.WriteLine("Enemy HP: " + enemyHealth);
                    Thread.Sleep(2000);
                    playerAdvantagePoints = playerAdvantagePoints - 1;
                }
            }
            else if (playerAdvantagePoints < 0)
            {
                Console.WriteLine("Enemy takes a shot!");
                Thread.Sleep(2000);
                shotFired = random.Next(1, 11);
                if (shotFired >= 1 && shotFired <= 5)
                {
                    Console.WriteLine("You got hit!");
                    Thread.Sleep(2000);
                    playerHealth = playerHealth - 25;
                    Console.WriteLine("Your HP: " + playerHealth);
                    Thread.Sleep(2000);
                    playerAdvantagePoints = playerAdvantagePoints + 1;
                }
                else if (shotFired >= 6 && shotFired <= 9)
                {
                    Console.WriteLine("Enemy missed");
                    Thread.Sleep(2000);
                    Console.WriteLine("Your HP: " + playerHealth);
                    Thread.Sleep(2000);
                    playerAdvantagePoints = playerAdvantagePoints + 1;
                }
                else if (shotFired == 10)
                {
                    Console.WriteLine("Headshot!");
                    Thread.Sleep(2000);
                    playerHealth = playerHealth - 100;
                    Console.WriteLine("Your HP: " + playerHealth);
                    Thread.Sleep(2000);
                    playerAdvantagePoints = playerAdvantagePoints + 1;
                }
            }
            else if (playerAdvantagePoints == 0)
            {
                shotFired = random.Next(0, 2);
                switch (shotFired)
                {
                    case 0:
                        playerAdvantagePoints = playerAdvantagePoints - 1;
                        break;
                    case 1:
                        playerAdvantagePoints = playerAdvantagePoints + 1;
                        break;
                }
            }
        }
        gameOver1 = true;

    }

}

//testing area
/*
bool red = true;

while (red == true)
{
    red = false;
    Console.WriteLine("i'm red");
    Thread.Sleep(2000);
}
if (red == false)
{
    Console.WriteLine("i'm not red");
}
*/
/*
if (red == true)
    break;
*/
//game starts here


do
{
    gameOver1 = false;
    do
    {
        fightMode = false;
        while (fightMode == false)
        {

            /*
            Console.WriteLine($"New Round Started! Your team: {currentPlayerTeam}");
            if (currentPlayerTeam == playerTeams[0])
            {
                Console.Write("Your objective is to eliminate all enemies");
                Console.Write(" or to plant the bomb at a bombsite and defend it from the enemy until it explodes\n");
            }
            else if (currentPlayerTeam == playerTeams[1])
            {
                Console.Write("Your objective is to eliminate all enemies");
                Console.Write(" or to defuse the bomb after it is planted at a bombsite\n");
            }
            */
            //TESTING HERE DELETE AFTERWARDS
            /*
            Console.WriteLine("it goes by fighmode line");
            fightMode = true;
            Console.WriteLine(fightMode);
            */
            /*
            userImput = "STATS";
            playerStats();
            */
            resetImput();
            askContinue();
            if (userImput == "QUIT")
                break;
            bombPlantedA = false;
            bombPlantedB = false;
            while (bombPlantedA == false && bombPlantedB == false)
            {
                enemyTurn();
                combatE();
                if (fightMode == true)
                    break;
                if (bombPlantedA == true || bombPlantedB == true)
                    break;
                askContinue();
                if (userImput == "QUIT")
                    break;
                resetImput();
                playerTurn();
                combatP();
                if (fightMode == true)
                    break;
                resetImput();
                askContinue();
                if (userImput == "QUIT")
                    break;
            }

            double enemyWhereLook = 255;
            double timeLeftToDefuse = 60;

            if (bombPlantedA == true)
            {
                Console.WriteLine("Bomb has been planted at A! (" + bombsiteA + ")");
                enemyWhereLook = random.Next(0, 2);
                if (enemyWhereLook == 0)
                {
                    enemyEntryPoint = enemyEntryPoints[2];
                }
                else if (enemyWhereLook == 1)
                {
                    enemyEntryPoint = enemyEntryPoints[3];
                }
            }
            else if (bombPlantedB == true)
            {
                Console.WriteLine("Bomb has been planted at B! (" + bombsiteB + ")");
                enemyWhereLook = random.Next(0, 2);
                if (enemyWhereLook == 0)
                {
                    enemyEntryPoint = enemyEntryPoints[0];
                }
                else if (enemyWhereLook == 1)
                {
                    enemyEntryPoint = enemyEntryPoints[1];
                }

                while (timeLeftToDefuse > 0)
                {   
                    playerTurn();
                    resetImput();
                    combatP();
                    if (fightMode == true)
                        break;
                    resetImput();
                    timeLeftToDefuse = timeLeftToDefuse - 10;
                    Console.WriteLine("You have " + timeLeftToDefuse + "secconds left to defuse!");
                }
            }
            


            if (timeLeftToDefuse <= 0)
            {
                Console.WriteLine("==========");
                Console.WriteLine("Terrorist win! bomb has been detonated!");
                Console.WriteLine("==========");
                gameOver1 = true;
            }
        }
        Console.WriteLine("Broke out of the 'fightmode' loop.");
        fightMain();

    } while (gameOver1 == false);


    Console.WriteLine("play again?");
    if (userImput != "QUIT")
    {
        resetImput();
        askContinue();
    }
    if (userImput == "QUIT")
        break;
    resetImput();

} while (userImput != "QUIT");








//19032025 fightmode bool not working - fixed
//31032025 fighmode fixed trying to correct playtest glitches - fixed

Console.WriteLine();
