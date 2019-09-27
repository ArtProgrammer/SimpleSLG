using System.Collections.Generic;

namespace SimpleAI.Messaging
{
    /// <summary>
    /// The message to be sent by dispatcher among the entities.
    /// </summary>
    public class Telegram
    {
        public int SenderID;

        public int ReceiverID;

        public int MsgType;

        public float DispatchTime;

        public Telegram(float dispathtime, int senderid, int receiverid, int msgtype)
        {
            DispatchTime = dispathtime;
            SenderID = senderid;
            ReceiverID = receiverid;
            MsgType = msgtype;
        }

        public Telegram()
        { 

        }
    }

    /// <summary>
    /// The comparer used to sorted the messages by the DispatchTime
    /// with latest in the front.
    /// </summary>
    public class  TelegramCompare : IComparer<Telegram>
    {
        int IComparer<Telegram>.Compare(Telegram x, Telegram y)
        {
            if (x.DispatchTime.Equals(y.DispatchTime))
            {
                return 0;
            }

            if (x.DispatchTime > y.DispatchTime)
            {
                return 1;
            }

            return -1;
        }
    }
}
