using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using SharedObjects;


namespace CommunicationSubsystem
{
    public class CommSubSystem
    {
        private UDPCommunicator _myUdpCommunicator;
        private IPAddress _bestAddress;

        private readonly ConversationDictionary _queueDictionary;
        private readonly ConversationFactory _conversationFactory;
        private readonly CommProcessState _processState;

        public CommProcessState ProcessState => _processState;
        public int Port => _myUdpCommunicator?.Port ?? 0;
        public string BestLocalEndPoint => $"{FindBestLocalIpAddress()}:{Port}";

        public CommSubSystem(CommProcessState processState, ConversationFactory factory)
        {
            _processState = processState;
            _conversationFactory = factory;
            _conversationFactory.ManagingSubSystem = this;
            _queueDictionary = new ConversationDictionary();
        }

        public void Initialize()
        {
            _conversationFactory.Initialize();

            _myUdpCommunicator = new UDPCommunicator()
            {
                MinPort = ProcessState.Options.MinPort,
                MaxPort = ProcessState.Options.MaxPort,
                Timeout = ProcessState.Options.Timeout,
                EnvelopeHandler = ProcessIncomingEnvelope
            };

            _processState.State = CommProcessState.PossibleState.Initialized;
        }

        public void Start()
        {
            _myUdpCommunicator.Start();
        }

        public void Stop()
        {
            if(_myUdpCommunicator != null)
            {
                _myUdpCommunicator.Stop();
                _myUdpCommunicator = null;
            }
        }

        public virtual T CreateFromConversationType<T>() where T : Conversation, new()
        {
            return _conversationFactory.CreateFromConversationType<T>();
        }

        public ConversationQueue SetupConversationQueue(MessageId convId)
        {
            return _queueDictionary.CreateQueue(convId);
        }

        public void CloseConversationQueue(MessageId convId)
        {
            _queueDictionary.CloseQueue(convId);
        }

        public Error Send(Envelope env)
        {
            return _myUdpCommunicator.Send(env);
        }

        public void ProcessIncomingEnvelope(Envelope env)
        {
            if(env == null)
            {
                return;
            }

            ConversationQueue existConversationQueue = _queueDictionary.Lookup(env.MessageToBeSent.ConvId); // need to change ConvID to MessageID
            if(existConversationQueue != null)
            {
                existConversationQueue.Enqueue(env);
            }
            else
            {
                Conversation conv = _conversationFactory.CreateFromMessage(env);
                conv?.Launch();
            }
        }

        public void SetupMultiCastAddress(string groupAddress)
        {

        }

        private IPAddress FindBestLocalIpAddress()
        {
            if(_bestAddress != null)
            {
                return _bestAddress;
            }

            long bestPreferredLifeTime = 0;
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                if(adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach(UnicastIPAddressInformation ip in adapter.GetIPProperties().UnicastAddresses)
                    {
                        if(ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            if(_bestAddress == null || ip.AddressPreferredLifetime > bestPreferredLifeTime)
                            {
                                _bestAddress = ip.Address;
                                bestPreferredLifeTime = ip.AddressPreferredLifetime;
                            }
                        }
                    }
                }
            }
            return _bestAddress;
        }


    }
}
