using System;
using System.Collections.Generic;

using SimpleAI.Game;
using SimpleAI.Utils;
using SimpleAI.Logger;
using SimpleAI.Timer;

namespace SimpleAI.Messaging
{
    public sealed class MessageDispatcher : 
        SingletonAsComponent<MessageDispatcher>, IUpdateable
    {
        private float ImmediatelyMsgTime = float.Epsilon;

        private SortedSet<Telegram> PriorityQ = 
            new SortedSet<Telegram>(new TelegramCompare());

        static MessageDispatcher() { }

        private MessageDispatcher() { }

        ~MessageDispatcher()
        {
            PriorityQ.Clear();
        }

        public static MessageDispatcher Instance
        {
            get {
                return (MessageDispatcher)InsideInstance;
            }
        }

        void Start()
        {
            GameLogicSupvisor.Instance.Register(this);
        }

        /// <summary>
        /// Send the message to the receiver.
        /// </summary>
        /// <param name="receiver">The reference of 
        /// the entity receiving the message.</param>
        /// <param name="msg">The message reference to be sent.</param>
        private void Discharge(BaseGameEntity receiver, Telegram msg)
        {
            TinyLogger.Instance.DebugLog("$ discharge msg start!");
            if (!System.Object.ReferenceEquals(receiver, null))
            { 
                if (!receiver.HandleMessage(msg))
                {
                    // receiver can not handle this message.
                    TinyLogger.Instance.DebugLog(
                        string.Format("$ failed to handle the msg!"));
                } else {
                    TinyLogger.Instance.DebugLog(
                        string.Format("$ handle the msg!"));
                }
            } else
            {
                TinyLogger.Instance.DebugLog("$ Discharge msg null receiver!");
            }
        }

        /// <summary>
        /// Uniform method adding the messages to be dispatched.
        /// Dispatch the no delay messages immediately,
        /// and record the delayed messages into a queue sorted
        /// by they delayed time with latest in the front.
        /// </summary>
        /// <param name="delay">The time to delay dispatching
        ///  the message.</param>
        /// <param name="senderID">The id of sender entity.</param>
        /// <param name="receiverID">The id of the target entity to 
        /// receive the message.</param>
        /// <param name="msgType">The type of the message for 
        /// identifying.</param>
        public void DispatchMsg(float delay, 
                int senderID, 
                int receiverID, 
                int msgType)
        {
            BaseGameEntity receiver = 
                EntityManager.Instance.GetEntityByID(receiverID);

            if (System.Object.ReferenceEquals(receiver, null)) {
                // no receiver with id == receiverID
                return;
            }

            Telegram telegram = new Telegram(0, senderID, receiverID, msgType);

            if (delay < ImmediatelyMsgTime) {
                Discharge(receiver, telegram);
            }
            else {
                float currentTime = TimeWrapper.Instance.realtimeSinceStartup;
                telegram.DispatchTime = currentTime + delay;

                // insert in the queue.
                PriorityQ.Add(telegram);
            }
        }

        /// <summary>
        /// Dispatchs the message.
        /// </summary>
        /// <param name="delay">Delay.</param>
        /// <param name="senderID">Sender identifier.</param>
        /// <param name="receiverID">Receiver identifier.</param>
        /// <param name="msg">Message.</param>
        public void DispatchMsg(float delay, 
            int senderID, 
            int receiverID, 
            int msgType, 
            Telegram msg)
        {
            if (System.Object.ReferenceEquals(msg, null))
            {
                // no receiver with id == receiverID
                return;
            }

            BaseGameEntity receiver = 
                EntityManager.Instance.GetEntityByID(receiverID);

            if (System.Object.ReferenceEquals(receiver, null))
            {
                // no receiver with id == receiverID
                return;
            }

            msg.DispatchTime = delay;
            msg.SenderID = senderID;
            msg.ReceiverID = receiverID;
            msg.MsgType = msgType;

            if (delay < ImmediatelyMsgTime)
            {
                // todo: there should be a queue to hold left immediately messages to
                // limit the time cost on them if there were to many messages per frame.
                Discharge(receiver, msg);
            }
            else
            {
                float currentTime = TimeWrapper.Instance.realtimeSinceStartup;
                msg.DispatchTime = currentTime + delay;

                // insert in the queue.
                PriorityQ.Add(msg);
            }
        }

        public void DispatchMsg(Telegram msg)
        {
            if (System.Object.ReferenceEquals(msg, null))
            {
                TinyLogger.Instance.DebugLog("$DispatchMsg null msg!");
                return;
            }

            BaseGameEntity receiver = 
                EntityManager.Instance.GetEntityByID(msg.ReceiverID);

            if (System.Object.ReferenceEquals(receiver, null))
            {
                TinyLogger.Instance.DebugLog(
                    "$DispatchMsg failed to found receicer!");
                return;
            }

            if (msg.DispatchTime < ImmediatelyMsgTime)
            {
                Discharge(receiver, msg);
            }
            else
            {
                float currentTime = TimeWrapper.Instance.realtimeSinceStartup;
                msg.DispatchTime = currentTime + msg.DispatchTime;

                // insert in the queue.
                PriorityQ.Add(msg);
            }
        }

        /// <summary>
        /// Check and dispatch the delayed messages that dispatch time arrived.
        /// </summary>
        void DispatchDelayedMessages(float dt)
        {
            // TODO: get the current time.
            float currentTime = TimeWrapper.Instance.realtimeSinceStartup;

            while (PriorityQ.Count > 0 && 
                PriorityQ.Min.DispatchTime < currentTime &&
                PriorityQ.Min.DispatchTime > 0.0) {

                Telegram msg = PriorityQ.Min;

                BaseGameEntity receiver = 
                    EntityManager.Instance.GetEntityByID(msg.ReceiverID);

                Discharge(receiver, msg);

                PriorityQ.Remove(PriorityQ.Min);
            }
        }

        public void OnUpdate(float dt)
        {
            DispatchDelayedMessages(dt);
        }

        public void OnFixedUpdate(float dt)
        {

        }
    }
}
