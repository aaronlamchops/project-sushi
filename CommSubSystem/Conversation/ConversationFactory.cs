using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedObjects;
using System.Net;

namespace CommSubSystem.Conversation
{
    class ConversationFactory
    {
        private static ConversationFactory _Instance;
        private static readonly object MyLock = new object();
        private ConversationFactory() { }
        public int DefaultTimeout = 300;
        public int DefaultMaxRetries = 3;


        public static ConversationFactory Instance
        {
            get
            {
                lock (MyLock)
                {
                    if (_Instance == null)
                    {
                        _Instance = new ConversationFactory();
                    }
                }
                return _Instance;
            }
        }

        public virtual Conversation CreateFromMessage(Envelope env)
        {
            Conversation conversation = null;
            bool initiator = false;


            
            return conversation;
        }

        public virtual Conversation CreateFromConversationType(ConversationTypes type, IPEndPoint receiver)
        {
            Conversation conversation = null;
            bool initiator = true;
            if(type == ConversationTypes.CreateGame)
            {
                conversation = new CreateGameConv()
                {
                    ConvId = MessageId.Create(),
                    Timeout = DefaultTimeout,
                    MaxRetries = DefaultMaxRetries,
                    Done = false,
                    InitiatorConv = initiator,
                    EndIP = receiver
                };
            }
            return conversation;
        }

        private ConversationTypes MatchMessageTypeToConversation(Envelope.TypeOfMessage msgType)
        {
            ConversationTypes convType;
            switch (msgType)
            {
                case Envelope.TypeOfMessage.CreateGame:
                    convType = ConversationTypes.CreateGame;
                    break;
                case Envelope.TypeOfMessage.ExitGame:
                    convType = ConversationTypes.ExitGame;
                    break;
                case Envelope.TypeOfMessage.HeartBeat:
                    convType = ConversationTypes.HeartBeat;
                    break;
                case Envelope.TypeOfMessage.JoinGame:
                    convType = ConversationTypes.JoinGame;
                    break;
                case Envelope.TypeOfMessage.PassCard:
                    convType = ConversationTypes.PassCard;
                    break;
                case Envelope.TypeOfMessage.SelectCard:
                    convType = ConversationTypes.SelectCard;
                    break;
                case Envelope.TypeOfMessage.StartGame:
                    convType = ConversationTypes.StartGame;
                    break;
                case Envelope.TypeOfMessage.StartNewRound:
                    convType = ConversationTypes.StartNewRound;
                    break;
                case Envelope.TypeOfMessage.UpdateChat:
                    convType = ConversationTypes.UpdateChat;
                    break;
                case Envelope.TypeOfMessage.UpdateState:
                    convType = ConversationTypes.UpdateState;
                    break;
                case Envelope.TypeOfMessage.UserInfo:
                    convType = ConversationTypes.UserInfo;
                    break;
                default:
                    convType = ConversationTypes.None;
                    break;
            }
            return convType;
        }
    }
}
