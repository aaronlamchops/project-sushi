using System;
using System.Collections.Generic;


namespace SharedObjects
{
    public class Game
    {
        public int gameId;
        public List<Player> playerList = new List<Player>();
        private int playerCount;
        private int numChosenCards;
        private int numRounds;
        private int round;
        private int direction = 0;
        private Deck _Deck = new Deck();

        public Game(int gameId, int playerCount, SendHandler sendFunct)
        {
            this.gameId = gameId;
            this.playerCount = playerCount;
            SendCardToPlayer = sendFunct;

            switch (playerCount)
            {
                case 2:
                    numRounds = 10;
                    break;
                case 3:
                    numRounds = 9;
                    break;
                case 4:
                    numRounds = 8;
                    break;
                case 5:
                    numRounds = 7;
                    break;
                default:
                    numRounds = 10;
                    break;
            }
        }

        public void AddPlayer(int playerId)
        {
            if (playerList.Count < playerCount)
            {
                Player player = new Player
                {
                    Id = playerId
                };
                playerList.Add(player);
                
                if(playerList.Count == playerCount)
                {
                    StartGame();
                }
            }
        }

        public void SelectCard(int playerId, CardTypes card)
        {
            //remove card from hand and add to played cards
            Player player = playerList.Find(x => x.Id == playerId);
            player.ChooseCard(card);
            numChosenCards++;
            //if all players have chosen a card, pass cards
            if (numChosenCards == playerCount)
            {
                //reset
                numChosenCards = 0;
                round--;

                //if last card, score round
                if (round == 0)
                {
                    foreach(Player currPlayer in playerList)
                    {
                        currPlayer.ScoreCards();
                        currPlayer.Hand = _Deck.DealHand(numRounds);
                    }
                }
                
                PassCards();
            }
        }

        private void StartGame()
        {
            int numDealCards = numRounds;

            //shuffle deck
            _Deck.ShuffleDeck();

            //assign cards
            foreach(Player player in playerList)
            {
                player.Hand = _Deck.DealHand(numDealCards);
            }

            SendCardInfo();
        }

        private void PassCards()
        {
            List<CardTypes> tempHand1 = null;
            List<CardTypes> tempHand2 = null;
            if (direction == 0)
            {
                for (int i = 0; i < playerCount; i++)
                {
                    //first case
                    if (i == 0)
                    {
                        tempHand1 = playerList[i].Hand;
                    } 
                    //last case
                    else if(i == playerCount - 1)
                    {
                        tempHand2 = playerList[i].Hand;
                        playerList[i].Hand = tempHand1;
                        playerList[0].Hand = tempHand2;
                    }
                    //normal case
                    else
                    {
                        tempHand2 = playerList[i].Hand;
                        playerList[i].Hand = tempHand1;
                        tempHand1 = tempHand2;
                    }
                }
            }
            else
            {
                for (int i = playerCount - 1; i >= 0; i--)
                {
                    //last case
                    if (i == 0)
                    {
                        tempHand1 = playerList[i].Hand;
                        playerList[i].Hand = tempHand1;
                        playerList[playerCount-1].Hand = tempHand2;
                    }
                    //first case
                    else if (i == playerCount - 1)
                    {
                        tempHand1 = playerList[i].Hand;
                    }
                    //normal case
                    else
                    {
                        tempHand2 = playerList[i].Hand;
                        playerList[i].Hand = tempHand1;
                        tempHand1 = tempHand2;
                    }
                }
            }
            SendCardInfo();
        }

        public delegate void SendHandler(int playerId, List<CardTypes> Hand);
        public SendHandler SendCardToPlayer { get; set; }

        private void SendCardInfo()
        {
            foreach(Player player in playerList)
            {
                SendCardToPlayer(player.Id, player.Hand);
            }
        }
    }

    [Serializable]
    public class GameInfo
    {
        public int gameId;
        public string GameName;
        public List<Player> playerList = new List<Player>();
        public int MinPlayers;
        public int MaxPlayers;
        public Player host;

        public GameInfo(int gameId, Player host, int minPlayer, int maxPlayer, string name)
        {
            this.GameName = name;
            this.gameId = gameId;
            this.host = host;
            this.MinPlayers = minPlayer;
            this.MaxPlayers = maxPlayer;
        }

        public void AddPlayer(Player p)
        {
            if (playerList.Count < MaxPlayers)
            {
                playerList.Add(p);
            }
            else
            {
                //error, maxplayers exceeded
            }
        }
    }
}
