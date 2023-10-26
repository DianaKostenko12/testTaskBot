using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgInfoBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelegramBotClient client = new TelegramBotClient("6768021218:AAGW1iPHFXoG4BYcuR4ottJhvDL9GGdj73g");

            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            Message message = update?.Message;

            if (message?.Text != null)
            {
                if(message.Text == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Write name of the channel:");
                    return;
                }

                ChatMember getChatMember = await botClient.GetChatMemberAsync(message.Text, message.From.Id);

                string chatMemberStatus = getChatMember.Status.ToString();
                bool isNotMember = chatMemberStatus == "Left" || chatMemberStatus == null || chatMemberStatus == "null" || chatMemberStatus == "";

                await botClient.SendTextMessageAsync(message.Chat.Id, $"The user is {(isNotMember ? "not" : "")} subscribed to this channel");
            }
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            Console.WriteLine("Error happend!");
            return Task.CompletedTask;
        }
    }
}