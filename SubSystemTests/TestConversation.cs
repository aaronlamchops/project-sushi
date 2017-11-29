using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommSubSystem;
using Messages;
using SharedObjects;
using CommSubSystem.ConversationClass;

namespace SubSystemTests
{
    [TestClass]
    class TestConversation
    {
        [TestMethod]
        public void testFactoryNewConv()
        {
            ConversationFactory._Instance.CreateFromConversationType<CreateGame>();
        }
    }
}
