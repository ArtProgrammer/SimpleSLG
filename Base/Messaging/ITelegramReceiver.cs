
namespace SimpleAI.Messaging
{
    public interface ITelegramReceiver
    {
        bool HandleMessage(Telegram msg);
    }
}
